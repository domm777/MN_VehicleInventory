# API Contract DOCUMENTATION

## Get All Vehicles
Endpoint: "GET /api/MN_Vehicles"
Description: Retrives a the list of all MN_Vehicles in the MN_Inventory

Response (res) [200 OK](https://http.cat/status/200)
```json
[
    {
        "id": "1",
        "vehicleCode": "V-101",
        "locationId": 1,
        "vehicleType": "Sedan",
        "status": "Available"
    }
]
```

## Get Vehicle By ID
Endpoint: "GET /api/MN_Vehicles/{id}"
Description: Retrives a a single vehicle by it's ID

Response (res) [200 OK](https://http.cat/status/200)
```json
[
    {
        "id": "2",
        "vehicleCode": "V-102",
        "locationId": 2,
        "vehicleType": "SUBERU",
        "status": "Available"
    }
]
```

## Create Vehicle
Endpoint: "POST /api/MN_Vehicles"
Description: Adds a new vehicle to inventory, default status set -> Available

Response (res) [201 OK](https://http.cat/status/201)
```json
[
    {
        "id": "3",
        "vehicleCode": "V-103",
        "locationId": 3,
        "vehicleType": "SUV",
        "status": "Available"
    }
]
```

## Update Vehicle By ID
Endpoint: "PUT /api/MN_Vehicles/{id}/status"
Description: Updates the status of that vehicle with the given id then enforces a domain rules

Response (res) [200 OK](https://http.cat/status/200)
```json
[
    {
        "id": "3",
        "vehicleCode": "V-101",
        "locationId": 3,
        "vehicleType": "SUV",
        "status": "Rented"
    }
]
```

## Delete Vehicle By ID
Endpoint: "GET /api/MN_Vehicles/{id}"
Description: Retrives a a single vehicle by it's ID

Response (res) [204 OK](https://http.cat/status/204)
```json
[{}]
```