namespace ParkingLotSystem
{
    public enum VehicleType { Motorcycle, Car, Truck }

    // Vehicle Class
    public class Vehicle
    {
        public string LicensePlate { get; }
        public VehicleType Type { get; }

        public Vehicle(string licensePlate, VehicleType type)
        {
            LicensePlate = licensePlate;
            Type = type;
        }
    }

    // Parking Spot Class
    public class ParkingSpot
    {
        public int SpotId { get; }
        public VehicleType SpotType { get; }
        public bool IsOccupied { get; private set; }
        public Vehicle? ParkedVehicle { get; private set; }

        public ParkingSpot(int spotId, VehicleType spotType)
        {
            SpotId = spotId;
            SpotType = spotType;
            IsOccupied = false;
        }

        public void AssignVehicle(Vehicle vehicle)
        {
            if (!IsOccupied && vehicle.Type == SpotType)
            {
                ParkedVehicle = vehicle;
                IsOccupied = true;
            }
        }

        public void FreeSpot()
        {
            ParkedVehicle = null;
            IsOccupied = false;
        }
    }

    // Ticket Class
    public class Ticket
    {
        public int TicketId { get; }
        public Vehicle Vehicle { get; }
        public DateTime EntryTime { get; }
        public ParkingSpot Spot { get; }

        public Ticket(int ticketId, Vehicle vehicle, ParkingSpot spot)
        {
            TicketId = ticketId;
            Vehicle = vehicle;
            EntryTime = DateTime.Now;
            Spot = spot;
        }
    }

    // Parking Rate Class
    public class ParkingRate
    {
        private readonly Dictionary<VehicleType, double> _rates = new()
    {
        { VehicleType.Motorcycle, 2.0 },
        { VehicleType.Car, 5.0 },
        { VehicleType.Truck, 10.0 }
    };

        public double GetRate(VehicleType type) => _rates[type];
    }

    // Payment Class
    public class Payment
    {
        public Ticket Ticket { get; }
        public double Amount { get; private set; }
        public bool IsPaid { get; private set; }

        public Payment(Ticket ticket, double amount)
        {
            Ticket = ticket;
            Amount = amount;
            IsPaid = false;
        }

        public void ProcessPayment()
        {
            IsPaid = true;
        }
    }

    // Parking Lot Class
    public class ParkingLot
    {
        private List<ParkingSpot> _spots = new();
        private int _ticketCounter = 1;
        private ParkingRate _rate = new();

        public ParkingLot(int small, int medium, int large)
        {
            for (int i = 0; i < small; i++) _spots.Add(new ParkingSpot(i, VehicleType.Motorcycle));
            for (int i = 0; i < medium; i++) _spots.Add(new ParkingSpot(i + small, VehicleType.Car));
            for (int i = 0; i < large; i++) _spots.Add(new ParkingSpot(i + small + medium, VehicleType.Truck));
        }

        public Ticket? AssignSpot(Vehicle vehicle)
        {
            foreach (var spot in _spots)
            {
                if (!spot.IsOccupied && spot.SpotType == vehicle.Type)
                {
                    spot.AssignVehicle(vehicle);
                    return new Ticket(_ticketCounter++, vehicle, spot);
                }
            }
            return null; // No spot available
        }

        public double ReleaseSpot(Ticket ticket)
        {
            ticket.Spot.FreeSpot();
            TimeSpan duration = DateTime.Now - ticket.EntryTime;
            double amount = _rate.GetRate(ticket.Vehicle.Type) * Math.Ceiling(duration.TotalHours);
            return amount;
        }
    }

    // Test Program
    public class Program
    {
        public static void Main()
        {
            ParkingLot parkingLot = new ParkingLot(2, 2, 1);
            Vehicle car = new Vehicle("ABC123", VehicleType.Car);
            Ticket? ticket = parkingLot.AssignSpot(car);

            if (ticket != null)
            {
                Console.WriteLine($"Vehicle {car.LicensePlate} parked at spot {ticket.Spot.SpotId}");
                //double amount = parkingLot.ReleaseSpot(ticket);
                //Console.WriteLine($"Parking fee: {amount} EUR");
            }
            else
            {
                Console.WriteLine("No parking spot available.");
            }

            car = new Vehicle("ABCABC", VehicleType.Car);
            ticket = parkingLot.AssignSpot(car);

            if (ticket != null)
            {
                Console.WriteLine($"Vehicle {car.LicensePlate} parked at spot {ticket.Spot.SpotId}");
                //double amount = parkingLot.ReleaseSpot(ticket);
                //Console.WriteLine($"Parking fee: {amount} EUR");
            }
            else
            {
                Console.WriteLine("No parking spot available.");
            }


            car = new Vehicle("BRUCE", VehicleType.Car);
            ticket = parkingLot.AssignSpot(car);

            if (ticket != null)
            {
                Console.WriteLine($"Vehicle {car.LicensePlate} parked at spot {ticket.Spot.SpotId}");
                //double amount = parkingLot.ReleaseSpot(ticket);
                //Console.WriteLine($"Parking fee: {amount} EUR");
            }
            else
            {
                Console.WriteLine("No parking spot available.");
            }
        }
    }
}
