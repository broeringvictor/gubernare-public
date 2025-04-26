using System.Text;
using System.Text.Json;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding
{
    public class Service : IService
    {
        public async Task<string> SendSearchAllLegalProceedingAsync(string login, string password,
            CancellationToken cancellationToken)
        {
            var url = "http://localhost:5000/alllegalproceeding";
            using var httpClient = new HttpClient();

            try
            {
                var payload = new LoginRequest // Usando a classe corrigida
                {
                    Login = login,
                    Password = password
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync(cancellationToken);
                }
                else
                {
                    return $"Erro: {response.StatusCode}";
                }
            }
            catch (Exception e)
            {
                return $"Exceção: {e.Message}";
            }
        }
    }
}