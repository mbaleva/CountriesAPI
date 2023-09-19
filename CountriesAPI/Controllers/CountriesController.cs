using CountriesAPI.Clients;
using CountriesAPI.Constants;
using CountriesAPI.Helpers;
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
    public async Task<IActionResult> GetCountries(string? filter, string? sort, int? limit, int? offset) 
    {
        var countries = await _client.GetAll();

        if (!string.IsNullOrEmpty(filter))
        {
            var areBothConditionsPresented = filter.Contains("and");

            if (areBothConditionsPresented)
            {
                var filters = FilteringHelper.ExtractNameAndPopulationFromFilter(filter);

                countries = FilterByName(countries, filters.Name);
                countries = FilterByPopulation(countries, filters.Population);
            }

            if (filter.StartsWith("contains"))
            {
                var filterParams = filter.Replace("(", " ")
                    .Replace(")", " ")
                    .Replace(",", string.Empty)
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                countries = FilterByName(countries, filterParams[filterParams.Length - 1]); 
            }

            if (filter.StartsWith("lessThan"))
            {
                var filterParams = filter.Replace("(", " ")
                    .Replace(")", " ")
                    .Replace(",", string.Empty)
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                countries = FilterByPopulation(countries, filterParams[filterParams.Length - 1]);
            }
        }

        if (!string.IsNullOrEmpty(sort) && (sort == SortConstants.Ascending || sort == SortConstants.Descending))
        {
            countries = SortByName(countries, sort);
        }

        if (limit.HasValue)
        {
            countries = GetFirstNRecords(countries, limit.Value, offset);
        }

        return Ok(countries.Select(x => x.Name.Common));
    }

    public IEnumerable<Country> FilterByName(IEnumerable<Country> countries, string filter) 
    {
        return countries.Where(x => x.Name.Common.ToLower().Contains(filter.ToLower()));
    }

    public IEnumerable<Country> FilterByPopulation(IEnumerable<Country> countries, string filter) 
    {
        var populationToMlns = long.Parse(filter + "000000");

        return countries.Where(x => x.Population < populationToMlns);
    }

    public IOrderedEnumerable<Country> SortByName(IEnumerable<Country> countries, string sort) 
    {
        if (sort.ToLower() == SortConstants.Ascending)
        {
            return countries.OrderBy(x => x.Name.Common);
        }
        return countries.OrderByDescending(x => x.Name.Common);
    }

    public IEnumerable<Country> GetFirstNRecords(IEnumerable<Country> countries, int limit, int? offset) 
    {
        if (offset.HasValue)
        {
            return countries.Skip(offset.Value)
                .Take(limit);
        }

        return countries.Take(limit);
    }
}
