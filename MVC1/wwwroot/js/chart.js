document.addEventListener("DOMContentLoaded", function () {
    // Получение данных из data-* атрибутов
    const dataY = JSON.parse(document.getElementById("chartData").dataset.y);
    const dataTemperatureMaterial = JSON.parse(
        document.getElementById("chartData").dataset.temperatureMaterial
    );
    const dataTemperatureGas = JSON.parse(
        document.getElementById("chartData").dataset.temperatureGas
    );
    const dataTemperatureDifference = JSON.parse(
        document.getElementById("chartData").dataset.temperatureDifference
    );

    // Построение графика температуры материала и газа
    const ctx1 = document.getElementById("temperatureChart").getContext("2d");
    new Chart(ctx1, {
        type: "line",
        data: {
            labels: dataY,
            datasets: [
                {
                    label: "Температура материала",
                    data: dataTemperatureMaterial,
                    borderColor: "blue",
                    borderWidth: 2,
                    fill: false,
                },
                {
                    label: "Температура газа",
                    data: dataTemperatureGas,
                    borderColor: "red",
                    borderWidth: 2,
                    fill: false,
                },
            ],
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true,
                },
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: "Координата y, м",
                    },
                },
                y: {
                    title: {
                        display: true,
                        text: "Температура, °C",
                    },
                },
            },
        },
    });

    // Построение графика разности температур
    const ctx2 = document.getElementById("differenceChart").getContext("2d");
    new Chart(ctx2, {
        type: "line",
        data: {
            labels: dataY,
            datasets: [
                {
                    label: "Разность температур",
                    data: dataTemperatureDifference,
                    borderColor: "green",
                    borderWidth: 2,
                    fill: false,
                },
            ],
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true,
                },
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: "Координата y, м",
                    },
                },
                y: {
                    title: {
                        display: true,
                        text: "Разность температур, °C",
                    },
                },
            },
        },
    });
});
