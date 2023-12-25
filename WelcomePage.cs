using System.Text.Json;

namespace Welcome
{
    public class WelcomePage
    {
        public string main { get; set; }

        public string Info()
        {
            return (this.main);
        }
        public static WelcomePage FromJson(string pathWelcome)
        {
            string jsonString = File.ReadAllText(pathWelcome);
            WelcomePage page = JsonSerializer.Deserialize<WelcomePage>(jsonString);
            return page;
        }
    }
}
