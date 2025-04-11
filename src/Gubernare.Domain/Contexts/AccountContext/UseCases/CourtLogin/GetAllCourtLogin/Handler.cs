using Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Contracts;
using MediatR;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin
{
    // Request é sua classe de comando. Por exemplo:
    // public record Request(Guid UserId) : IRequest<Response>;

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;

        public Handler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            // Chama o repositório para obter a lista
            var courtLogins = await _repository.GetAllAsync(request.UserId, cancellationToken);

            // Mapeia cada CourtLogin para ResponseData
            var resultList = courtLogins
                .Select(x => new ResponseData(
                    CourtSystem: x.CourtSystem,
                    Id: x.Id
                ))
                .ToList();

            // Retorna a lista inteira
            return new Response(
                "Lista de tribunais obtida com sucesso!",
                resultList
                
            );
        }
    }
}