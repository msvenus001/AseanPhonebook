namespace AseanPhonebook;

/// <summary>Controls the menus and connects user input to the Phonebook.</summary>
public class PhonebookApp
{
    private readonly Phonebook _phonebook = new();

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");
            Console.Write("Enter choice: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": StoreEntries(); break;
                case "2": EditEntry(); break;
                case "3": SearchByCountry(); break;
                case "4": running = false; break;
                default: break; // invalid option: menu simply shows again
            }
        }
    }

    // ---------- [1] STORE ----------
    private void StoreEntries()
    {
        bool another;
        do
        {
            Console.WriteLine();
            string number;
            while (true)
            {
                number = InputHelper.ReadNonEmpty("Enter student number: ");
                if (!_phonebook.StudentNumberExists(number)) break;
                Console.WriteLine("That student number already exists. Enter a different one.");
            }

            string surname = InputHelper.ReadNonEmpty("Enter surname: ");
            string firstName = InputHelper.ReadNonEmpty("Enter first name: ");
            string occupation = InputHelper.ReadNonEmpty("Enter occupation: ");
            char gender = InputHelper.ReadGender("Enter gender (M for male, F for female): ");
            int countryCode = InputHelper.ReadCountryCode("Enter country code: ");
            string areaCode = InputHelper.ReadDigits("Enter area code: ");
            string phone = InputHelper.ReadDigits("Enter number: ");

            _phonebook.Add(new Student(number, surname, firstName, occupation,
                                       gender, countryCode, areaCode, phone));
            Console.WriteLine("Entry saved.");

            another = InputHelper.ReadYesNo("Do you want to enter another entry [Y/N]? ");
        } while (another);
    }

    // ---------- [2] EDIT ----------
    private void EditEntry()
    {
        Console.WriteLine();
        string number = InputHelper.ReadNonEmpty("Enter student number: ");
        Student? student = _phonebook.FindByStudentNumber(number);

        if (student == null)
        {
            Console.WriteLine("Error: student number does not exist.");
            return;
        }

        bool editing = true;
        while (editing)
        {
            Console.WriteLine();
            Console.WriteLine($"Here is the existing information about {student.StudentNumber}:");
            Console.WriteLine(student.DescribeForEdit());
            Console.WriteLine();
            Console.WriteLine("Which of the following information do you wish to change?");
            Console.WriteLine("[1] Student number [2] Surname [3] Gender [4] Occupation");
            Console.WriteLine("[5] Country code [6] Area code [7] Phone number");
            Console.WriteLine("[8] None - Go back to main menu");

            switch (InputHelper.ReadInt("Enter choice: "))
            {
                case 1:
                    while (true)
                    {
                        string newNumber = InputHelper.ReadNonEmpty("Enter new student number: ");
                        bool taken = !newNumber.Equals(student.StudentNumber, StringComparison.OrdinalIgnoreCase)
                                     && _phonebook.StudentNumberExists(newNumber);
                        if (!taken) { student.StudentNumber = newNumber; break; }
                        Console.WriteLine("That student number already exists. Enter a different one.");
                    }
                    break;
                case 2: student.Surname = InputHelper.ReadNonEmpty("Enter new surname: "); break;
                case 3: student.Gender = InputHelper.ReadGender("Enter new gender (M/F): "); break;
                case 4: student.Occupation = InputHelper.ReadNonEmpty("Enter new occupation: "); break;
                case 5: student.CountryCode = InputHelper.ReadCountryCode("Enter new country code: "); break;
                case 6: student.AreaCode = InputHelper.ReadDigits("Enter new area code: "); break;
                case 7: student.PhoneNumber = InputHelper.ReadDigits("Enter new phone number: "); break;
                case 8: editing = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    // ---------- [3] SEARCH ----------
    private void SearchByCountry()
    {
        Console.WriteLine();
        Console.WriteLine("From which country:");
        Console.WriteLine("[1] Philippines [2] Thailand [3] Singapore [4] Indonesia");
        Console.WriteLine("[5] Malaysia [6] ALL [0] No More");

        var selected = new List<Country>();
        bool all = false;
        int n = 1;

        while (true)
        {
            int choice = InputHelper.ReadInt($"Enter choice {n}: ");

            if (choice == 0) break;

            if (choice == 6)
            {
                all = true;
                break;
            }

            if (choice >= 1 && choice <= 5)
            {
                Country country = CountryRegistry.All[choice - 1];
                if (!selected.Contains(country)) selected.Add(country);
                n++;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        if (all) selected = new List<Country>(CountryRegistry.All);

        if (selected.Count == 0)
        {
            Console.WriteLine("No countries selected.");
            return;
        }

        var results = _phonebook.SearchByCountryCodes(selected.Select(c => c.Code));

        Console.WriteLine();
        Console.WriteLine(all
            ? "Here are the students from all countries:"
            : $"Here are the students from {JoinNames(selected.Select(c => c.Name).ToList())}:");

        if (results.Count == 0)
        {
            Console.WriteLine("(No students found.)");
            return;
        }

        foreach (Student s in results)
            Console.WriteLine(s.DescribeForSearch());
    }

    private static string JoinNames(List<string> names)
    {
        if (names.Count == 1) return names[0];
        if (names.Count == 2) return $"{names[0]} and {names[1]}";
        return string.Join(", ", names.Take(names.Count - 1)) + " and " + names[^1];
    }
}