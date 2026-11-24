namespace AseanPhonebook;

/// <summary>Represents one supported ASEAN country and its dialing code.</summary>
public class Country
{
    public string Name { get; }
    public int Code { get; }

    public Country(string name, int code)
    {
        Name = name;
        Code = code;
    }
}

/// <summary>Holds the five supported countries. Order matches the search menu.</summary>
public static class CountryRegistry
{
    public static readonly List<Country> All = new()
    {
        new Country("Philippines", 63),  // [1]
        new Country("Thailand",    66),  // [2]
        new Country("Singapore",   65),  // [3]
        new Country("Indonesia",   62),  // [4]
        new Country("Malaysia",    60)   // [5]
    };

    public static bool IsValidCode(int code) => All.Any(c => c.Code == code);

    public static Country? FindByCode(int code) => All.FirstOrDefault(c => c.Code == code);
}