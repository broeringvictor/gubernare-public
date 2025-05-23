using Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Contracts;
using Gubernare.Domain.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin;

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


        Entities.CourtLogin courtLogin;

        try
        {
            courtLogin = new Entities.CourtLogin(
                userId: request.UserId,
                courtSystem: request.CourtSystem,
                login: request.Login,
                password: new EncryptedPassword(request.Password)
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
            await _repository.SaveAsync(courtLogin, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response(
            "Tribunal registrado com sucesso",
            new ResponseData(courtLogin.CourtSystem) // De acordo com Request
        );
    }
}