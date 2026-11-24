namespace AseanPhonebook;

/// <summary>One phonebook entry: a student and their contact details.</summary>
public class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public char Gender { get; set; }          // 'M' or 'F'
    public int CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(string studentNumber, string surname, string firstName, string occupation,
                   char gender, int countryCode, string areaCode, string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = char.ToUpper(gender);
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    private string Pronoun => Gender == 'M' ? "His" : "Her";

    /// <summary>Full number in country-area-number form, e.g. 63-2-4567890.</summary>
    public string FullPhone => $"{CountryCode}-{AreaCode}-{PhoneNumber}";

    /// <summary>Text shown in the edit menu.</summary>
    public string DescribeForEdit() =>
        $"{FirstName} {Surname} is a {Occupation}. {Pronoun} number is {FullPhone}";

    /// <summary>Text shown in search results.</summary>
    public string DescribeForSearch() =>
        $"{Surname}, {FirstName}, with student number {StudentNumber}, is a {Occupation}. " +
        $"{Pronoun} phone number is {FullPhone}";
}