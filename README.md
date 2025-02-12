# Hotel Booking API

## Setup

1. Clone the repository.
2. Update the connection string in `appsettings.json`.
3. Run the following commands to apply migrations and seed the database:
	a. Add-Migration HotelBookingMigration0
	b. Update-Database
	c. `POST /api/data/seed`: Seeds the database with initial data.
4. Run the application: dotnet run

## API Endpoints

- `GET /api/hotel/{name}`: Find a hotel by name.
- `GET /api/room/checkroom`: Find available rooms between two dates for a given number of people. e.g: "http://localhost:5000/api/room/available?startDate=2023-12-01&endDate=2023-12-05&numberOfGuests=2"
- `POST /api/booking/createbooking`: Book a room.
- `GET /api/booking/{referenceNumber}`: Find booking details by reference number.

- `POST /api/data/seed`: Seeds the database with initial data.
- `POST /api/data/reset`: Resets the database by removing all data.

## Testing

Run the tests using the following command: dotnet test
   
