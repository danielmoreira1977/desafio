namespace Questao5.Application.Abstractions.Messaging;


public abstract record IdempotenciaCommand(    Guid chave_idempotencia );
