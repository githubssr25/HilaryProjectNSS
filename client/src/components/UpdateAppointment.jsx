import { useContext, useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { GlobalContext } from './GlobalContext';
import { Link } from 'react-router-dom'; // Import Link from react-router-dom

export const UpdateAppointments = () => {
  const { appointmentId } = useParams(); // Get the appointmentId from route params
  const { appointments, stylists, services } = useContext(GlobalContext); // Fetch data from GlobalContext
  const [appointment, setAppointment] = useState(null); // State to hold the appointment details
  const [updatedTime, setUpdatedTime] = useState(''); // State to hold the updated time
  const [selectedServiceOptions, setSelectedServiceOptions] = useState([]);
  const [chosenStylistId, setChosenStylistId] = useState(0);

  const activeStylists = stylists.filter(stylist => stylist.isActive);

// Fetch the appointment details when the component mounts
useEffect(() => {
    const foundAppointment = appointments.find(
      (appt) => appt.appointmentId === parseInt(appointmentId)
    );
    if (foundAppointment) {
      setAppointment(foundAppointment);
      setUpdatedTime(foundAppointment.timeOf); // Pre-fill the current time
    }
  }, [appointmentId, appointments]);

  const handleTimeChange = (e) => {
    setUpdatedTime(e.target.value); // Update the time state when the user changes the input
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(`Updated time for appointment ${appointmentId}:`, updatedTime);
    // Add logic to submit the updated time to the backend
  };

  const handleChosenStylistChange = (e) =>{
    e.preventDefault();
    setChosenStylistId(e.target.value);
  }

  if (!appointment) {
    return <p>Loading appointment details...</p>;
  }

  const handleCancelToggle = (event) => {
    const fullId = event.target.id // e.g., "eachAppointment-123"
    const appointmentId = fullId.split('-')[1];


    //map iterates over the original array and returns a new array where each element is the result of the callback function you provide.

    const ourAppointment = appointments.map((eachAppt) => {
        if(eachAppt.appointmentId == appointmentId){
            return {...eachAppt,
                isChecked: eachAppt.isChecked
            };
        } else {
            return eachAppt;
        }
    });
     
//    while map creates a new array, the objects within that array are still references to the original objects unless you 
//    explicitly create new objects for them. React relies on reference changes to detect state updates, and directly
//     modifying properties of the original object would violate React's immutability principle. 
//. map Creates a New Array but Not New Objects.The map function creates a new array by iterating over the original array and applying the transformation you specify
// However, unless you explicitly create a new object, the objects in the new array are just references to the objects in the original array.

// BELOW IS WRONG 
// const updatedAppointments = appointments.map((eachAppt) => {
//     if (eachAppt.appointmentId == appointmentId) {
//       eachAppt.isChecked = !eachAppt.isChecked; // Mutating the original object
//     }
//     return eachAppt; // Returns the same object
//   });
  
// appointments and updatedAppointments will now share references for all the unchanged objects.
// React won't detect the change because the reference for the modified object is the same as the original.

   

// In React, state is immutable, meaning you should not directly modify existing state objects or arrays. 
// Instead, you need to create a new object or array with the updated values and then use the setState 
// function (like setAppointments) to update the state. This immutability is a core principle of React and ensures predictable rendering behavior.

setAppointment(ourAppointment);

  };

  const handleServiceToggle = (serviceId) => {
    setSelectedServiceOptions((prevSelected) => {
      if (prevSelected.includes(serviceId)) {
        // Remove the service ID if it’s already selected
        return prevSelected.filter((id) => id !== serviceId);
      } else {
        // Add the service ID if it’s not selected
        return [...prevSelected, serviceId];
      }
    });
  };

//CHECK later this BELOW MIGHT BE RIGHT 12-3 4:!2 BUT IT ALSO MIGHT BE LOGIC YOU WANT TO JUST DO IN OVERAL LSUBMIT YO UARE BASICALLY
// IN THIS BELOW DIRECTLY ADDING THE SERVICEIDS TO THE UPDATEAPPOINTMENTDTO WE ARE BASICALLY CREATING ON THIS FRONT END 
// const handleServiceToggle = (serviceId) => {
//     setAppointment((prevAppointment) => {
//       const updatedServiceIds = prevAppointment.UpdatedServiceIds || []; // Initialize if undefined
  
//       if (updatedServiceIds.includes(serviceId)) {
//         // Remove the service ID if it’s already selected
//         return {
//           ...prevAppointment,
//           UpdatedServiceIds: updatedServiceIds.filter((id) => id !== serviceId),
//         };
//       } else {
//         // Add the service ID if it’s not selected
//         return {
//           ...prevAppointment,
//           UpdatedServiceIds: [...updatedServiceIds, serviceId],
//         };
//       }
//     });
//   };
  



  return (
    <div>
      <h1>Update Appointment</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="time">Appointment Time:</label>
          <input
            type="datetime-local"
            id="time"
            value={updatedTime}
            onChange={handleTimeChange}
            required
          />
        </div>

        <div>
            <label> Do you want to cancel your appointment </label>
            <input
            type="checkbox"
            id={`eachAppointment-${appointment.appointmentId}`}
            //not event.target.value becuase we are using checked and apparently we use check almost always over value for chckboxes
            checked={appointment.isCancelled} // checked is read-only. It reflects the current state of the 
            //appointment.isCancelled property but does not inherently update or toggle the state by itself. This is why you need an onChange handler
            onChange={() => handleCancelToggle(appointment.appointmentId)}

            />


        </div>


        <div>
        <label htmlFor="stylist"> Update Your Chosen Stylist </label>
          <select
          id="stylist"
          value={chosenStylistId}
          onChange={handleChosenStylistChange}
          >
             <option value="" disabled>
              Select Which Stylist You Prefer
            </option>
            {activeStylists.map(eachStylist => (
                <option key={eachStylist.stylistId} value={eachStylist.stylistId}>
                    Name: {eachStylist.name} ID: {eachStylist.stylistId}
                </option>

            ))}
          </select>
        </div>
      <div>
        <label> Choose Services You Want </label>
        {services.map((eachService) => (
        <>
        <input
        type="checkbox"
        id={`eachService-${eachService.serviceId}`}
        value={eachService.serviceId}
        checked={selectedServiceOptions.includes(eachService)}
        onChange={() => handleServiceToggle(eachService)}
        />
        <label htmlFor={`eachService-${eachService.serviceId}`} >
           Service Name {eachService.name} 
           Service Price {eachService.price}
           {eachService.durationMinutes} Min
        </label>
        </>
    ))}
     </div>
     <button type="submit">Update Time</button>
      </form>
    </div>
  );
};