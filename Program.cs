using Welcome;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

string pathWelcome = "Welcome.json";

WelcomePage welcome = WelcomePage.FromJson(pathWelcome);

app.MapControllers();

app.MapGet("/", () => welcome.Info());
app.Run();
