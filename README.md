# CLDV6211POE – EventEase Booking System

---

## Project Overview

This application allows booking specialists to:

* Manage venues and events
* Create and manage bookings
* Upload and store images using local Azure Blob Storage (Azurite)
* Search bookings using enhanced filtering functionality
* Maintain data persistence using SQL LocalDB

---

## Architecture

This project follows the Model-View-Controller (MVC) design pattern:

* Models → Define database structure and relationships
* Views → Handle UI and data display
* Controllers → Manage application logic and user interaction
* Services → Handle external integrations
* ViewModels → Combine data from multiple models for display

---

## Project Structure

```
CLDV6211POE/
│
├── Controllers/
│   ├── VenuesController.cs
│   ├── EventsController.cs
│   └── BookingsController.cs
│
├── Models/
│   ├── Venue.cs
│   ├── Event.cs
│   └── Booking.cs
│
├── ViewModels/
│   └── BookingViewModel.cs
│
├── Views/
│   ├── Venues/
│   ├── Events/
│   ├── Bookings/
│   └── Shared/
│       └── _Layout.cshtml
│
├── Services/
│   └── BlobService.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── wwwroot/
│
└── appsettings.json
```

---

## Features Implemented

### CRUD Operations

* Create, Read, Update, Delete functionality for:

  * Venues
  * Events
  * Bookings

---

### Image Upload with Azurite

* Images are uploaded using a Blob Service
* Stored in a container called `venue-images`
* Uses local Azure emulator (Azurite)
* Image URLs are stored in the database

---

### Enhanced Booking Display

* Combines data from:

  * Bookings
  * Venues
  * Events
* Displays meaningful data such as:

  * Venue Name
  * Event Name
  * Location
  * Capacity
  * Dates

---

### Search Functionality

* Search bookings by:

  * Booking ID
  * Event Name
* Implemented in Bookings Index view

---

### Validation & Error Handling

* Prevents duplicate bookings (same venue + event)
* Handles image upload errors
* Ensures application stability during invalid operations

---

### Database Persistence

* Uses SQL LocalDB / SQL Express
* Data persists between application runs
* Managed using Entity Framework Core

---

## Usage Guide

### Venues

* Create new venues
* Upload images
* View and edit venue details

---

### Events

* Create events with start and end dates
* Upload event images

---

### Bookings

* Create bookings by selecting a venue and event
* Prevents duplicate bookings
* View combined booking information

---

### Search

* Use search bar in Bookings page
* Filter by Booking ID or Event Name

---

## Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server LocalDB
* Azure Blob Storage (Azurite Emulator)
* Bootstrap (UI Styling)

---




