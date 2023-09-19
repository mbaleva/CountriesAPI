using CountriesAPI.Clients;
using CountriesAPI.Controllers;
using CountriesAPI.Models;
using FluentAssertions;
using Moq;

namespace CountriesAPI.Tests;

public class CountriesControllerTests
{
    private readonly Mock<ICountriesClient> _clientMock = new Mock<ICountriesClient>();

    [Fact]
    public void FilterByName_Should_ReturnCountriesWithContainingNameOnly()
    {
        var countries = new List<Country> {
            new Country
            {
                Name = new NameInfo 
                {
                    Common = "Bulgaria"
                }
            },
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Test"
                }
            }
        };

        var countriesController = new CountriesController(_clientMock.Object);

        var result = countriesController.FilterByName(countries, "bul");

        result.Should().NotBeNull();
        result.Count().Should().Be(1);
        result.First().Name.Common.Should().Be("Bulgaria");
    }

    [Fact]
    public void FilterByPopulation_Should_ReturnCountriesWithPopulationLessThanProvided()
    {
        var countries = new List<Country> {
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Bulgaria"
                },
                Population = 11_000_000
            },
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Test"
                },
                Population = 100_000_000
            }
        };

        var countriesController = new CountriesController(_clientMock.Object);

        var result = countriesController.FilterByPopulation(countries, "25");

        result.Should().NotBeNull();
        result.Count().Should().Be(1);
        result.First().Name.Common.Should().Be("Bulgaria");
        result.First().Population.Should().Be(11_000_000);
    }

    [Fact]
    public void SortByName_Should_SortByAsc()
    {
        var countries = new List<Country> {
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Bulgaria"
                },
                Population = 11
            },
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Test"
                },
                Population = 100
            }
        };

        var countriesController = new CountriesController(_clientMock.Object);

        var result = countriesController.SortByName(countries, "ascend");

        result.Should().NotBeNull();
        result.Count().Should().Be(2);
        result.First().Name.Common.Should().Be("Bulgaria");
    }

    [Fact]
    public void SortByName_Should_SortByDesc()
    {
        var countries = new List<Country> {
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Bulgaria"
                },
                Population = 11
            },
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Test"
                },
                Population = 100
            }
        };

        var countriesController = new CountriesController(_clientMock.Object);

        var result = countriesController.SortByName(countries, "descend");

        result.Should().NotBeNull();
        result.Count().Should().Be(2);
        result.First().Name.Common.Should().Be("Test");
    }

    [Fact]
    public void GetFirstNRecords_Should_GetDataWithoutOffset()
    {
        var countries = new List<Country> {
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Bulgaria"
                },
                Population = 11
            },
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Test"
                },
                Population = 100
            }
        };

        var countriesController = new CountriesController(_clientMock.Object);

        var result = countriesController.GetFirstNRecords(countries, 1, null);

        result.Should().NotBeNull();
        result.Count().Should().Be(1);
        result.First().Name.Common.Should().Be("Bulgaria");
    }

    [Fact]
    public void GetFirstNRecords_Should_GetDataWithOffset()
    {
        var countries = new List<Country> {
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Bulgaria"
                },
                Population = 11
            },
            new Country
            {
                Name = new NameInfo
                {
                    Common = "Test"
                },
                Population = 100
            }
        };

        var countriesController = new CountriesController(_clientMock.Object);

        var result = countriesController.GetFirstNRecords(countries, 1, 1);

        result.Should().NotBeNull();
        result.Count().Should().Be(1);
        result.First().Name.Common.Should().Be("Test");
    }
}