namespace PostMicroService.Domain
{
    public static class AppErrors
    {
        // Post errors
        public static readonly IError PostNotFoundError = new IError(100, "Post could not be found");
    }
}
