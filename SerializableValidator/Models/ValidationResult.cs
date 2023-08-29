namespace SerializableValidator.Models
{
    public record ValidationResult(
        bool IsValid,
        List<string> Errors
    )
    {
        public bool IsValid { get; private set; } = IsValid;
        public List<string> Errors { get; private set; } = Errors;
        public ValidationResult() : this(true, new List<string>()) { }


        public void AddError(string error)
        {
            if (error == null)
                return;

            IsValid = false;
            Errors.Add(error);
        }

        public void AddError(string error, object? arg0)
        {
            if (error == null)
                return;

            IsValid = false;
            Errors.Add(string.Format(error, arg0));
        }

        public static ValidationResult operator +(ValidationResult a, ValidationResult b)
        {
            var errors = new List<string>(a.Errors.Count + b.Errors.Count);
            errors.AddRange(a.Errors);
            errors.AddRange(b.Errors);
            var isValid = a.IsValid && b.IsValid;

            return new ValidationResult(isValid, errors);
        }
    }
}
