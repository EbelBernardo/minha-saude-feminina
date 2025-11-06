using System.Collections;

namespace MinhaSaudeFeminina.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("Falha na validação de dados")
        {
            Errors = errors.ToList();
        }
    }
}
