
namespace Questao5.Domain.Entities
{
    public sealed  class ContaCorrente
    {
        public string IdContaCorrente { get; private set; }
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo {  get; private set; }
        

        private readonly List<Movimento> _movimentos = new();

        public IReadOnlyCollection<Movimento> Movimentos => _movimentos;

        public void AddMovimentacao(List<Movimento> movimentos) 
        {
            _movimentos.AddRange(movimentos);
        }

        private decimal TotalDeCredito => RetornaTotalDeMovimentacoes("C");
        private decimal TotalDeDebitos => RetornaTotalDeMovimentacoes("D");

        private decimal RetornaTotalDeMovimentacoes(string tipo) => _movimentos.Where(item => item.TipoMovimento == tipo).Sum(item => item.Valor);

        public decimal Saldo => TotalDeCredito - TotalDeDebitos;
}
}
