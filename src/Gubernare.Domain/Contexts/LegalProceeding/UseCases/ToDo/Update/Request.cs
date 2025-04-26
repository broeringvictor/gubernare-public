using MediatR;
using System;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update;

public sealed record Request(
    Guid?      Id,
    string?   Title,
    string?   Description,
    DateTime? DueDate,
    bool?     IsCompleted
) : IRequest<Response>;