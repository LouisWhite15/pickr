namespace Selections.Domain.Exceptions;

public class SelectionsDomainException : Exception
{
    public SelectionsDomainException()
    {
    }

    public SelectionsDomainException(string message)
        : base(message)
    {
    }

    public SelectionsDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}