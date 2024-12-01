import { useState } from "react";
import { createCustomer } from "../data/customerData";
import { useContext } from 'react';
import { GlobalContext } from './GlobalContext';

export const CreateCustomer = () => {
  const {customers} = useContext(GlobalContext);
  const [customerName, setCustomerName] = useState("");
  const [message, setMessage] = useState("");

  const handleCreate = async (e) => {
    e.preventDefault();
    const result = await createCustomer(customerName);
    if (result) {
      setMessage(`Customer Created: ID ${result.newCustomerId}, Name ${result.newCustomerName}`);
    } else {
      setMessage("Failed to create customer.");
    }
  };

  return (
    <form onSubmit={handleCreate}>
      <label htmlFor="customerName">Customer Name:</label>
      <input
        type="text"
        id="customerName"
        value={customerName}
        onChange={(e) => setCustomerName(e.target.value)}
        required
      />
      <button type="submit">Create Customer</button>
      {message && <p>{message}</p>}
    </form>
  );
};
