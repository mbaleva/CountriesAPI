namespace CountriesAPI.Models;

public class Country
{
    public NameInfo Name { get; set; } = default!;

    public long Population { get; set; }
}
