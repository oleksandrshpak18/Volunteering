namespace Volunteering.Helpers
{
    public class Response<T>
    {
        public T? Data { get; set; } // Holds the data if successful
        public string? Error { get; set; } // Holds the error message, if any
        public bool IsSuccess => Data != null; // Indicates success or failure based on data presence
    }

}
