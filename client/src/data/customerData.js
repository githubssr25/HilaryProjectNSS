export const getAllCustomers = async () => {
    const response = await fetch("http://localhost:5173/api/customers");
    const data = await response.json();
    console.log("Fetched Customers:", data);
    return data;
  };
  
  export const createCustomer = async (customerName) => {
    const response = await fetch("http://localhost:5173/api/customers", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ name: customerName }),
    });
  
    if (!response.ok) {
      console.error("Failed to create customer");
      return null;
    }
  
    const data = await response.json();
    console.log("Created Customer:", data);
    return data;
  };
  
  
  export const deleteCustomer = async (id) => {
    const response = await fetch(`http://localhost:5173/api/customers/${id}`, {
      method: "DELETE",
    });
    console.log(`Deleted Customer with ID: ${id}`);
    return response.status === 204; // Return true if successfully deleted
  };
  
  export const getCustomerAppointmentAndServices = async (customerId) => {
    const response = await fetch(`http://localhost:5173/api/customers/${customerId}/appointmentAndService`);
    if (!response.ok) {
      console.error(`Failed to fetch appointments and services for CustomerId ${customerId}`);
      return null; // Return null if the request fails
    }
    const data = await response.json();
    console.log(`Fetched Appointments and Services for CustomerId ${customerId}:`, data);
    return data;
  };