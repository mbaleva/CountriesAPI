using CountriesAPI.Models;

namespace CountriesAPI.Helpers;

public static class FilteringHelper
{
    public static FilteringParameters ExtractNameAndPopulationFromFilter(string filter) 
    {
        var filterParams = filter.Replace("and", string.Empty)
                    .Replace("(", " ")
                    .Replace(")", " ")
                    .Replace(",", string.Empty)
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var populationIndex = Array.FindIndex(filterParams, x => x.ToLower() == "population") + 1;

        if (populationIndex <= -1 || populationIndex > filterParams.Length)
        {
            throw new InvalidOperationException("Invalid filter");
        }

        var populationToFilterBy = filterParams[populationIndex];

        var nameIndex = Array.FindIndex(filterParams, x => x.ToLower() == "name") + 1;

        if (nameIndex <= -1 || nameIndex > filterParams.Length)
        {
            throw new InvalidOperationException("Invalid filter");
        }

        var nameToFilterBy = filterParams[nameIndex];

        return new FilteringParameters 
        {
            Name = nameToFilterBy,
            Population = populationToFilterBy
        };
    }
}
