namespace MinhaSaudeFeminina.Exceptions
{
    public class BusinessException : Exception
    {
        public List<string> Errors { get; set; }

        public BusinessException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public BusinessException(IEnumerable<string> errors, string message) : base(message)
        {
            Errors = errors.ToList();
        } 
    }
}
