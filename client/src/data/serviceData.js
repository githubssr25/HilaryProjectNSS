export const getAllServices = async () => {
    const response = await fetch("http://localhost:5173/api/services");
    const data = await response.json();
    console.log("Fetched Services:", data);
    return data;
  };
  