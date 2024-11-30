import { useContext } from 'react';
import { GlobalContext } from './GlobalContext';
import { useState} from 'react';
import {getCustomerAppointmentAndServices} from '../data/customerData'


export const GetBalance = () => {
  const { customers, services, stylists} = useContext(GlobalContext);
  const [chosenCustomerId, setChosenCustomerId] = useState(null);
  const [ourAppointmentAndService, setOurAppointmentAndService] = useState(null);
  const[ourPrice, setOurPrice] = useState(null);

  const handleCustomerSelection = (e) => {
    setChosenCustomerId(e.target.value);
    setOurPrice(null); // Reset price when selecting a new customer
    setOurAppointmentAndService(null);
  };

  console.log("what is value of customers, services, stylists", customers, services, stylists);

  const onSubmitForm = async (e) => {
    e.preventDefault();
    // Add logic to calculate balance or perform the desired action
    console.log("Selected Customer ID:", chosenCustomerId);

const ourAppointmentAndServiceResponse = await getCustomerAppointmentAndServices(parseInt(chosenCustomerId));

console.log("value of ourAppointmentAndServiceResponse", ourAppointmentAndServiceResponse);

if(ourAppointmentAndService != null){
  setOurAppointmentAndService(ourAppointmentAndServiceResponse);
}

const ourPrice = calculateTotalPrice(ourAppointmentAndServiceResponse);
setOurPrice(ourPrice);

  }

  const calculateTotalPrice = (appointments) => {
  const totalPrice = appointments.reduce((sum, eachAppt) => {
  if(!eachAppt.isCancelled && eachAppt.services.length > 0 && Array.isArray(eachAppt.services)){
    const serviceTotal = eachAppt.services.reduce((serviceSum, eachService) => {
      return serviceSum + eachService.price; //corresponds to this reduce services.reduce((serviceSum, eachService
    }, 0);
    return sum + serviceTotal; //corresond to this reduce appointments.reduce((sum, eachAppt) => {
  } else {
    return sum; //corresond to this reduce appointments.reduce((sum, eachAppt) => {
  }
}, 0);
return totalPrice;//you need this so calculateTotalPrice(ourAppointmentAndServiceResponse);this has an actual value
  }

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
