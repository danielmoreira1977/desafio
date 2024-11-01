namespace Questao5.Domain.Entities
{
    public sealed class Idempotencia
    {
        public Guid Chave_Idempotencia { get; set; }
        public string Requisicao { get; set; }
        public bool Resultado{ get; set; }

    }
}
