namespace PostService.Domain
{
    public static class AppErrors
    {
        // Post Errors
        public static readonly IError PostNotNound = new IError(100, "Post could not be found");
    }
}
