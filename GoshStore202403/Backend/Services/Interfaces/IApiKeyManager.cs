namespace Backend.Services.Interfaces
{
    public interface IApiKeyManager
    {
        Task InvokeAsync (HttpContext context);
    }
}
