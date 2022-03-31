namespace UserMicroService.Domain
{
    public static class AppErrors
    {
        // User errors
        public static readonly IError UserNotFoundError = new IError(100, "User could not be found");
    }
}
