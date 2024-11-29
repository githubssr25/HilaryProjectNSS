export const getAllStylists = async () => {
    const response = await fetch("http://localhost:5173/api/stylists");
    const data = await response.json();
    console.log("Fetched Services:", data);
    return data;
  };
  