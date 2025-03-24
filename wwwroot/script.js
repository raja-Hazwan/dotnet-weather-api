async function fetchWeather() {
    const city = document.getElementById("cityInput").value;
    if (!city) {
        alert("Please enter a city name.");
        return;
    }

    const apiUrl = `/api/weather/${city}`;

    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error("Weather data not found.");
        }

        const data = await response.json();

        // Convert Fahrenheit to Celsius
        const tempFahrenheit = data.currentConditions.temp;
        const tempCelsius = ((tempFahrenheit - 32) * 5 / 9).toFixed(2);

        document.getElementById("weatherResult").innerHTML = `
            <p><strong>Location:</strong> ${data.address}</p>
            <p><strong>Temperature:</strong> ${tempCelsius}°C</p>
            <p><strong>Weather:</strong> ${data.currentConditions.conditions}</p>
        `;
    } catch (error) {
        document.getElementById("weatherResult").innerHTML = `<p style="color: red;">${error.message}</p>`;
    }
}
