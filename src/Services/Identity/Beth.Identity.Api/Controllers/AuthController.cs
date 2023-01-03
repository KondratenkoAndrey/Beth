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

        public AuthController(IOneTimeCodeService oneTimeCodeService)
        {
            _oneTimeCodeService = oneTimeCodeService;
        }

        [Route("otc/mobile/{mobilePhone}")]
        [HttpGet]
        [ProducesResponseType(typeof(SentCodeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> SendOtcToPhone(
            [Required]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "Должно быть 10 символов")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Должно содержать только цифры")]
            string mobilePhone)
        {
            var (code, isNew) = await _oneTimeCodeService.SendOneTimeCode(mobilePhone);
            var response = new SentCodeModel(code, isNew);
            return Ok(response);
        }

        [Route("otc/mobile/{mobilePhone}/verify/{code:int}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VerifyCode(string mobilePhone, int code)
        {
            var oneTimeCode = await _oneTimeCodeService.FindOneTimeCodeAsync(mobilePhone);
            if (oneTimeCode == null)
            {
                return NotFound();
            }

            if (oneTimeCode.Code != code)
            {
                return BadRequest();
            }
            
            return Ok();
        }
    }
}