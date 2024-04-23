namespace Volunteering.Helpers
{
    public class AuthResult
    {
        public string ?Token {  get; set; }
        public bool Result { get; set; }
        public List<string>? Messages { get; set; }
    }
}
