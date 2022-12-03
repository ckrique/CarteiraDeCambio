using CarteiraDeCambio.DTO;
using CarteiraDeCambio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarteiraDeCambio.Repositories;
using CarteiraDeCambio.Business;

namespace CarteiraDeCambio.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AtivosCambiaisController : Controller
    {
        private readonly IAtivoCambialRepository _ativoCambialRepository;
        private readonly IMoedaRepository _moedaRepository;
        private readonly ISaldoRepository _saldoRepository;
        private CarteiraDeCambioService _carteiraDeCambioService;

        public AtivosCambiaisController(IAtivoCambialRepository ativoCambialRepository, 
                                        IMoedaRepository moedaRepository, 
                                        ISaldoRepository saldoRepository,
                                        CarteiraDeCambioService carteiraDeCambioBusiness) 
        {
            _ativoCambialRepository = ativoCambialRepository;
            _moedaRepository = moedaRepository;
            _saldoRepository = saldoRepository;
            _carteiraDeCambioService = carteiraDeCambioBusiness;

            _carteiraDeCambioService.InicializaMoedasNoBancoAsync();
            _carteiraDeCambioService.InicializaSaldosDeCarteirasNoBancoAsync();
        }


        [HttpPost]
        [ProducesResponseType(typeof(AtivoCambial), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ReceberCompraDeMoeda")]
        public ActionResult<AtivoCambial> ReceberCompraDeMoeda([FromBody] string siglaMoeda, decimal valor)
        {
            if (string.IsNullOrEmpty(siglaMoeda) || valor == 0)
                return BadRequest("Invalid product");

            _carteiraDeCambioService.ReceberCompraDeMoedaAsync(siglaMoeda, valor);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(AtivoCambial), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("RealizarVendaDeMoeda")]
        public ActionResult<AtivoCambial> RealizarVendaDeMoeda([FromBody] string siglaMoeda, decimal valor)
        {
            if (string.IsNullOrEmpty(siglaMoeda) || valor == 0)
                return BadRequest("Invalid product");

            //await _testObjectRepository.CreateTestObject(testObject);

            //return CreatedAtRoute("GetTestObject", new { id = testObject.Id }, testObject);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SaldoDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SaldoDTO>>> ListarSaldos()
        {
            List<SaldoDTO> listaDeSaldosDTO = new List<SaldoDTO>();
            IEnumerable<Moeda> listaMoedas = await _moedaRepository.GetMoedas();


            List<Saldo> saldos = (List<Saldo>)await _saldoRepository.GetSaldos();

            foreach (Saldo saldo in saldos) 
            {
                SaldoDTO saldoDTO= new SaldoDTO();
                saldoDTO.valor = saldo.valor.ToString();
                saldoDTO.siglaMoeda = listaMoedas.Where(m => m.Id == saldo.Id).FirstOrDefault().sigla;
                saldoDTO.NomeMoeda = listaMoedas.Where(m => m.Id == saldo.Id).FirstOrDefault().nome;

                listaDeSaldosDTO.Add(saldoDTO);
            }

            return Ok(listaDeSaldosDTO);
        }

    }
}
