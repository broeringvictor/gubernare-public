using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts;
using MediatR;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Valida a Requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Gerar os Objetos

        Contract contract;

        try
        {
            // Inicializando o objeto Contract
            contract = new Contract(
                name: request.Name,
                type: request.Type,
                description: request.Description,
                notes: request.Notes,
                startDate: request.StartDate,
                endDate: request.EndDate,
                price: request.Price,
                documentFolder: request.DocumentFolder
            );
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 03. Persiste os Dados

        try
        {
            await _repository.SaveAsync(contract, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response(
            "Contrato criado com sucesso",
            new ResponseData(contract.Id, contract.Name) // De acordo com Request
        );
    }
}
