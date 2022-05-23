namespace Exercicios.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HEADER_API_KEY = "ApiKey";
        private const string SECRETS_API_KEY = "c7072035-fab3-4bcc-b590-167a91f905a7";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var pathUrl = context?.Request?.Path.Value?.ToLower();

            if (pathUrl!.Contains("apikeyexemplo")|| pathUrl!.Contains("listaexercicios/exercicio456"))
            {
                if (!context!.Request.Headers.TryGetValue(HEADER_API_KEY, out var apiKeyRequest))
                {
                    context.Response.StatusCode = 404;
                    context.Response.WriteAsync("Header da api key não encontrado");
                    return;
                }

                if (!SECRETS_API_KEY.Equals(apiKeyRequest))
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "text/plain";
                    context.Response.WriteAsync("API KEY NÃO É VALIDA");
                    return;
                }
            }

            await _next(context!);
        }
    }
}
