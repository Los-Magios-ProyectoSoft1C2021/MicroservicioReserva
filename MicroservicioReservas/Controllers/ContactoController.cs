using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Template.Application.EmailServices;
using Template.Domain.DTOs.Request;
using Template.Domain.Email;

namespace MicroservicioReservas.Controllers
{
    [Route("api/contacto")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private EmailSenderOptions Options { get; set; }
        private readonly IEmailSender _sender;

        public ContactoController(IEmailSender sender, IOptions<EmailSenderOptions> options)
        {
            _sender = sender;
            Options = options.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SendMailForContacto([FromBody] ContactoEmail request)
        {
            await _sender.SendEmailAsync(subject: request.Motivo, email: Options.Email, message: request.GetMensaje())
                .ConfigureAwait(false);

            return Ok();
        }
    }
}
