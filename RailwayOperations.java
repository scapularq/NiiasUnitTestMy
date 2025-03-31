import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

public class RailwayOperations {
    
    public String calculateArrivalTime(String departureTime, int travelMinutes) {

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("HH:mm");
        LocalTime time = LocalTime.parse(departureTime, formatter);
        LocalTime arrivalTime = time.plusMinutes(travelMinutes);

        return arrivalTime.format(formatter);
    }
    
    public boolean isCargoOverweight(double cargoWeight, double maxWeight) {

        if (cargoWeight < 0 || maxWeight < 0) {
            throw new IllegalArgumentException("Weight cannot be negative");
        }

        return cargoWeight > maxWeight;
    }
    
    // Рассчитать стоимость перевозки с учетом коэффициента перегруза
    public double calculateShippingCost(double distanceKm, double ratePerKm, double cargoWeight) {

        if (distanceKm <= 0 || ratePerKm <= 0 || cargoWeight <= 0) {
            throw new IllegalArgumentException("All parameters must be positive");
        }
        
        double baseCost = distanceKm * ratePerKm * cargoWeight;
        double overloadCoefficient = cargoWeight > 10000 ? 1.15 : 1.0;
        
        return baseCost * overloadCoefficient;
    }
}