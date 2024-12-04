export const getAppointmentById = async (id) => {
    const response = await fetch(`http://localhost:5173/api/appointments/${id}`);
    const data = await response.json();
    console.log(`Fetched Appointment ID ${id}:`, data);
    return data;
  };

  export const getAppointmentsByCustomerId = async (customerId) => {
    console.log(`Fetching appointments for Customer ID ${customerId}...`); // Debug log before fetch

    try {
        const response = await fetch(`http://localhost:5173/api/appointments/customer/${customerId}`);
        console.log("Response received for customer appointments:", response); // Debug log after fetch

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json(); // Attempt to parse JSON
        console.log(`Fetched Appointments for Customer ID ${customerId}:`, data); // Debug log parsed data
        return data;
    } catch (error) {
        console.error(`Error fetching appointments for Customer ID ${customerId}:`, error); // Log any error
        return []; // Return an empty array as fallback
    }
};


  export const getAllAppointments = async () => {
    console.log("Fetching all appointments..."); // Debug log before the fetch
    try {
        const response = await fetch("http://localhost:5173/api/appointments");
        // console.log("Response received for appointments:", response); // Debug log after fetch

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json(); // Attempt to parse JSON
        console.log("Fetched Appointments:", data); // Debug log parsed data
        return data;
    } catch (error) {
        console.error("Error fetching appointments:", error); // Log any error
        return []; // Return an empty array as fallback
    }
};

export const createAppointment = async (customerId, stylistId, serviceIds) => {

  console.log("Request body:", {
    customerId: parseInt(customerId, 10),
    stylistId: parseInt(stylistId, 10),
    timeOf: new Date().toISOString(),
    serviceIds: serviceIds,
  });
  
  const response = await fetch("api/appointments", {
    method: "POST",
    headers: {
      "Content-Type": "application/json", // Important: Ensure the backend knows it's JSON
    },
    body: JSON.stringify({
      customerId: parseInt(customerId, 10), // Ensure integers
  stylistId: parseInt(stylistId, 10),
      timeOf: new Date().toISOString(), // Optional: Send current time if not passed
      serviceIds: serviceIds, // List of selected service IDs
    }),
  });

  if (!response.ok) {
    throw new Error(`Failed to create appointment: ${response.statusText}`);
  }

  return response.json(); // Parse and return the response
};

  
  export const updateAppointment = async (id, updatedData) => {
    const response = await fetch(`http://localhost:5173/api/appointments/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(updatedData),
    });
    const data = await response.json();
    console.log(`Updated Appointment ID ${id}:`, data);
    return data;
  };
  
  export const deleteAppointment = async (id) => {
    const response = await fetch(`http://localhost:5173/api/appointments/${id}`, {
      method: "DELETE",
    });
    console.log(`Deleted Appointment with ID: ${id}`);
    return response.status === 204; // Return true if successfully deleted
  };
  