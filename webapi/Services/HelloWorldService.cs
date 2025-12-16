namespace webapi.Services
{
    public class HelloWorldService : IHelloWorldService
    {
        public string GetHelloWorld() => "Hello World";
    }

    public interface IHelloWorldService
    {
        string GetHelloWorld();
    }
}
