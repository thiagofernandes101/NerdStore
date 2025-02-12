namespace NerdStore.Core.Patterns
{
    public class Option<T>
    {
        private readonly T _content;

        public bool HasValue { get; }
        public T Content => HasValue ? _content : throw new InvalidOperationException("No value present");

        private Option() => HasValue = false;

        private Option(T value)
        {
            _content = value ?? throw new ArgumentNullException(nameof(value));
            HasValue = true;
        }

        public static Option<T> None() => new Option<T>();

        public static Option<T> Some(T value) => new Option<T>(value);

        public T GetValueOrDefault(T defaultValue = default) => HasValue ? _content : defaultValue;

        public override string ToString() => HasValue ? _content.ToString() : "No value";
    }

}
