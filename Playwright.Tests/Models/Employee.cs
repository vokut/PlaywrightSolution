using Bogus;

namespace Playwright.Tests.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }

        public Employee()
        {
            var faker = new Faker();
            EmployeeId = faker.Random.Int(10000, 99999).ToString();
            FirstName = faker.Name.FirstName();
            MiddleName = faker.Name.FirstName();
            LastName = faker.Name.LastName();
        }
    }
}
