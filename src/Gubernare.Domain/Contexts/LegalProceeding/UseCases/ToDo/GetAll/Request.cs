using MediatR;
using System;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll;

public sealed record Request(Guid UserId) : IRequest<Response>;