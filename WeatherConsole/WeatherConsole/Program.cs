namespace WeatherConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WeatherApp app = new WeatherApp();
            app.Process(args);
        }
    }
}
