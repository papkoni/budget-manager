namespace UserService.Application.Exceptions;

public class InvalidTokenException(string message) : Exception(message);