namespace Pk.Com.Jazz.ECP.Utilities
{
    public class ResponseEdit
    {
        private readonly RequestDelegate next;

        public ResponseEdit(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //To add Headers AFTER everything you need to do this
            context.Response.OnStarting(state => {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("Server","M-UNNOWKN");

                return Task.CompletedTask;
            }, context);

            // Call the next delegate/middleware in the pipeline
            await next(context);
        }

       
    }
}
