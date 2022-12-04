namespace CarteiraDeCambio.Service
{
    public interface ICarteiraDeCambioService
    {
        public void InicializaBanco();
        public Task ReceberCompraDeMoedaAsync(string sigla, decimal valorDoAtivo);
    }
}
