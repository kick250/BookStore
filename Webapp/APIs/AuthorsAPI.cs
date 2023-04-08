using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Webapp.APIs;

public class AuthorsAPI : IAPI
{
    public AuthorsAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public List<Author> GetAll()
    {
        var response = Get("/Authors").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        List<Author>? result = JsonConvert.DeserializeObject<List<Author>>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido.");

        return result;
    }

    public Author GetById(int id)
    {
        var response = Get($"/Authors/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        Author? result = JsonConvert.DeserializeObject<Author>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido.");

        return result;
    }

    public void Create(Author author)
    {
        var response = Post("/Authors", author).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}
