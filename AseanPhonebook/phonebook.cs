namespace AseanPhonebook;

/// <summary>Stores all students in memory (no size limit) and handles lookup/search.</summary>
public class Phonebook
{
    private readonly List<Student> _students = new();

    public void Add(Student student) => _students.Add(student);

    public Student? FindByStudentNumber(string studentNumber) =>
        _students.FirstOrDefault(s =>
            s.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));

    public bool StudentNumberExists(string studentNumber) =>
        FindByStudentNumber(studentNumber) != null;

    /// <summary>Returns students whose country code is in the list, sorted by surname.</summary>
    public List<Student> SearchByCountryCodes(IEnumerable<int> countryCodes)
    {
        var codes = new HashSet<int>(countryCodes);
        return _students
            .Where(s => codes.Contains(s.CountryCode))
            .OrderBy(s => s.Surname, StringComparer.OrdinalIgnoreCase)
            .ThenBy(s => s.FirstName, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}