using System.Net;

namespace LibrarySystem.Exceptions;

public class ApiException(
        string englishDescription,
        string arabicDescription,
        HttpStatusCode statusCode,
        string errorCode)
    : Exception(englishDescription)
{
    public string ArabicDescription { get; set; } = arabicDescription;
    public string EnglishDescription { get; set; } = englishDescription;
    public HttpStatusCode StatusCode { get; set; } = statusCode;
    public string ErrorCode { get; set; } = errorCode;
}

public static class ExceptionFactory
{
    public static ApiException BadRequestException()
        => new("Bad request", "طلب غير صالح", HttpStatusCode.BadRequest, "E0001");

    public static ApiException EntityNotFoundException()
        => new("Entity not found", "العنصر غير متوفر", HttpStatusCode.NotFound, "E0002");

    public static ApiException NotAcceptableActionException()
        => new("Not acceptable action or request", "عملية أو طلب غير مقبولين", HttpStatusCode.NotAcceptable, "E0003");
}

