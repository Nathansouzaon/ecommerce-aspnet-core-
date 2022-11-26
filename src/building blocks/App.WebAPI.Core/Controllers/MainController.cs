using App.Core.Communication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.WebAPI.Core.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida()) return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros) AdicionarErroProcessamento(erro.ErrorMessage);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors) AdicionarErroProcessamento(erro.ErrorMessage);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            ResponsePossuiErros(resposta);
            return CustomResponse();
        }

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta is null || resposta.Errors.Mensagens.Any() is false) return false;

            foreach (var mensagem in resposta.Errors.Mensagens) AdicionarErroProcessamento(mensagem);

            return true;
        }

        protected bool OperacaoValida() => Erros.Any() is false;

        protected void AdicionarErroProcessamento(string erro) => Erros.Add(erro);

        protected void LimparErrosProcessamento() => Erros.Clear();
    }
}
