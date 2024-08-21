namespace IronDome2.Middlewares.attack
{
    public class AttackLogInMiddleWare
    {
        private readonly RequestDelegate _next;

        public AttackLogInMiddleWare(RequestDelegate next)
        { this._next = next; }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            Console.WriteLine("inside AttackLogInMiddleWare");
            await this._next(context);
        }
    }
}
