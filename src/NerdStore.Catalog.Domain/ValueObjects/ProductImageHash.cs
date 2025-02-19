﻿namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record ProductImageHash
    {
        public string Value { get; }
        private ProductImageHash(string value) => Value = value;
        public static ProductImageHash Create(string value) => new(value);
        public override string ToString() => Value;
    }
}