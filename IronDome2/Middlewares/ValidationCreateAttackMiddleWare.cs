using IronDome2.Middlewares.attack;
using System.Text.Json;

namespace IronDome2.Middlewares
{
    public class ValidationCreateAttackMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _words = new List<string>
            {
            "origine",
            "name"
            };


        public ValidationCreateAttackMiddleWare(RequestDelegate next)
        { this._next = next; }

        //public async Task Validatation(HttpContext httpContext)
        //{
        //    var request = httpContext.request;
        //    string body = GetBodyAsync(request.body);
        //    if (!string.IsNullOrEmpty(body))
        //    {
        //        var document = JsonDocument.Parse(body);
        //        if (!document.RootElement.TryGetProperty)






        //            var val1 = context(x => x == _words[0]);
        //    var val2 = context(x => x == _words[1]);
        //    if (val1 && val2)
        //    {
        //        await this._next(context);
        //    }
        //    Console.WriteLine("invalid attack!");
        //}

        //private string GetBodyAsync() 
        //{ 
        //    return "";
        //}
    }
}
