using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Exercicios.Controllers
{
    /// <summary>
    ///  Microsoft.AspNetCore.Http.StatusCode
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ListaExercicios : ControllerBase
    {

        /// <summary>
        /// 1xx Informatinal
        /// </summary>
        /// <returns></returns>
        [HttpGet("Exercicio1")]
        public IActionResult Exercicio1(int valor1, int valor2)
        {
            if ((valor1 +valor2) % 2 == 0)
            {
                var result = new ObjectResult(new { Resposta = "Par" });
                result.StatusCode = 200;
                return result;
            }
            else
            {
                var result = new ObjectResult(new { Resposta = "Impar" });
                result.StatusCode = 201;
                return result;
            }
        }
        [HttpPost("Exercicio2")]
        public IActionResult Exercicio2(string valor)
        {
            try
            {
                int num_var = Int32.Parse(valor);
                var result = new ObjectResult(new { Resposta = "Conversão Realizada" });
                result.StatusCode = 202;
                return result;
            }
            catch (FormatException)
            {
                var result = new ObjectResult(new { Resposta = "Conversão Impossível" });
                result.StatusCode = 409;
                return result;
            }
        }
        [HttpPost("Exercicio3")]
        public IActionResult Exercicio3(int valor)
        {
            var result = new ObjectResult(new { Resposta = "Código não encontrado" });
            result.StatusCode = 203;
           if (valor >= 500 && valor <= 599)
            {
                if (((HttpStatusCode)valor).ToString()!=valor.ToString())
                    return Ok(new { descricao = ((HttpStatusCode)valor).ToString() });
                else
                    return result;
            }
            else
            {
                return result;
            }
        }
        [HttpGet("Exercicio456")]
        public ActionResult ExemploApliKeyController([FromHeader(Name = "apikey")][Required] string requiredHeader)
        {
            
            return Ok("API KEY É VÁLIDA : "+ requiredHeader);
        }

 
    }
}
