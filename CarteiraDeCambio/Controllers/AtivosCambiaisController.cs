using CarteiraDeCambio.DTO;
using CarteiraDeCambio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarteiraDeCambio.Repositories;

namespace CarteiraDeCambio.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AtivosCambiaisController : Controller
    {
        private readonly IAtivoCambialRepository _ativoCambialRepository;
        private readonly IMoedaRepository _moedaRepository;
        private readonly ISaldoRepository _saldoRepository;

        public AtivosCambiaisController(IAtivoCambialRepository ativoCambialRepository, 
                                        IMoedaRepository moedaRepository, 
                                        ISaldoRepository saldoRepository) 
        {
            _ativoCambialRepository = ativoCambialRepository;
            _moedaRepository = moedaRepository; ;
            _saldoRepository = saldoRepository;
        }


        [HttpPost]
        [ProducesResponseType(typeof(AtivoCambial), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AtivoCambial>> ReceberCompraDeMoeda([FromBody] string siglaMoeda, decimal valor)
        {
            if (string.IsNullOrEmpty(siglaMoeda) || valor == 0)
                return BadRequest("Invalid product");

            //await _testObjectRepository.CreateTestObject(testObject);

            //return CreatedAtRoute("GetTestObject", new { id = testObject.Id }, testObject);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(AtivoCambial), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AtivoCambial>> RealizarVendaDeMoeda([FromBody] string siglaMoeda, decimal valor)
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
            IEnumerable<SaldoDTO> listaDeSaldos = new List<SaldoDTO>();
            
            var saldos = await _saldoRepository.GetTestObjects();

            return Ok(listaDeSaldos);
        }

    }
}
