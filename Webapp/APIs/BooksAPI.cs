using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Webapp.APIs;

public class BooksAPI : IAPI
{
    public BooksAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public List<Book> GetAll()
    {
        var response = Get("/Books").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        List<Book>? result = JsonConvert.DeserializeObject<List<Book>>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido.");

        return result;
    }

    public void Create(Book book, List<int> authorIds)
    {
        var bookParams = new
        {
            Book = book,
            AuthorIds = authorIds
        };

        var response = Post("/Books", bookParams).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}
