using CountriesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace CountriesAPI.Clients;

public interface ICountriesClient
{
    [Get("/all")]
    Task<IEnumerable<Country>> GetAll();
}
