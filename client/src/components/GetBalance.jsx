import { useContext } from 'react';
import { GlobalContext } from './GlobalContext';
import { useState, useEffect } from 'react';
import {getAppointmentsByCustomerId} from '../data/appointmentData'


export const GetBalance = () => {
  const { customers, services, stylists, refreshAppointments } = useContext(GlobalContext);
  const [chosenCustomerId, setChosenCustomerId] = useState(null);
  const [ourAppointments, setOurAppointments] = useState(null);
  const[ourPrice, setOurPrice] = useState(null);

  const handleCustomerSelection = (e) => {
    setChosenCustomerId(e.target.value);
    setOurPrice(null); // Reset price when selecting a new customer
  };

  console.log("what is value of customers, services, stylists", customers, services, stylists);

  const onSubmitForm = (e) => {
    e.preventDefault();
    // Add logic to calculate balance or perform the desired action
    console.log("Selected Customer ID:", chosenCustomerId);

    const ourCustomer = customers.filter(eachCustomer => eachCustomer.customerId == parseInt(chosenCustomerId));
    console.log("what is ourCustomer", ourCustomer);

    const totalPrice = customers.services.reduce((sum, eachCustomer) => {
        return sum + eachCustomer.price;
    }, 0);

    console.log("what is the total price", totalPrice);

    setOurPrice(totalPrice);

    // const appointmentResponse = getAppointmentsByCustomerId(chosenCustomerId);

    // console.log("do we get appointments by customer ", appointmentResponse);
    // if(appointmentResponse != null){
    //     setOurAppointments(appointmentResponse);
    // }

  };

  return (
    <>
      <form onSubmit={onSubmitForm}>
        <label htmlFor="customer">Find Total Balance Owed</label>
        <div>
          <select
            id="customer"
            value={chosenCustomerId || ''}
            onChange={handleCustomerSelection}
          >
             <option value="" disabled>
              Select Which User You Are
            </option>
            {customers.map((indCustomer) => (
                <option
                key={indCustomer.customerId}
                value={indCustomer.customerId}
                >
                 Name: {indCustomer.name} ID: {indCustomer.customerId}
                </option>
            ))}
          </select>
        </div>
        <button type="submit">Submit</button>
      </form>


      {/* Display the total price if calculated */}
      {ourPrice !== null && (
        <div>
          <h3>Total Balance Owed:</h3>
          <p>${ourPrice.toFixed(2)}</p>
        </div>
      )}
    </>
  );
};
