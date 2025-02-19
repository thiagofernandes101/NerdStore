﻿namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record ProductDescription
    {
        public string Value { get; }
        private ProductDescription(string value) => Value = value;
        public static ProductDescription Create(string value) => new(value);
        public override string ToString() => Value;
    }
}