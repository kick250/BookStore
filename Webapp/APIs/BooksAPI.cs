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
}
