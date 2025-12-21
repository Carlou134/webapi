using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.DTOs;
using webapi.Features.Usuarios.Commands.CreateUsuario;
using webapi.Models;

namespace webapi.Services
{
    public class UserService : IUserService
    {
        private readonly UsersContext _usersContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserService(
            UsersContext usersContext,
            IMapper mapper,
            IMediator mediator)
        {
            _usersContext = usersContext!;
            _mapper = mapper!;
            _mediator = mediator!;
        }

        public async Task<IReadOnlyCollection<UsuarioDto>> GetUsuarios()
        {
            var usuarios = await _usersContext.Usuario.AsNoTracking().Include(x => x.Rol).ToListAsync();
            return _mapper.Map<List<UsuarioDto>>(usuarios);
        }

        public async Task<OperationResult<bool>> Save(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        public Task Update(Guid id, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserService
    {
        Task<IReadOnlyCollection<UsuarioDto>> GetUsuarios();
        Task<OperationResult<bool>> Save(CreateUsuarioCommand request, CancellationToken cancellationToken);
        Task Update(Guid id, Usuario usuario);
        Task Delete(Guid id);
    }
}
