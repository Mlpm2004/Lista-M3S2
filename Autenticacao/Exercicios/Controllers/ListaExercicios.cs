using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Exercicios.Estatico;
using Microsoft.AspNetCore.Authorization;

namespace Exercicios.Controllers
{
    /// <summary>
    ///  Microsoft.AspNetCore.Http.StatusCode
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ListaExerciciosController : ControllerBase
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

        [HttpPost("Exercicio7")]
        public ActionResult<string> Exercicio7(string usuario, string senha)
        {
            if (!usuario.ToLower().Equals("admin") && !usuario.ToLower().Equals("contador"))
            {
                return Ok("Erro no campo usuário");
            }

            if (usuario.ToLower().Equals("admin") && !senha.ToLower().Equals("123456"))
            {
                return Ok("Erro no campo senha");
            }
            if (usuario.ToLower().Equals("contador") && !senha.ToLower().Equals("987654"))
            {
                return Ok("Erro no campo senha");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", usuario) ,
                                                               new Claim(ClaimTypes.Role, "Administrador")}),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = JwtConfiguracao.Issuer,
                Audience = JwtConfiguracao.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(JwtConfiguracao.Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
        // Exercicio8
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult<string> AutenticadoSucessoAdmin()
        {
            return Ok("Autencição feita com sucesso");
        }

        [HttpPost("Exercicio8")]
        public ActionResult<string> AcessoAdministrativo(string nome, string usuario, string senha)
        {
            if (usuario.ToLower().Equals("contador") && senha.ToLower().Equals("987654"))
            {
                var result = new ObjectResult(new { Resposta = "Perfil sem acesso a esta área" });
                result.StatusCode = 403;
                return result;
            }
            if (!usuario.ToLower().Equals("admin") )
            {
                return Ok("Erro no campo usuário");
            }
            if (usuario.ToLower().Equals("admin") && !senha.ToLower().Equals("123456"))
            {
                return Ok("Erro no campo senha");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", usuario), new Claim(ClaimTypes.Role, "Administrador") }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = JwtConfiguracao.Issuer,
                Audience = JwtConfiguracao.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(JwtConfiguracao.Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok("Criado Usuário : "+nome);
        }

        // Exercicio9
       
        [HttpPost("Exercicio9")]
        public ActionResult<string> GerarImpostoDeRenda(string usuario, string senha)
        {
            if (usuario.ToLower().Equals("admin") && senha.ToLower().Equals("123456"))
            {
                {
                    var result = new ObjectResult(new { Resposta = "Perfil sem acesso a esta área" });
                    result.StatusCode = 403;
                    return result;
                }
            }
            if (!usuario.ToLower().Equals("contador"))
            {
                return Ok("Erro no campo usuário");
            }
            if (usuario.ToLower().Equals("contador") && !senha.ToLower().Equals("987654"))
            {
                return Ok("Erro no campo senha");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", usuario), new Claim(ClaimTypes.Role, "Contador") }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = JwtConfiguracao.Issuer,
                Audience = JwtConfiguracao.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(JwtConfiguracao.Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok("Valor : 1500.00");
        }
        [HttpGet]
        [Route("Exercicio10")]
        [AllowAnonymous]
        public ActionResult<string> LiberadoTodosPerfil()
        {
            return Ok("Acesso utilizado para todos os Perfis");
        }
    }
}
