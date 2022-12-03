using CarteiraDeCambio.Entities;
using CarteiraDeCambio.Repositories;

namespace CarteiraDeCambio.Business
{
    public class CarteiraDeCambioService
    {
        //SIGLAS ISO 4217
        public const string SIGLA_DOLAR_ESTADOS_UNIDOS = "USD";
        public const string SIGLA_EURO = "EUR";
        public const string SIGLA_REAL = "BRL";

        public const string DESCRICAO_DOLAR_ESTADOS_UNIDOS = "Dólares Norte-Americanos";
        public const string DESCRICAO_EURO = "Euros";
        public const string DESCRICAO_REAL = "Reais";

        private readonly IAtivoCambialRepository _ativoCambialRepository;
        private readonly IMoedaRepository _moedaRepository;
        private readonly ISaldoRepository _saldoRepository;

        public CarteiraDeCambioService(IAtivoCambialRepository ativoCambialRepository,
                                        IMoedaRepository moedaRepository,
                                        ISaldoRepository saldoRepository)
        {
            _ativoCambialRepository = ativoCambialRepository;
            _moedaRepository = moedaRepository;
            _saldoRepository = saldoRepository;

        }


        public async Task ReceberCompraDeMoedaAsync(string sigla, decimal valorDoAtivo) 
        {
            AtivoCambial ativoCambial = new AtivoCambial();
            Saldo saldo = new Saldo();

            Moeda moeda = await _moedaRepository.GetMoedaBySigla(sigla);

            if (moeda is null)
                throw new Exception("Erro ao tentar receber a compra do Ativo Cambial na Carteira");

            ativoCambial.idMoeda = moeda.Id;

            ativoCambial.dataCriacao = DateTime.Now;
            _ativoCambialRepository.CreateAtivoCambial(ativoCambial);

            saldo = await _saldoRepository.GetSaldoByIdMoeda(moeda.Id);

            saldo.valor = saldo.valor + valorDoAtivo;

            _saldoRepository.UpdateSaldo(saldo);

            //TODO: REALIZAR INSERT DA MOEDA NO BANCO DE DADOS
        }

        public async Task InicializaMoedasNoBancoAsync() 
        {            
            IList<Moeda> listaMoedas = (List<Moeda>)await _moedaRepository.GetMoedas();  
            
            if (listaMoedas.Where(m => m.sigla.Equals(SIGLA_DOLAR_ESTADOS_UNIDOS)).FirstOrDefault() is null)
            {
                Moeda moedaDolar = new Moeda();

                moedaDolar.sigla = SIGLA_DOLAR_ESTADOS_UNIDOS;
                moedaDolar.nome = DESCRICAO_DOLAR_ESTADOS_UNIDOS;

                _moedaRepository.CreateMoeda(moedaDolar);
            }

            if (listaMoedas.Where(m => m.sigla.Equals(SIGLA_EURO)).FirstOrDefault() is null)
            {
                Moeda moedaEuro = new Moeda();

                moedaEuro.sigla = SIGLA_EURO;
                moedaEuro.nome = DESCRICAO_EURO;

                _moedaRepository.CreateMoeda(moedaEuro);
            }

            if (listaMoedas.Where(m => m.sigla.Equals(SIGLA_REAL)).FirstOrDefault() is null)
            {
                Moeda moedaReal = new Moeda();

                moedaReal.sigla = SIGLA_REAL;
                moedaReal.nome = DESCRICAO_REAL;

                _moedaRepository.CreateMoeda(moedaReal);
            }
        }

        public async Task InicializaSaldosDeCarteirasNoBancoAsync()
        {
            IList<Saldo> listaSaldosEmCarteiras = (List<Saldo>)await _saldoRepository.GetSaldos();

            Moeda moedaDolar = await _moedaRepository.GetMoedaBySigla(SIGLA_DOLAR_ESTADOS_UNIDOS);
            Moeda moedaEuro = await _moedaRepository.GetMoedaBySigla(SIGLA_EURO);
            Moeda moedaReal = await _moedaRepository.GetMoedaBySigla(SIGLA_REAL);


            if (listaSaldosEmCarteiras.Where(s => s.idMoeda.Equals(moedaDolar.Id)).FirstOrDefault() is null)
            {
                Saldo saldoCarteiraDeDolares = new Saldo();

                saldoCarteiraDeDolares.valor = 0;
                saldoCarteiraDeDolares.idMoeda = moedaDolar.Id;

                _saldoRepository.CreateSaldo(saldoCarteiraDeDolares);
            }

            if (listaSaldosEmCarteiras.Where(s => s.idMoeda.Equals(moedaEuro.Id)).FirstOrDefault() is null)
            {
                Saldo saldoCarteiraDeEuros = new Saldo();

                saldoCarteiraDeEuros.valor = 0;
                saldoCarteiraDeEuros.idMoeda = moedaDolar.Id;

                _saldoRepository.CreateSaldo(saldoCarteiraDeEuros);
            }

            if (listaSaldosEmCarteiras.Where(s => s.idMoeda.Equals(moedaReal.Id)).FirstOrDefault() is null)
            {
                Saldo saldoCarteiraDeReais = new Saldo();

                saldoCarteiraDeReais.valor = 0;
                saldoCarteiraDeReais.idMoeda = moedaDolar.Id;

                _saldoRepository.CreateSaldo(saldoCarteiraDeReais);
            }
        }
    }
}