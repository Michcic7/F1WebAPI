namespace API.Exceptions;

public class InvalidNameException : Exception
{
	public InvalidNameException()
	{
	}

	public InvalidNameException(string message)
		: base(message)
	{
	}

	public InvalidNameException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
