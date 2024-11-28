export const getAppointmentById = async (id) => {
    const response = await fetch(`http://localhost:5173/api/appointments/${id}`);
    const data = await response.json();
    console.log(`Fetched Appointment ID ${id}:`, data);
    return data;
  };


  export const getAllAppointments = async () => {
    try {
        const response = await fetch("http://localhost:5173/api/appointments");
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json(); // Attempt to parse JSON
        console.log("Fetched Appointments:", data);
        return data;
    } catch (error) {
        console.error("Error fetching appointments:", error);
        return []; // Return an empty array as fallback
    }
};

  export const createAppointment = async (appointment) => {
    const response = await fetch("http://localhost:5173/api/appointments", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(appointment),
    });
    const data = await response.json();
    console.log("Created Appointment:", data);
    return data;
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
  