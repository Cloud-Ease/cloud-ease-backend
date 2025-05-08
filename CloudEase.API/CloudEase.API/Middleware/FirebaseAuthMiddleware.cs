using CloudEase.API.Services;

namespace CloudEase.API.Middleware
{
    public class FirebaseAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public FirebaseAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, FirebaseAuthService authService)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var headerValue))
            {
                var token = headerValue.ToString().Replace("Bearer ", "");
                var userId = await authService.VerifyToken(token);

                if (userId != null)
                {
                    context.Items["UserId"] = userId;
                    await _next(context);
                    return;
                }
            }

            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
        }
    }
}
