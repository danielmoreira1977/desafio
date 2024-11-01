namespace Questao5.Domain.Entities
{
    public sealed class Movimento
    {
        public string IdMovimento { get; set; }
        public Guid IdContaCorrentex { get; set; }
        public DateTime DataMovimento { get; set; }
        public decimal Valor { get; set; }
        public string  TipoMovimento{ get; set; }

    }
}
