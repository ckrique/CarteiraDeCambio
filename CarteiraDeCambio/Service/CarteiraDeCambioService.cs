using CarteiraDeCambio.Entities;
using CarteiraDeCambio.Repositories;
using CarteiraDeCambio.Service;
using MongoDB.Driver;

namespace CarteiraDeCambio.Business
{
    public class CarteiraDeCambioService : ICarteiraDeCambioService
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

            InicializaBanco();
        }

        public void InicializaMoedasNoBanco() 
        {
            IEnumerable<Moeda> listaMoedas = _moedaRepository.GetMoedasSync();

            //foreach(Moeda moeda in listaMoedas)
            //{
            //    if(moeda.sigla.Equals(SIGLA_DOLAR_ESTADOS_UNIDOS))

            //}

            if (listaMoedas is null)
                listaMoedas = new List<Moeda>();
            
            if (!listaMoedas.Any() || listaMoedas.Where(m => m.sigla.Equals(SIGLA_DOLAR_ESTADOS_UNIDOS)).FirstOrDefault() is null)
            {
                Moeda moedaDolar = new Moeda();

                moedaDolar.sigla = SIGLA_DOLAR_ESTADOS_UNIDOS;
                moedaDolar.nome = DESCRICAO_DOLAR_ESTADOS_UNIDOS;

                //_moedaRepository.CreateMoeda(moedaDolar);
                _moedaRepository.CreateMoedaSync(moedaDolar);
            }

            if (!listaMoedas.Any() || listaMoedas.Where(m => m.sigla.Equals(SIGLA_EURO)).FirstOrDefault() is null)
            {
                Moeda moedaEuro = new Moeda();

                moedaEuro.sigla = SIGLA_EURO;
                moedaEuro.nome = DESCRICAO_EURO;

                //_moedaRepository.CreateMoeda(moedaEuro);
                _moedaRepository.CreateMoeda(moedaEuro);
            }

            if (!listaMoedas.Any() || listaMoedas.Where(m => m.sigla.Equals(SIGLA_REAL)).FirstOrDefault() is null)
            {
                Moeda moedaReal = new Moeda();

                moedaReal.sigla = SIGLA_REAL;
                moedaReal.nome = DESCRICAO_REAL;

                //_moedaRepository.CreateMoeda(moedaReal);
                _moedaRepository.CreateMoedaSync(moedaReal);
            }
        }

        public void InicializaSaldosDeCarteirasNoBanco()
        {
            IList<Saldo> listaSaldosEmCarteiras = (List<Saldo>) _saldoRepository.GetSaldosSync();

            Moeda moedaDolar = _moedaRepository.GetMoedaBySiglaSync(SIGLA_DOLAR_ESTADOS_UNIDOS);
            Moeda moedaEuro = _moedaRepository.GetMoedaBySiglaSync(SIGLA_EURO);
            Moeda moedaReal = _moedaRepository.GetMoedaBySiglaSync(SIGLA_REAL);
                       

            if (!listaSaldosEmCarteiras.Any() || listaSaldosEmCarteiras.Where(s => s.idMoeda.Equals(moedaDolar.Id)).FirstOrDefault() is null)
            {
                Saldo saldoCarteiraDeDolares = new Saldo();

                saldoCarteiraDeDolares.valor = 0;
                saldoCarteiraDeDolares.idMoeda = moedaDolar.Id;

                _saldoRepository.CreateSaldoSync(saldoCarteiraDeDolares);
            }

            if (!listaSaldosEmCarteiras.Any() || listaSaldosEmCarteiras.Where(s => s.idMoeda.Equals(moedaEuro.Id)).FirstOrDefault() is null)
            {
                Saldo saldoCarteiraDeEuros = new Saldo();

                saldoCarteiraDeEuros.valor = 0;
                saldoCarteiraDeEuros.idMoeda = moedaEuro.Id;

                _saldoRepository.CreateSaldoSync(saldoCarteiraDeEuros);
            }

            if (!listaSaldosEmCarteiras.Any() || listaSaldosEmCarteiras.Where(s => s.idMoeda.Equals(moedaReal.Id)).FirstOrDefault() is null)
            {
                Saldo saldoCarteiraDeReais = new Saldo();

                saldoCarteiraDeReais.valor = 0;
                saldoCarteiraDeReais.idMoeda = moedaReal.Id;

                _saldoRepository.CreateSaldoSync(saldoCarteiraDeReais);
            }
        }

        public void InicializaBanco()
        {
            /*
            //Create client connection to our MongoDB database
            var client = new MongoClient(MongoDBConnectionString);

            //Create a session object that is used when leveraging transactions
            var session = client.StartSession();

            session.StartTransaction();*/

            InicializaMoedasNoBanco();
            InicializaSaldosDeCarteirasNoBanco();
        }
    }
}