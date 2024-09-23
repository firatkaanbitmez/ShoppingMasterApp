public class PhoneNumber
{
    public string CountryCode { get; private set; }
    public string Number { get; private set; }

    private PhoneNumber() { }

    public PhoneNumber(string countryCode, string number)
    {
        ValidatePhoneNumber(countryCode, number);

        CountryCode = countryCode;
        Number = number;
    }

    private void ValidatePhoneNumber(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            throw new ArgumentException("Country Code is required.");
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone Number is required.");

        if (!number.StartsWith("0") && number.Length != 10)
        {
            throw new ArgumentException("Phone number must be 10 digits without leading 0.");
        }
    }

    public string GetFullNumber()
    {
        // Telefon numarasını uluslararası formata çevir
        return $"{CountryCode}{Number}";
    }

    public static PhoneNumber Create(string countryCode, string number)
    {
        return new PhoneNumber(countryCode, number);
    }
}
