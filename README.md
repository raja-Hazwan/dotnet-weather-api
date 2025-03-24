# 🌦️ Weather API - ASP.NET Core

🚀 A simple Weather API built using **ASP.NET Core** that fetches real-time weather data from a third-party API (Visual Crossing). This project demonstrates API integration, caching with Redis, Swagger for API documentation, and environment variable management.

---

## 📌 Features

✅ Fetch real-time weather data by city name 🌍  
✅ Caching with **Redis** to reduce API calls and improve performance ⚡  
✅ Uses **environment variables** for API keys and configurations 🔒  
✅ **Swagger UI** for API testing 🛠️  
✅ **Error handling** for invalid cities or API failures ❌  
✅ Scalable and extendable with **.NET Core Web API** 🚀  
✅ **User-friendly front-end** with simple search functionality 🔎  

---

## 🛠️ Technologies Used

- **C# (ASP.NET Core Web API)** - Backend framework  
- **RestSharp** - HTTP client for API requests  
- **Redis** - In-memory caching  
- **Microsoft.Extensions.Configuration** - Environment variable management  
- **Swagger** - API documentation & testing  
- **JavaScript, HTML, CSS** - Basic front-end for user interaction  
- **Docker (Optional)** - For containerized deployment  

---

## 🚀 Getting Started

### 1️⃣ Prerequisites

Before you begin, ensure you have the following installed:

- **.NET 8 SDK** or later → [Download](https://dotnet.microsoft.com/en-us/download)
- **Redis** → [Download & Install](https://redis.io/docs/getting-started/installation/)
- **Visual Studio 2022** (or VS Code) for development
- **Git** for version control

### 2️⃣ Clone the Repository
```sh
git clone https://github.com/yourusername/weather-api.git
cd weather-api
```

### 3️⃣ Setup Environment Variables
Create a **`appsettings.Development.json`** file in the root directory and add:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Redis": {
    "ConnectionString": "localhost:6379",
    "CacheExpirationInHours": 12
  }
}
```

Then, **set your Weather API key** as an **environment variable**:
```sh
export WEATHER_API_KEY=your_api_key_here
```
*(On Windows, use `set WEATHER_API_KEY=your_api_key_here` instead.)*

### 4️⃣ Install Dependencies
```sh
dotnet restore
```

### 5️⃣ Run the Application
```sh
dotnet run
```

The API will start on **`http://localhost:5015`**.

---

## 🔥 API Endpoints

### 🌍 Get Weather Data
**GET** `/api/weather/{city}`  
Fetches real-time weather data for the specified city.

#### Example Request:
```sh
GET http://localhost:5015/api/weather/London
```

#### Example Response:
```json
{
  "address": "London, UK",
  "currentConditions": {
    "temp": 18.3,
    "conditions": "Partly Cloudy"
  }
}
```

---

## 🌐 Front-End Access

The front-end can be accessed by opening:
```
http://localhost:5015/index.html
```

This simple UI allows users to enter a city name and retrieve weather information.

---

## 📜 Swagger API Documentation
Swagger is enabled for easy testing. Open:
```
http://localhost:5015/swagger/index.html
```

---

## 📦 Docker Setup (Optional)

To run the API using Docker, follow these steps:

1️⃣ Build the Docker image:
```sh
docker build -t weather-api .
```

2️⃣ Run the container:
```sh
docker run -p 5015:5015 -e WEATHER_API_KEY=your_api_key_here weather-api
```

---

## 🤝 Contributing
Pull requests are welcome! If you find a bug or want to request a feature, please open an issue.

---

## 📜 License
This project is licensed under the **MIT License**.

---

## 📁 Project Structure
```
Weather_API/
│-- Controllers/
│   ├── WeatherController.cs
│-- wwwroot/
│   ├── index.html
│   ├── script.js
│   ├── style.css
│-- appsettings.json
│-- appsettings.Development.json
│-- Program.cs
│-- Weather_API.sln
│-- README.md
```

