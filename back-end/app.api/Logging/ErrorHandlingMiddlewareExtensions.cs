using Microsoft.AspNetCore.Builder;

namespace app.api.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class ErrorLoggingMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}