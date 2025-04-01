using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts;
using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;
using MediatR;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validação da Requisição

        var validation = Specification.Ensure(request);
        if (!validation.IsValid)
            return new Response("Requisição inválida", 400, validation.Notifications);

        #endregion

        #region 02. Normalização e Verificação de CPF duplicado

        Cpf? cpf = null;

        try
        {
            if (!string.IsNullOrWhiteSpace(request.cpfNumber))
            {
                // Cria e valida o CPF (também limpa os caracteres especiais)
                cpf = new Cpf(request.cpfNumber);

                // Verifica se já existe no banco com o valor limpo
                var exists = await _repository.AnyAsync(cpf.CpfValue, cancellationToken);
                if (exists)
                    return new Response("Este CPF já está em uso", 400);
            }
        }
        catch (Exception ex)
        {
            return new Response($"Erro ao validar/verificar CPF: {ex.Message}", 400);
        }

        #endregion

        #region 03. Criação do RG

        Rg? rg = null;

        try
        {
            if (!string.IsNullOrWhiteSpace(request.rgNumber))
            {
                rg = new Rg(request.rgNumber);
            }
        }
        catch (Exception ex)
        {
            return new Response($"Erro ao validar RG: {ex.Message}", 400);
        }

        #endregion

        #region 04. Criação do objeto IndividualClient

        IndividualClient individualClient;

        try
        {
            individualClient = new IndividualClient(
                name: request.name,
                email: request.email,
                phone: request.phone,
                zipCode: request.zipCode,
                street: request.street,
                city: request.city,
                state: request.state,
                country: request.country,
                jobTitle: request.jobTitle,
                maritalStatus: request.maritalStatus,
                homeland: request.homeland,
                cpfNumber: cpf,
                rgNumber: rg,
                birthDate: request.birthDate,
                fristContact: request.fristContact
            );
        }
        catch (Exception ex)
        {
            return new Response($"Erro ao construir objeto do cliente: {ex.Message}", 400);
        }

        #endregion

        #region 05. Persistência dos dados

        try
        {
            await _repository.SaveAsync(individualClient, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao salvar dados no banco", 500);
        }

        #endregion

        return new Response(
            "Cliente criado com sucesso",
            new ResponseData(individualClient.Id, individualClient.Name)
        );
    }
}
