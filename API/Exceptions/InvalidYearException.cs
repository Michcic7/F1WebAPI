namespace API.Exceptions;

public class InvalidYearException : Exception
{
	public InvalidYearException()
	{
	}

	public InvalidYearException(string message)
		: base(message)
	{
	}

	public InvalidYearException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
