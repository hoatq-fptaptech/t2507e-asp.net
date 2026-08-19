using Microsoft.Azure.Functions.Worker;

namespace Company.FunctionApp1;

public class ProcessDemoFunction
{
    [Function("ProcessDemoFunction")]
    public void Run(
        [RabbitMQTrigger("demo-queue",
            ConnectionStringSetting = "RabbitMQConnectionString")]
        string message)
    {
        Console.WriteLine(message);
    }
}