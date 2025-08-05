using Netwise;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static async Task Main(string[] args)
    {
        string txtFilePath = "D:\\Własne Projekty\\RecruitmentTask\\Netwise\\cat_facts.txt";

        FactService service = new FactService(new HttpClient());
        TxtFileHandler TxtFileHandler = new TxtFileHandler(txtFilePath);

        while (true)
        {
            Console.WriteLine("1-Connect 2-Display file 3-Exit");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CatFact catFact = await service.GetFact();
                    TxtFileHandler.SaveFact(catFact);
                    break;
                case "2":
                    TxtFileHandler.DisplayFile();
                    break;
                case "3":
                    Console.WriteLine("Exiting the application.");
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    continue;
            }
        }
    }
}