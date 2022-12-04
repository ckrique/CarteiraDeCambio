using CarteiraDeCambio.DTO;
using CarteiraDeCambio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarteiraDeCambio.Repositories;
using CarteiraDeCambio.Business;
using CarteiraDeCambio.Service;

namespace CarteiraDeCambio.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AtivosCambiaisController : Controller
    {
        private readonly IAtivoCambialRepository _ativoCambialRepository;
        private readonly IMoedaRepository _moedaRepository;
        private readonly ISaldoRepository _saldoRepository;
        private readonly ICarteiraDeCambioService _carteiraDeCambioService;

        public AtivosCambiaisController(IAtivoCambialRepository ativoCambialRepository, 
                                        IMoedaRepository moedaRepository, 
                                        ISaldoRepository saldoRepository,
                                        ICarteiraDeCambioService carteiraDeCambioService) 
        {
            _ativoCambialRepository = ativoCambialRepository;
            _moedaRepository = moedaRepository;
            _saldoRepository = saldoRepository;
            _carteiraDeCambioService = carteiraDeCambioService;            
        }


        [HttpPost]
        //[ProducesResponseType(typeof(AtivoCambial), StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ReceberCompraDeMoeda")]
        //public ActionResult<AtivoCambial> ReceberCompraDeMoeda([FromBody] string siglaMoeda, decimal valorDoAtivoRecebido)
        public ActionResult ReceberCompraDeMoeda([FromBody] AtivoCambialDTO ativoCambialDTO)
        {
            if (string.IsNullOrEmpty(ativoCambialDTO.siglaMoeda) || ativoCambialDTO.valor <= 0)
                return BadRequest("Operação Incorreta, verifique os valores enviados");

            //TODO: validar sigla da moeda            
            
            Moeda moedaDaDoAtivo = _moedaRepository.GetMoedaBySiglaSync(ativoCambialDTO.siglaMoeda);

            AtivoCambial ativoCamvial = new AtivoCambial();

            ativoCamvial.idMoeda = moedaDaDoAtivo.Id;

            ativoCamvial.valor = ativoCambialDTO.valor;

            ativoCamvial.dataCriacao = DateTime.Now;

            _ativoCambialRepository.CreateAtivoCambial(ativoCamvial);

            Saldo saldo = _saldoRepository.GetSaldoByIdMoedaSync(moedaDaDoAtivo.Id);

            saldo.valor = saldo.valor + ativoCambialDTO.valor;

            _saldoRepository.UpdateSaldo(saldo);

            return Ok();
        }

        [HttpPost]
        //[ProducesResponseType(typeof(AtivoCambial), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("RealizarVendaDeMoeda")]
        public ActionResult<AtivoCambial> RealizarVendaDeMoeda([FromBody] AtivoCambialDTO ativoCambialDTO)
        {
            if (string.IsNullOrEmpty(ativoCambialDTO.siglaMoeda) || ativoCambialDTO.valor <= 0)
                return BadRequest("Operação Incorreta, verifique os valores enviados.");

            //TODO: validar sigla da moeda            

            Moeda moedaDaDoAtivo = _moedaRepository.GetMoedaBySiglaSync(ativoCambialDTO.siglaMoeda);

            Saldo saldo = _saldoRepository.GetSaldoByIdMoedaSync(moedaDaDoAtivo.Id);

            if (saldo.valor < ativoCambialDTO.valor)
                return BadRequest(string.Format("Não há saldo suficiente na carteira de {0} para realizar esta venda.", moedaDaDoAtivo.nome));

            AtivoCambial ativoCamvial = new AtivoCambial();

            ativoCamvial.idMoeda = moedaDaDoAtivo.Id;
            ativoCamvial.valor = ativoCambialDTO.valor;
            ativoCamvial.dataCriacao = DateTime.Now;

            _ativoCambialRepository.CreateAtivoCambial(ativoCamvial);            

            saldo.valor = saldo.valor - ativoCambialDTO.valor;

            _saldoRepository.UpdateSaldo(saldo);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SaldoDTO>), StatusCodes.Status200OK)]
        [Route("ListarSaldos")]
        //public async Task<ActionResult<IEnumerable<SaldoDTO>>> ListarSaldos()
        //public async Task<ActionResult<IEnumerable<Saldo>>> ListarSaldos()
        public async Task<ActionResult<IEnumerable<SaldoDTO>>> ListarSaldos()
        {
            List<SaldoDTO> listaDeSaldosDTO = new List<SaldoDTO>();
            IEnumerable<Moeda> listaMoedas = await _moedaRepository.GetMoedas();


            //List<Saldo> listaSaldos = (List<Saldo>)await _saldoRepository.GetSaldos();

            List<Saldo> listaSaldos = (List<Saldo>) _saldoRepository.GetSaldosSync();

            foreach (Saldo saldo in listaSaldos)
            {
                SaldoDTO saldoDTO = new SaldoDTO();
                saldoDTO.valor = saldo.valor.ToString();
                saldoDTO.siglaMoeda = listaMoedas.Where(m => m.Id.Equals(saldo.idMoeda)).FirstOrDefault().sigla;
                saldoDTO.NomeMoeda = listaMoedas.Where(m => m.Id.Equals(saldo.idMoeda)).FirstOrDefault().nome;

                listaDeSaldosDTO.Add(saldoDTO);
            }

            return Ok(listaDeSaldosDTO);

            //return listaSaldos;
        }

    }
}
