using Microsoft.AspNetCore.Mvc;
using Act3Treasury.Dtos;
using Act3Treasury.Services;

namespace Act3Treasury.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreasuryController : ControllerBase
    {
        private readonly ITreasuryService _treasuryService;
        public TreasuryController(ITreasuryService treasuryService)
        {
            _treasuryService = treasuryService;
        }

        [HttpPost ("restore")]
        public IActionResult Restore([FromBody] RestoreRequest request)
        {
            try
            {
                var result = _treasuryService.Restore(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
