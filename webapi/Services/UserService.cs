using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using webapi.Context;
using webapi.DTOs;
using webapi.Features.Usuarios.Commands.CreateUsuario;
using webapi.Features.Usuarios.Commands.DeleteUsuario;
using webapi.Features.Usuarios.Commands.UpdateUsuario;
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

        public async Task<OperationResult<bool>> Update(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        public async Task<OperationResult<bool>> Delete(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }

    public interface IUserService
    {
        Task<IReadOnlyCollection<UsuarioDto>> GetUsuarios();
        Task<OperationResult<bool>> Save(CreateUsuarioCommand request, CancellationToken cancellationToken);
        Task<OperationResult<bool>> Update(UpdateUsuarioCommand request, CancellationToken cancellationToken);
        Task<OperationResult<bool>> Delete(DeleteUsuarioCommand request, CancellationToken cancellationToken);
    }
}
