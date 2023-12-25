using BookProfile;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Controllers
{
    [Route("Library")]
    public class BooksController : Controller
    {
        [HttpGet]
        public IActionResult Welcome()
        {
            return Content("Добро пожаловать в нашу онлайн библиотеку!");
        }

        [HttpGet("Books")]
        public IActionResult DisplayBooks()
        {
            string booksData = System.IO.File.ReadAllText("./booksData.json");

            List<Book> bookList = JsonSerializer.Deserialize<List<Book>>(booksData);

            return Content("<style>" +
                "table {border-collapse: collapse; width: 100%;}" +
                "th, td {border: 1px solid #ddd; padding: 8px;}" +
                "tr:nth-child(even) {background-color: #f2f2f2;}" +
                "th {padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white;}" +
                "</style>" +
                "<table>" +
                "<tr><th>Название книги</th><th>Автор</th></tr>" +
                string.Join("", bookList.Select(bookItem => $"<tr><td>{bookItem.title}</td><td>{bookItem.author}</td></tr>")) +
                "</table>", "text/html; charset=utf-8");

        }
        [HttpGet("Profile/{id?}")]
        public IActionResult UserProfile(int? id)
        {
            var profileData = System.IO.File.ReadAllText("./userProfile.json");
            var userProfiles = JsonSerializer.Deserialize<Dictionary<string, Profile>>(profileData);

            if (id.HasValue && id >= 0 && id <= 5 && userProfiles.ContainsKey(id.ToString()))
            {
                var selectedProfile = userProfiles[id.ToString()];
                return Content("<style>" +
                    "table {border-collapse: collapse; width: 100%;}" +
                    "th, td {border: 1px solid #ddd; padding: 8px;}" +
                    "tr:nth-child(even) {background-color: #f2f2f2;}" +
                    "th {padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 10%}" +
                    "</style>" +
                    "<table>" +
                    $"<tr><th>ID</th><td>{selectedProfile.id}</td></tr>" +
                    $"<tr><th>Name</th><td>{selectedProfile.name}</td></tr>" +
                    $"<tr><th>Age</th><td>{selectedProfile.age}</td></tr>" +
                    "</table>", "text/html; charset=utf-8");
            }
            else if (userProfiles.ContainsKey("Current"))
            {
                var currentProfile = userProfiles["Current"];
                return Content("<style>" +
                    "table {border-collapse: collapse; width: 100%;}" +
                    "th, td {border: 1px solid #ddd; padding: 8px;}" +
                    "tr:nth-child(even) {background-color: #f2f2f2;}" +
                    "th {padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 10%}" +
                    "</style>" +
                    "<table>" +
                    $"<tr><th>ID</th><td>{currentProfile.id}</td></tr>" +
                    $"<tr><th>Name</th><td>{currentProfile.name}</td></tr>" +
                    $"<tr><th>Age</th><td>{currentProfile.age}</td></tr>" +
                    "</table>", "text/html; charset=utf-8");
            }
            else
            {
                return Content("Информации о пользователе отсутствует", "text/html; charset=utf-8");
            }
        }

    }
}
