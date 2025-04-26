namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding;




public record ResponseData(IEnumerable<Entities.LegalProceeding> Proceedings);