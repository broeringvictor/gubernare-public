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

            #region 02. Enviar os dados descriptografados para o Microserviço
            try
            {
                
              var search = await service.SendSearchAllLegalProceedingAsync(courtLogin.Login, courtCipher, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Response("[A1RP103] Falha ao enviar dados ao microserviço.", 500);
            }
            #endregion
            
            #region 03. Organizar os dados

          
            try
            {
                var legalProceedings = JsonSerializer.Deserialize<List<Entities.LegalProceeding>>(
                    search,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // Cria uma lista para armazenar os processos desserializados
                var proceedingsList = new List<Entities.LegalProceeding>();

                if (legalProceedings != null)
                {
                    foreach (var proceeding in legalProceedings)
                    {
                        // Adiciona cada processo à lista
                        proceedingsList.Add(proceeding);
                    }
                }

                // Aqui você pode usar a lista proceedingsList conforme necessário
                // (ex: adicionar ao repositório, incluir na resposta, etc.)
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Response("[A1RP104] Falha ao processar os dados.", 500);
            }
            #endregion


            return new Response("Sucesso", 200);
        }

        }
    }


