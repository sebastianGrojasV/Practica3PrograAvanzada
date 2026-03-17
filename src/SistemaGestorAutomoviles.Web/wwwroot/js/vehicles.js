const apiBaseUrl = "https://localhost:7249/api/vehicles";

const vehicleForm = document.getElementById("vehicleForm");
const tableBody = document.getElementById("vehiclesTableBody");
const messageContainer = document.getElementById("message");
const detailCard = document.getElementById("detailCard");
const cancelEditButton = document.getElementById("cancelEditButton");

document.addEventListener("DOMContentLoaded", () => {
    loadVehicles();
    resetForm();
});

vehicleForm.addEventListener("submit", async event => {
    event.preventDefault();

    const vehicleId = document.getElementById("vehicleId").value;
    const payload = collectFormData();

    try {
        const endpoint = vehicleId ? `${apiBaseUrl}/${vehicleId}` : apiBaseUrl;
        const method = vehicleId ? "PUT" : "POST";

        const response = await fetch(endpoint, {
            method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const errorBody = await parseError(response);
            throw new Error(errorBody);
        }

        showMessage(vehicleId ? "Vehículo actualizado correctamente." : "Vehículo creado correctamente.", "success");
        resetForm();
        await loadVehicles();
    } catch (error) {
        showMessage(error.message, "error");
    }
});

cancelEditButton.addEventListener("click", () => {
    resetForm();
});

async function loadVehicles() {
    try {
        const response = await fetch(apiBaseUrl);
        if (!response.ok) {
            throw new Error("No se pudo cargar el listado.");
        }

        const vehicles = await response.json();
        tableBody.innerHTML = "";

        vehicles.forEach(vehicle => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${vehicle.id}</td>
                <td>${vehicle.brand}</td>
                <td>${vehicle.model}</td>
                <td>${vehicle.year}</td>
                <td>$${vehicle.price.toFixed(2)}</td>
                <td>${vehicle.isAvailable ? "Sí" : "No"}</td>
                <td>
                    <button onclick="showDetail(${vehicle.id})">Detalle</button>
                    <button onclick="startEdit(${vehicle.id})">Editar</button>
                    <button onclick="removeVehicle(${vehicle.id})">Eliminar</button>
                </td>
            `;
            tableBody.appendChild(row);
        });
    } catch (error) {
        showMessage(error.message, "error");
    }
}

window.showDetail = async function (id) {
    try {
        const response = await fetch(`${apiBaseUrl}/${id}`);
        if (!response.ok) {
            throw new Error("No se encontró el vehículo solicitado.");
        }

        const vehicle = await response.json();
        detailCard.classList.remove("hidden");
        detailCard.innerHTML = `
            <h3>Detalle vehículo #${vehicle.id}</h3>
            <p><strong>Marca/Modelo:</strong> ${vehicle.brand} ${vehicle.model}</p>
            <p><strong>Año:</strong> ${vehicle.year}</p>
            <p><strong>Color:</strong> ${vehicle.color}</p>
            <p><strong>Tipo:</strong> ${vehicle.type}</p>
            <p><strong>Transmisión:</strong> ${vehicle.transmission}</p>
            <p><strong>Kilometraje:</strong> ${vehicle.mileage} km</p>
            <p><strong>Disponible:</strong> ${vehicle.isAvailable ? "Sí" : "No"}</p>
        `;
    } catch (error) {
        showMessage(error.message, "error");
    }
};

window.startEdit = async function (id) {
    try {
        const response = await fetch(`${apiBaseUrl}/${id}`);
        if (!response.ok) {
            throw new Error("No se pudo cargar la información para editar.");
        }

        const vehicle = await response.json();
        document.getElementById("vehicleId").value = vehicle.id;
        document.getElementById("brand").value = vehicle.brand;
        document.getElementById("model").value = vehicle.model;
        document.getElementById("year").value = vehicle.year;
        document.getElementById("price").value = vehicle.price;
        document.getElementById("color").value = vehicle.color;
        document.getElementById("type").value = vehicle.type;
        document.getElementById("transmission").value = vehicle.transmission;
        document.getElementById("mileage").value = vehicle.mileage;
        document.getElementById("isAvailable").value = `${vehicle.isAvailable}`;
        document.getElementById("submitButton").textContent = "Actualizar";
    } catch (error) {
        showMessage(error.message, "error");
    }
};

window.removeVehicle = async function (id) {
    const confirmed = window.confirm("¿Seguro que deseas eliminar este vehículo?");
    if (!confirmed) {
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/${id}`, { method: "DELETE" });
        if (!response.ok) {
            const errorBody = await parseError(response);
            throw new Error(errorBody);
        }

        showMessage("Vehículo eliminado correctamente.", "success");
        detailCard.classList.add("hidden");
        await loadVehicles();
    } catch (error) {
        showMessage(error.message, "error");
    }
};

function collectFormData() {
    return {
        brand: document.getElementById("brand").value.trim(),
        model: document.getElementById("model").value.trim(),
        year: Number(document.getElementById("year").value),
        price: Number(document.getElementById("price").value),
        color: document.getElementById("color").value.trim(),
        type: document.getElementById("type").value.trim(),
        transmission: document.getElementById("transmission").value.trim(),
        mileage: Number(document.getElementById("mileage").value),
        isAvailable: document.getElementById("isAvailable").value === "true"
    };
}

function resetForm() {
    vehicleForm.reset();
    document.getElementById("vehicleId").value = "";
    document.getElementById("submitButton").textContent = "Guardar";
}

function showMessage(message, type) {
    messageContainer.textContent = message;
    messageContainer.className = type;
}

async function parseError(response) {
    try {
        const body = await response.json();
        return body.message || JSON.stringify(body);
    } catch {
        return "Ocurrió un error inesperado.";
    }
}
