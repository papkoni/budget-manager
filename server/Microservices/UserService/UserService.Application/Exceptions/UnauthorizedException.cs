namespace UserService.Application.Exceptions;

public class UnauthorizedException(string message) : Exception(message);