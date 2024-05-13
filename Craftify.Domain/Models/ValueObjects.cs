using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Models
{
    public abstract class ValueObjects : IEquatable<ValueObjects>
    {
        public abstract IEnumerable<object> GetEqulityComponents();

        public override bool Equals(object? obj)
        {
            if(obj is null || obj.GetType() != GetType()) return false;

            var valueObject = (ValueObjects)obj;

            return GetEqulityComponents()
                .SequenceEqual(valueObject.GetEqulityComponents());
        }

        public static bool operator ==(ValueObjects left, ValueObjects right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(ValueObjects left, ValueObjects right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return GetEqulityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObjects? other)
        {
            return Equals((object?) other);
        }

    }

    

    public class Price(decimal amount,string currency) : ValueObjects
    {
        public decimal Amount { get; private set; } = amount;

        public string Currency { get; private set;} = currency;

        public override IEnumerable<object> GetEqulityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
