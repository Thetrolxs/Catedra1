
using Bogus;
using Catedra1.src.Models;

namespace Catedra1.src.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();

                if(!context.Genders.Any())
                {
                    context.Genders.AddRange(
                        new Gender { Type = "masculino"},
                        new Gender { Type = "femenino"},
                        new Gender { Type = "otro"},
                        new Gender { Type = "prefiero no decirlo"}
                    );
                }

                context.SaveChanges();

                var generatedRuts = new HashSet<string>();

                var faker = new Faker<User>()
                .RuleFor(u => u.Rut, f => GenerateUniqueRandomRut(generatedRuts))
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.GenderId, f => f.Random.Number(1,4))
                .RuleFor(u => u.Birthday, f => f.Date.Past(30, DateTime.Now.AddYears(-18)));

                var users = faker.Generate(20);

                context.Users.AddRange(users);

                context.SaveChanges();   
            }
        }
    private static string GenerateUniqueRandomRut(HashSet<string> existingRuts)
    {
        string rut;
        do
        {
            rut = GenerateRandomRut();
        } while (existingRuts.Contains(rut));

        existingRuts.Add(rut);
        return rut;
    }

    private static string GenerateRandomRut()
    {
        Random random = new();
        int rutNumber = random.Next(1, 99999999); // Número aleatorio de 7 o 8 dígitos
        int verificator = CalculateRutVerification(rutNumber);
        string verificatorStr = verificator.ToString();
        if(verificator == 10){
            verificatorStr = "k";
        }

        return $"{rutNumber}-{verificatorStr}";
    }

    // Método para calcular el dígito verificador de un RUT
    private static int CalculateRutVerification(int rutNumber)
    {
        int[] coefficients = { 2, 3, 4, 5, 6, 7 };
        int sum = 0;
        int index = 0;

        while (rutNumber != 0)
        {
            sum += rutNumber % 10 * coefficients[index];
            rutNumber /= 10;
            index = (index + 1) % 6;
        }

        int verification = 11 - (sum % 11);
        return verification == 11 ? 0 : verification;
    }
    }
}