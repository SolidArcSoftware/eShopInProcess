using EShop.Domain.SharedKernel.ValueObjects;

namespace EShop.Domain.Ordering.ValueObjects
{
    public sealed class AddressValueObject : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public AddressValueObject(string street, string city, string state, string country, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street cannot be empty.", nameof(street));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be empty.", nameof(city));
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State cannot be empty.", nameof(state));
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country cannot be empty.", nameof(country));
            if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException("ZipCode cannot be empty.", nameof(zipCode));

            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}