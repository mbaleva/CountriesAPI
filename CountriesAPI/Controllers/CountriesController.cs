using CountriesAPI.Clients;
using CountriesAPI.Constants;
using CountriesAPI.Models;
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

    private IEnumerable<Country> FilterByName(IEnumerable<Country> countries, string filter) 
    {
        return countries.Where(x => x.Name.Common.ToLower().Contains(filter.ToLower()));
    }

    private IEnumerable<Country> FilterByPopulation(IEnumerable<Country> countries, string filter) 
    {
        var populationToMlns = long.Parse(filter + "000000");

        return countries.Where(x => x.Population < populationToMlns);
    }

    private IOrderedEnumerable<Country> SortByName(IEnumerable<Country> countries, string sort) 
    {
        if (sort.ToLower() == SortConstants.Ascending)
        {
            return countries.OrderBy(x => x.Name.Common);
        }
        return countries.OrderByDescending(x => x.Name.Common);
    }

    private IEnumerable<Country> GetFirstNRecords(IEnumerable<Country> countries, int limit, int? offset) 
    {
        if (offset.HasValue)
        {
            return countries.Skip(offset.Value)
                .Take(limit);
        }

        return countries.Take(limit);
    }
}
