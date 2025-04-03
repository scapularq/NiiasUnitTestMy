using System;

namespace RailwaySystem;
public class RailwayOperations
{
    public string CalculateArrivalTime(string departureTime, int travelMinutes)
    {
        var time = TimeSpan.Parse(departureTime);
        var arrivalTime = time.Add(TimeSpan.FromMinutes(travelMinutes));

        return arrivalTime.ToString(@"hh\:mm");
    }

    public bool IsCargoOverweight(double cargoWeight, double maxWeight)
    {
        if (cargoWeight < 0 || maxWeight < 0)
            throw new ArgumentException("Weight cannot be negative");

        return cargoWeight > maxWeight;
    }

    public double CalculateShippingCost(double distanceKm, double ratePerKm, double cargoWeight)
    {
        if (distanceKm <= 0 || ratePerKm <= 0 || cargoWeight <= 0)
            throw new ArgumentException("All parameters must be positive");

        double baseCost = distanceKm * ratePerKm * cargoWeight;
        double overloadCoefficient = cargoWeight > 10000 ? 1.15 : 1.0;

        return baseCost * overloadCoefficient;
    }

    // доп метод для расчета чистого времени пути
    public string CalculateNetTravelTime(int fullTravelTimeMinutes, int stopsMinutes)
    {
        var netTravelTime = fullTravelTimeMinutes - stopsMinutes;

        // Разделение на часы и минуты
        int hours = netTravelTime / 60;
        int minutes = netTravelTime % 60;

        if (fullTravelTimeMinutes < stopsMinutes) { throw new InvalidOperationException("The result cannot be negative"); }
        else if (fullTravelTimeMinutes < 0 || stopsMinutes < 0) {throw new ArgumentOutOfRangeException("Time cannot be negative");}

        // Форматирование результата
        if (hours > 0 && minutes > 0)
            return $"{hours} ч {minutes} мин";
        else if (hours > 0)
            return $"{hours} ч";
        else
            return $"{minutes} мин";
    }
}

