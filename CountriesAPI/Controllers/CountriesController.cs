using CountriesAPI.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CountriesAPI.Controllers;

[Route("/countries")]
public class CountriesController : ControllerBase
{
    private readonly ICountriesClient _client;

    public CountriesController(ICountriesClient client)
    {
        _client = client;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountries(string filter, string sort, int limit, int offset) 
    {
        var countries = await _client.GetAll();
        return Ok();
    }
}
