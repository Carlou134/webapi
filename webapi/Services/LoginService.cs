using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Context;
using webapi.DTOs;
using webapi.Models;

namespace webapi.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDto?> Login(LoginRequestDto request);
    }

    public class LoginService : ILoginService
    {
        private readonly UsersContext _context;
        private readonly IConfiguration _configuration;

        public LoginService(
            UsersContext context, 
            IConfiguration configuration)
        {
            _context = context!;
            _configuration = configuration!;
        }

        public async Task<LoginResponseDto?> Login(LoginRequestDto request)
        {
            try
            {
                var usuario = await _context.Usuario.AsNoTracking()
                    .Include(x => x.Rol)
                    .FirstOrDefaultAsync(x => x.Email == request.Email).ConfigureAwait(false);

                if (usuario == null) return null;

                bool passwordValido = BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    usuario.Password
                    );

                if (!passwordValido) return null;

                return GenerarToken(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private LoginResponseDto GenerarToken(Usuario usuario)
        {
            var jwt = _configuration.GetSection("Jwt");
            int expireMinutes = int.Parse(jwt["ExpireMinutes"]!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.NombreUsuario),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol!.NombreRol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: creds
                );

            return new LoginResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expireMinutes
            };
        }
    }
}
