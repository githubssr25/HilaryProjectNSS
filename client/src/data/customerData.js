export const getAllCustomers = async () => {
    const response = await fetch("http://localhost:5173/api/customers");
    const data = await response.json();
    console.log("Fetched Customers:", data);
    return data;
  };
  
  export const createCustomer = async (customer) => {
    const response = await fetch("http://localhost:5173/api/customers", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(customer),
    });
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
  