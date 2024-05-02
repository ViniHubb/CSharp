// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;

static class Program
{
    static void Main()
    {
        Console.WriteLine("##################################  C#  ##################################\n");
        int    num = 0;
        bool[] keys = {true, false, true, false, true, true, false};
        float  money;
        string text = "Grande";

        if (string.Equals(text, "grande", StringComparison.OrdinalIgnoreCase) && false || text == "grande"){
            num++;
        } else if(false || true && false){
            num+=num;
        }
        num = true ? 1 : 2;

        Console.Write("Escolha um numero: ");
        //text = Console.ReadLine();
        keys[0] = int.TryParse(text, out num);
        Console.WriteLine($"{keys[0]} -> {num}");
        text = num.ToString();
        Console.WriteLine($"Você escolheu o numero {num}");

        for (int i = 1; i <= num; i++){
            Console.Write($"[{i}] ");
        }

        try{
            Console.WriteLine("\n" + string.Join(' ', keys));
            text = null;
            num = text.Length;
        }
        catch (Exception exception) {
            Console.WriteLine("\n ERRO: " + exception.Message);
        }
        

        Console.WriteLine("\n##########################################################################\n");

        ILogger logger = new FileLogger("mylog.txt");
        Bank account1 = new Bank("Vini", 9999, logger);
        Bank account2 = new Bank("Ela", 8888, logger);

        List<Bank> accounts = new List<Bank>()
        {
            account1,
            account2,
        };

        foreach (Bank account in accounts)
        {
            Console.WriteLine(account.Balance);
        }

        DataStore<int> store = new DataStore<int>();
        store.Value = 33;
        Console.WriteLine(store.Value);

        Bank account3 = new Bank("Amanda", 7777, logger);
        string json = JsonSerializer.Serialize(account3);
        Console.WriteLine(json);

        Console.WriteLine("\n##########################################################################\n");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Testando...");
        Console.ResetColor();
        Console.WriteLine("Testando...");

        "Testando 2...".WriteLine(ConsoleColor.Yellow);
        Console.WriteLine("Testando 2...");

        Console.WriteLine("\n##########################################################################\n");

        int[] numbers = {1,3,5,7,9,11,13,15,17,19};

        var query = from number in numbers
                    where number < 10
                    select number;
        
        var results = query.ToList();

        Console.WriteLine("Lenth -> " + results.Count());

        foreach(int number in results)
        {
            Console.Write($"[{number}]");
        }

        Console.WriteLine("\n\n##########################################################################\n");
        
        var range = Enumerable.Range(0, 26).Select(number => (char)('A' + number));
        foreach (var letra in range)
        {
            Console.Write($"{letra} ");
            Thread.Sleep(100);
        }
        foreach (var letra in range)
        {
            
            var task = Task.Run(() => 
            {
                Thread.Sleep(100);
                Console.Write($"{letra} ");
            });
        }
        Console.ReadLine();

        Console.WriteLine("\n\n##########################################################################\n");
    }
}
static class Extensions
{
    public static void WriteLine(this string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
class DataStore<T>
{
    public T Value { get; set; }
}
class FileLogger : ILogger
{
    private readonly string filePath;
    public FileLogger(string filePath)
    {
        this.filePath = filePath;
    }
    public void Log(string message)
    {
        File.AppendAllText(filePath, message+"\n");
    }
}
class ConsoleLogger : ILogger
{
}
interface ILogger
{
    void Log(string message)
    {
        Console.WriteLine("LOGGER: " + message);
    }
}

class Bank
{
    public string Name { 
        get;
        private set;
    }
    private readonly ILogger logger;
    public decimal Balance { 
        get;
        private set;
    }
    public Bank(string name, decimal balance, ILogger logger)
    {
        if(string.IsNullOrWhiteSpace(name)){
            throw new Exception("Nome invalido");
        }
        if(balance < 0){
            throw new Exception("Saldo negativo invalido");
        }
        Name = name;
        Balance = balance;
        this.logger = logger;
    }
    public void Deposit(decimal amount)
    {  
        if(amount <= 0){
            logger.Log("O deposito deve ser um valor maior que ZERO");
            return;
        }
        Balance += amount;
    }
}