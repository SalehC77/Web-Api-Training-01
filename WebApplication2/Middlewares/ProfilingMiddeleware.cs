using System.Diagnostics;

namespace WebApplication2.Middlewares
{
    public class ProfilingMiddeleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddeleware> _logger;

        public ProfilingMiddeleware( RequestDelegate next , ILogger<ProfilingMiddeleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await this._next(context);
            stopWatch.Stop();
            this._logger.LogInformation($"Request '{context.Request.Path}' Took '{stopWatch.ElapsedMilliseconds}ms' to execute");
        }





    }
}
