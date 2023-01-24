using System.ComponentModel.DataAnnotations;
using System.Net;
using Beth.Identity.Api.Models;
using Beth.Identity.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beth.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IOneTimeCodeService _oneTimeCodeService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IOneTimeCodeService oneTimeCodeService, ILogger<AuthController> logger)
        {
            _oneTimeCodeService = oneTimeCodeService;
            _logger = logger;
        }

        [Route("otc/mobile/{mobilePhone}")]
        [HttpGet]
        [ProducesResponseType(typeof(OneTimeCodeDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> SendOtcToPhone(
            [Required]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "Должно быть 10 символов")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Должно содержать только цифры")]
            string mobilePhone)
        {
            var (code, isNew) = await _oneTimeCodeService.RequestOneTimeCode(mobilePhone);
            var response = new OneTimeCodeDto(code, isNew);
            return Ok(response);
        }

        [Route("otc/mobile/{mobilePhone}/verify/{code:int}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VerifyCode(string mobilePhone, int code)
        {
            var result = await _oneTimeCodeService.VerifyCodeAsync(mobilePhone, code);
            return result ? Ok() : NotFound();
        }
    }
}