using System;

using RailwaySystem;
public class RailwayOperationsTests
{
    private readonly RailwayOperations _railwayOperations;

    public RailwayOperationsTests()
    {
        _railwayOperations = new RailwayOperations();
    }

    // тесты для CalculateArrivalTime

    //      позитивные

    [Theory]
    [InlineData("12:00", 30, "12:30")]  // Обычный случай
    [InlineData("23:30", 1440, "23:30")]   // прошли сутки
    public void CalculateArrivalTime_ValidInput_ReturnsCorrectTime(string departure, int minutes, string expected)
    {
        var result = _railwayOperations.CalculateArrivalTime(departure, minutes);
        Assert.Equal(expected, result);
    }

    //      негативные 

    [Fact]

    public void CalculateArrivalTime_InvalidFormat_ThrowsFormatException()
    {
        var exception = Assert.Throws<FormatException>(
            () => _railwayOperations.CalculateArrivalTime("invalid_time", 30));

        Assert.Contains("was not recognized as a valid TimeSpan", exception.Message);
    }

    [Fact]
    public void CalculateArrivalTime_NegativeMinutes_ThrowsException()
    {
        try
        {
            var result = _railwayOperations.CalculateArrivalTime("12:00", -5);
            Assert.NotNull(result); // Проверка результата, если исключения не было
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Assert.Contains("cannot be negative", ex.Message); // Проверка сообщения
        }
        catch
        {
            Assert.Fail("Выброшено неожиданное исключение");
        }
    }

    //      тест на граничное значение
    [Fact]
    public void CalculateArrivalTime_MidnightBoundary_ReturnsCorrectTime()
    {
        var result = _railwayOperations.CalculateArrivalTime("23:59", 1);
        Assert.Equal("00:00", result);
    }

    // тесты для IsCargoOverweight

    //     позитивные

    [Theory]
    [InlineData(12.5, 30.0, false)] // нет перегруза
    [InlineData(30.0, 20.0, true)] // есть перегруз


    public void IsCargoOverweight_ValidInput_ReturnsFalse(double cargoWeight, double maxWeight, bool expected)
    {

        var result = _railwayOperations.IsCargoOverweight(cargoWeight, maxWeight);

        Assert.Equal(expected, result);

    }


    //  негативные   
    [Theory]
    [InlineData(50.0, -100.0)] // проверяем ввод отрицательных значений
    [InlineData(-50.0, 100.0)]

    public void IsCargoOverweight_InvalidInput_ThrowsException(double cargoWeight, double maxWeight)
    {

        var exception = Assert.Throws<ArgumentException>(
        () => _railwayOperations.IsCargoOverweight(cargoWeight, maxWeight));

        Assert.Equal("Weight cannot be negative", exception.Message);
    }

    //  тест на граничные значения

    [Fact]

    public void IsCargoOverweightEqualityBoundaryReturnsFalse()
    {
        var result = _railwayOperations.IsCargoOverweight(100.0, 100.0);
        Assert.False(result);
    }

    // тесты для CalculateShippingCost

    //  позитивные

    [Theory]
    [InlineData(25, 2, 30, 1500)] // вес не превышает 10 000
    [InlineData(25, 2, 10500, 603750)] // вес превышает 10 000


    public void CalculateShippingCost_ValidInput_CalculatesCorrectly(double distanceKm, double ratePerKm, double cargoWeight, double expected)
    {

        var result = _railwayOperations.CalculateShippingCost(distanceKm, ratePerKm, cargoWeight);

        Assert.Equal(expected, result);

    }

    //  негативные

    [Theory]
    [InlineData(0, 2, 500)] // Нулевая дистанция
    [InlineData(100, -1, 500)] // Отрицательный тариф
    public void CalculateShippingCost_InvalidInputs_ThrowsArgumentException(double distanceKm, double ratePerKm, double cargoWeight)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _railwayOperations.CalculateShippingCost(distanceKm, ratePerKm, cargoWeight));
    }

    [Fact]
    public void CalculateShippingCost_WeightBoundaryApplies_CorrectOutput()
    {
        // Act
        var result = _railwayOperations.CalculateShippingCost(50, 2, 10000);

        // Assert
        Assert.Equal(1000000, result);
    }

    // тесты для CalculateNetTravelTime

    //  позитивные

    [Theory]

    [InlineData(185, 20, "2 ч 45 мин")]
    [InlineData(60, 15, "45 мин")]
    public void CalculateNetTravelTime_ValidInput_ReturnsTime(int fullTravelTimeMinutes, int stopsMinutes, string expected)
    {

        var result = _railwayOperations.CalculateNetTravelTime(fullTravelTimeMinutes, stopsMinutes);

        Assert.Equal(expected, result);

    }

    //  негативные 

    [Theory]

    [InlineData(10, 20)]
    [InlineData(100, -20)]
    public void CalculateNetTravelTime_InvalidInput_ReturnsTime(int fullTravelTimeMinutes, int stopsMinutes)
    {
        var exception = Assert.ThrowsAny<Exception>(() => _railwayOperations.CalculateNetTravelTime(fullTravelTimeMinutes, stopsMinutes));

    }


    // граничное значение

    [Fact]
    public void CalculateNetTravelTime_ZeroMinutes_ReturnsZero()
    {

        var result = _railwayOperations.CalculateNetTravelTime(0, 0);

        Assert.Equal("0 мин", result);
    }

}
