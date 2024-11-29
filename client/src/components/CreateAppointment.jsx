import { useContext } from 'react';
import { GlobalContext } from './GlobalContext';
import { createContext, useState, useEffect } from 'react';
import {createAppointment} from '../data/appointmentData'


export const CreateAppointment = () => {
  const { customers, services, stylists, refreshAppointments } =
    useContext(GlobalContext);
  const [listOfServiceId, setListOfServiceId] = useState([]);
  const [chosenCustomerId, setChosenCustomerId] = useState(null);
  const [chosenStylistId, setChosenStylistId] = useState(null);
  const [chosenStylist, setChosenStylist] = useState(null);
  const [createdAppointment, setCreatedAppointment] = useState(null);

  // Sync selected stylist
// Sync selected stylist
useEffect(() => {
    if (chosenStylistId) {
      console.log("Finding stylist with ID:", chosenStylistId);
      console.log("Current stylists:", stylists); // Log all stylists for comparison
  
      const selectedStylist = stylists.find(
        (stylist) => stylist.stylistId === chosenStylistId
      );
  
      console.log("Chosen Stylist:", selectedStylist); // Log the selected stylist
      setChosenStylist(selectedStylist || null);
    } else {
      console.log("No Stylist Selected");
      setChosenStylist(null);
    }
  }, [chosenStylistId, stylists]);
  

  // UseEffect to refresh global state after an appointment is created
//   useEffect(() => {
//     if (createdAppointment) {
//       console.log("Fetching updated appointments after creation...");
//       refreshAppointments(); // Trigger the global state to refresh the appointments
//     }
//   }, [createdAppointment, refreshAppointments]);

  const onSubmitForm = async (event) => {
    event.preventDefault();
  
    if (chosenCustomerId && chosenStylistId && chosenStylist) {
      try {
        const ourCreatedAppointment = await createAppointment(
          chosenCustomerId,
          chosenStylistId,
          listOfServiceId
        );
  
        if (ourCreatedAppointment) {
          setCreatedAppointment(ourCreatedAppointment); // Optional
          console.log("Appointment created:", ourCreatedAppointment);
  
          // Refresh appointments immediately after creation
          console.log("Refreshing appointments...");
          await refreshAppointments();
        }
      } catch (error) {
        console.error("Error creating appointment:", error);
      }
    }
  };

  const addServiceIdToList = (event, serviceId) => {
    if (event.target.checked) {
      setListOfServiceId((prevList) => [...prevList, serviceId]);
    } else {
      setListOfServiceId((prevList) =>
        prevList.filter((id) => id !== serviceId)
      );
    }
  };

  const handleCustomerSelection = (event) => {
    setChosenCustomerId(parseInt(event.target.value));
  };

  const handleStylistSelection = (event) => {
    const stylistId = parseInt(event.target.value);
    console.log("Selected Stylist ID:", stylistId);
    setChosenStylistId(stylistId);
  };



  /* <div> */
  /* <label htmlFor="stylist">Select Stylist</label>
<select
  id="stylist"
  value={chosenStylistId} // Bind value to state
  onChange={(e) => handleStylistSelection(e)}
>
  <option value="" disabled>
    Select Which Stylist You Prefer
  </option>
  {stylists.map((indStylist) => (
    <option key={indStylist.stylistId} value={indStylist.stylistId}>
      Name: {indStylist.name} ID: {indStylist.stylistId}
    </option>
  ))}
</select>
</div>
 */

  return (
    <>
      <form onSubmit={onSubmitForm}>
        <label> Create A New Appointment </label>

        <div>
          <label htmlFor="customer"> Select Customer </label>
          <select
            id="customer"
            value={chosenCustomerId} // Bind value to state
            onChange={(e) => handleCustomerSelection(e)}
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
              //    <li> </li>
            ))}
          </select>
        </div>

        <div>
          <label htmlFor="stylist">Select Stylist</label>
          <select
            id="stylist"
            value={chosenStylistId || ""}
            onChange={handleStylistSelection}
          >
            <option value="" disabled>
              Select Which Stylist You Prefer
            </option>
            {stylists.map((indStylist) => (
              <option key={indStylist.stylistId} value={indStylist.stylistId}>
                Name: {indStylist.name} ID: {indStylist.stylistId}
              </option>
            ))}
          </select>
        </div>

        <div>
          {chosenStylist && chosenStylist.services && (
            <div>
              <label>Select Services You Want</label>
              <div>
                {chosenStylist.services.map((service) => (
                  <div key={service.serviceId}>
                    <input
                      type="checkbox"
                      id={`service-${service.serviceId}`}
                      value={service.serviceId}
                      onChange={(e) => addServiceIdToList(e, service.serviceId)}
                    />
                    <label htmlFor={`service-${service.serviceId}`}>
                      {service.name} - ${service.price} -{" "}
                      {service.durationMinutes} mins
                    </label>
                  </div>
                ))}
              </div>
            </div>
          )}
        </div>

        <div></div>
        <button type="submit"> Click Here to Create Appointment</button>
      </form>

      <div>
        { createdAppointment && (
            <h1> Successfully Created New Appointment </h1>
        )}
    </div>
    </>
  );
}