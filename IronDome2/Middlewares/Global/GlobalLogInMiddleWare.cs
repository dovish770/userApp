﻿namespace IronDome2.Middlewares.Global
{
    public class GlobalLoginMiddleWare
    {
        private readonly RequestDelegate _next;
        public GlobalLoginMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            Console.WriteLine($"Got Reqest to server: {request.Method}{request.Path} " +
                              $"From IP: {request.HttpContext.Connection.RemoteIpAddress}");
            await this._next(context);
        }
    }
}
