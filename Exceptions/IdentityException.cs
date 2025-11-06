namespace MinhaSaudeFeminina.Exceptions
{
    public class IdentityException : Exception
    {
        public List<string> Errors { get; }

        public IdentityException(IEnumerable<string> errors)
            :base("Falha na criação de usuário.")
        {
            Errors = errors.ToList();
        }
    }
}
