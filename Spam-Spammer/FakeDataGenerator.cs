using Bogus;

namespace PhishPoisoner;

public static class FakeDataGenerator
{
    private static readonly Faker Faker = new();

    public static (string email, string password) GenerateCredentials()
    {
        string email = Faker.Internet.Email();
        string password = Faker.Internet.Password(12, false, @"[A-Za-z0-9!@#$%^&*]");
        return (email, password);
    }
}