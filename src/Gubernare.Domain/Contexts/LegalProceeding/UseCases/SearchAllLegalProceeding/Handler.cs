using System.Text.Json;
using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.AccountContext.ValueObjects;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts;
using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding
{

    public class Handler(IService service, IRepository repository) : IRequestHandler<Request, Response>
    {



        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            #region 01. Recupera o CourtLogin

            CourtLogin? courtLogin;


            try
            {
                courtLogin = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (courtLogin is null)
                    return new Response("[A1RP100] Perfil não encontrado.", 404);
            }
            catch (Exception)
            {
                return new Response("[A1RP101] Não foi possível recuperar seu perfil", 500);
            }

            string courtCipher;

            try
            {
                courtCipher = EncryptedPassword.Decrypt(courtLogin.Password.Cipher);

            }
            catch (Exception)
            {
                return new Response("[A1RP102] Não foi possível descriptografar a sua senha.", 500);
            }

            #endregion



            #region 02. Enviar dados e processar resposta

            try
            {
                string searchResponse = await service.SendSearchAllLegalProceedingAsync(
                    login: courtLogin.Login.ToString(),
                    courtCipher,
                    cancellationToken
                );


                var result = JsonSerializer.Deserialize<JsonElement>(searchResponse);

                var listProceedings = new List<Entities.LegalProceeding>();

                if (result.ValueKind == JsonValueKind.Array)
                {
                    foreach (var proceedingElement in result.EnumerateArray())
                    {
                        var listProceedingsEvents = new List<Entities.LegalProceedingEvent>();

                        // Extrair eventos
                        if (proceedingElement.TryGetProperty("eventos", out JsonElement eventos)
                            && eventos.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var eventoArray in eventos.EnumerateArray().Skip(1))
                            {
                                if (eventoArray.ValueKind == JsonValueKind.Array
                                    && eventoArray.GetArrayLength() >= 5)
                                {
                                    var evento = new Entities.LegalProceedingEvent(
                                        legalProceedingId: Guid.NewGuid(),
                                        description: eventoArray[2].GetString() ?? "Descrição não disponível",
                                        date: DateTime.TryParse(eventoArray[1].GetString(), out var date)
                                            ? date
                                            : DateTime.MinValue,
                                        type: eventoArray[4].GetString() ?? "Não especificado",
                                        status: "Ativo",
                                        legalDeadline: DateTime.TryParse(eventoArray[4].GetString(), out var deadline)
                                            ? deadline
                                            : null
                                    );
                                    listProceedingsEvents.Add(evento);
                                }
                            }
                        }

                        // Extrair propriedades do processo com verificações
                        proceedingElement.TryGetProperty("Number", out var numberProp);
                        proceedingElement.TryGetProperty("CourtDivisionName",
                            out var courtDivisionNameProp); // Nome corrigido
                        proceedingElement.TryGetProperty("Competencia", out var competenciaProp);
                        proceedingElement.TryGetProperty("DataDistribuicao", out var dataDistribuicaoProp);
                        proceedingElement.TryGetProperty("Type", out var typeProp);

                        // Tratar data de distribuição
                        DateTime distributionDate = DateTime.TryParse(
                            dataDistribuicaoProp.GetString(),
                            out var parsedDate
                        )
                            ? parsedDate
                            : DateTime.Now;

                        var proceeding = new Entities.LegalProceeding(
                            number: numberProp.GetString() ?? "Número não disponível",
                            name: "",
                            clientRole: "",
                            courtDivisionName: courtDivisionNameProp.GetString() ?? "Divisão não disponível",
                            description: "",
                            legalCourt: competenciaProp.GetString() ?? "Competência não disponível",
                            accessCode: "",
                            distributionDate: distributionDate,
                            type: typeProp.GetString() ?? "Tipo não disponível",
                            status: "Ativo",
                            finishedDateTime: null,
                            courtInstance: null,
                            opposingPartyRole: null,
                            individualClients: null,
                            legalProceedingEvents: listProceedingsEvents,
                            opposingParties: null
                        );

                        listProceedings.Add(proceeding);
                    }
                }

                return new Response("Sucesso", new ResponseData(listProceedings));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Response("[A1RP104] Falha ao processar os dados.", 500);
            }
            
            
            #endregion

        }
    }
}