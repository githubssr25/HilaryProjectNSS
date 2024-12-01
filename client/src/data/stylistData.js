export const getAllStylists = async () => {
    const response = await fetch("http://localhost:5173/api/stylists");
    const data = await response.json();
    console.log("Fetched Services:", data);
    return data;
  };
  

  export const deactivateStylist = async (stylistId) => {
    const response = await fetch(`http://localhost:5173/api/stylists/${stylistId}/deactivate`, {
      method: "PUT",
    });
  
    if (!response.ok) {
      console.error(`Failed to deactivate stylist with ID ${stylistId}`);
      return null;
    }
  
    const data = await response.json();
    console.log(`Deactivated Stylist:`, data);
    return data;
  };
  