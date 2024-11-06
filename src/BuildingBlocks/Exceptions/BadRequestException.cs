namespace BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string messge) : base(messge)
        { 
        }

        public BadRequestException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
