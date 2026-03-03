namespace Domain.Exceptions
{
    public class InputException : Exception
    {
        public List<string> Errors { get; }

        public InputException(List<string> errors) : base("Um ou mais erros de validação ocorreram.")
        {
            Errors = errors;
        }
    }
}
