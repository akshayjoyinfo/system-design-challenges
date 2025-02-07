# Design Parking Lot System
Problem Statement: Design a Parking Lot System
Design a parking lot system that supports multiple parking spots and different types of vehicles. The system should be able to:

	- Allow vehicles to enter and exit.
	- Assign parking spots based on vehicle type (e.g., motorcycle, car, truck).
	- Track available and occupied spots.
	- Handle parking fees based on the duration of parking.
	- Support different parking rates for different vehicle types.
	- Enforce SOLID principles and appropriate design patterns.


## Entities

Entities (Models)
ParkingLot

Manages the overall parking system, tracks available and occupied spots.
ParkingSpot

Represents individual parking spots.
Can be of different types: small, medium, large.
Vehicle

Represents a vehicle entering the parking lot.
Can be of different types: motorcycle, car, truck.
Ticket

Issued when a vehicle enters.
Contains entry time, parking spot, and vehicle details.
ParkingRate

Defines the parking fee structure for different vehicle types.
Payment

Handles payment processing based on the ticket and duration.
EntryGate / ExitGate

Manages vehicle entry and exit.
Issues and closes tickets.

