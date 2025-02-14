namespace UserService.Application.Exceptions;

public class BadRequestException(string message) : Exception(message);