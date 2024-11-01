using Questao5.Domain.Shared;

namespace Questao5.Domain.Errors;

public static class DomainErrors
{
    public static class ContaCorrente
    {
        public static readonly Error ContaInvalida = new(
            "ContaCorrente.ContaInvalida",
            "TIPO: INVALID_ACCOUNT");


        public static readonly Error ContaInativa = new(
            "ContaCorrente.ContaInativa",
            "TIPO: INACTIVE_ACCOUNT");

    }    
    
    public static class Movimento
    {

        public static readonly Error ValorInvalido= new(
            "Movimento.ValorInvalido",
            "TIPO: INVALID_VALUE"); 
        
        public static readonly Error TipoInvalido= new(
            "Movimento.TipoInvalido",
            "TIPO: INVALID_TYPE");
    }  
    
    public static class Idempotencia
    {

        public static readonly Error RequisicaoProcessada= new(
            "Idempotencia.RequisicaoProcessada",
            "TIPO:EXISTING_REQUEST"); 
    }
}