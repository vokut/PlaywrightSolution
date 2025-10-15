using Bogus;

namespace Playwright.Core.Models
{
    public class Candidate
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Keywords { get; set; }
        public int RandomNumber { get; set; }

        public Candidate()
        {
            var faker = new Faker();
            RandomNumber = faker.Random.Int(10000, 99999);
            FirstName = faker.Name.FirstName();
            MiddleName = faker.Name.FirstName();
            LastName = faker.Name.LastName();
            Email = $"{FirstName.ToLower()}.{LastName.ToLower()}{RandomNumber}@test.com";
            PhoneNumber = faker.Phone.PhoneNumber("+359 ### ### ###");
            Keywords = faker.Random.AlphaNumeric(100);
        }

        public Candidate(string firstName, string lastName)
        {
            var faker = new Faker();
            RandomNumber = faker.Random.Int(10000, 99999);
            FirstName = firstName;
            MiddleName = faker.Name.FirstName();
            LastName = lastName;
            Email = $"{firstName.ToLower()}.{lastName.ToLower()}{RandomNumber}@test.com";
            PhoneNumber = faker.Phone.PhoneNumber("+359 ### ### ###");
            Keywords = faker.Random.AlphaNumeric(100);
        }
    }
}
