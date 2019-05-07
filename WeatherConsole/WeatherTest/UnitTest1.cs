using NUnit.Framework;
using WeatherConsole;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            WeatherApp app = new WeatherApp();
            Assert.AreEqual(app.Process(new string[] { }), "File already processed");
        }

        [Test]
        public void Test2()
        {
            WeatherApp app = new WeatherApp();
            Assert.AreEqual(app.Process(new string[] { "07052019_input.txt" }), "File Processed");
        }

        [Test]
        public void Test3()
        {
            WeatherApp app = new WeatherApp();
            Assert.AreEqual(app.Process(new string[] { "07052019_input" }), "File not found");
        }

        [Test]
        public void Test4()
        {
            WeatherApp app = new WeatherApp();
            Assert.AreEqual(app.Process(new string[] { "garbage" }), "File not found");
        }

        [Test]
        public void Test5()
        {
            WeatherApp app = new WeatherApp();
            Assert.AreEqual(app.Process(new string[0] {  }), "File already processed");
        }
    }
}