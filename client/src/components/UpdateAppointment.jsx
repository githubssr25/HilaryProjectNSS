import { useContext, useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { GlobalContext } from './GlobalContext';
import {updateAppointment} from '../data/appointmentData'

export const UpdateAppointment = () => {
  const { appointmentId } = useParams(); // Get the appointmentId from route params
  const { appointments, stylists, services, refreshAppointments } = useContext(GlobalContext); // Fetch data from GlobalContext
  const [appointment, setAppointment] = useState({}); // State to hold the appointment details
  const [updatedTime, setUpdatedTime] = useState(""); // State to hold the updated time
  const [selectedServiceOptions, setSelectedServiceOptions] = useState([]);
  const [chosenStylistId, setChosenStylistId] = useState(0);
  const [isUpdateSuccessful, setIsUpdateSuccessful] = useState(false); // Track update success
  const activeStylists = stylists.filter((stylist) => stylist.isActive);

console.log("services and stylists", services, stylists);



//filter designed to return array and ALL MATCHES
//even if you only have 1 you are still getting back an array like this 
//Without [0], filteredStylist is still an array (e.g., [{ stylistId: 1, services: [1, 2, 3] }]), so you’d get an error if you tried to directly access services.
// remember our ind service is an object 

const filteredServices = stylists
.filter((stylist) => stylist.stylistId === parseInt(chosenStylistId))
.map((stylist) => stylist.services)[0]; // Grab the first result

console.log("what is value of filteredServices", filteredServices);



  useEffect(() => {
    console.log("Appointments from context:", appointments);
  }, [appointments]);
  
  // Fetch the appointment details when the component mounts
  useEffect(() => {
    if (appointments.length > 0) {
      const foundAppointment = appointments.find(
        (appt) => appt.appointmentId === parseInt(appointmentId, 10)
      );
  
      console.log("Found Appointment:", foundAppointment);
  
      if (foundAppointment) {
        setAppointment(foundAppointment);
        setUpdatedTime(foundAppointment.timeOf); // Pre-fill the time
      } else {
        console.error(`No appointment found for ID ${appointmentId}`);
      }
    } else {
      console.error("Appointments array is empty or not available.");
    }
  }, [appointmentId, appointments]);
  
  
   // Trigger refreshAppointments whenever an update is successful
   useEffect(() => {
    if (isUpdateSuccessful) {
      refreshAppointments();

          // Delay resetting isUpdateSuccessful to allow the success message to display
    const timer = setTimeout(() => {
      setIsUpdateSuccessful(false); // Reset after 10 seconds
    }, 10000); // 10 seconds timeout

    // Cleanup the timer if the component unmounts or re-renders
    return () => clearTimeout(timer);
       // Reset the success flag to prevent unnecessary calls
      // If you don’t reset it to false, then isUpdateSuccessful would remain true, and every time the component re-renders,
      // the useEffect would think it needs to trigger the refreshAppointments() function again.By resetting isUpdateSuccessful to false, you ensure the useEffect doesn't keep running when it’s no longer necessary.
    }
  }, [isUpdateSuccessful, refreshAppointments]);

  const handleTimeChange = (e) => {
    // console.log("value of selectedServiceOptions", selectedServiceOptions);
    setUpdatedTime(e.target.value); // Update the time state when the user changes the input
    // console.log("what is e target value in handleTimeChange", e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const ourAppointment = appointments.find(eachAppt => eachAppt.appointmentId == appointmentId);

    const ourUpdatedAnnouncementDTO = {
      AppointmentId: appointmentId,
      StylistId: chosenStylistId,
      TimeOf: updatedTime,
      IsCancelled: ourAppointment.isCancelled,
      UpdatedServiceIds: selectedServiceOptions
    }

   
  updateAppointment(appointmentId, ourUpdatedAnnouncementDTO)
  .then((ourUpdatedResponse) => {
    console.log("value of our ourUpdatedResponse", ourUpdatedResponse);
    setIsUpdateSuccessful(true); // Trigger the refresh
    refreshAppointments();

    console.log("Appointments after refresh:", appointments);
    const updatedAppt = appointments.find((appt) => appt.appointmentId == appointmentId);
    console.log("Updated appointment:", updatedAppt);
  })
  .catch((error) => {
    console.error("Failed to update appointment:", error);
  });
};

  const handleChosenStylistChange = (e) => {
    e.preventDefault();
    console.log("value of e.target.value for stylist", parseInt(e.target.value));
    setChosenStylistId(parseInt(e.target.value, 10)); // Convert to integer
  };

  const handleCancelToggle = () => {
    //we are merely acting on the prev appoinment state which is already teh appointment state we want to update
    //so no need to do find or filter and all that stuff or pass in an event or antying
    setAppointment((prevAppointment) => {
      if (!prevAppointment) return prevAppointment; // Safety check
  
      return {
        ...prevAppointment,
        isCancelled: !prevAppointment.isCancelled, // Toggle the cancellation state
      };
    });
  };
  
  
//OLD PREV WRONG WAY 
  // const handleCancelToggle = (ourAppointmentId) => {
 // const fullId = event.target.id; // e.g., "eachAppointment-123" //note im not going to us this im jus demonstrating

 //map iterates over the original array and returns a new array where each element is the result of the callback function you provide.

  //   const ourAppointment = appointments.map((eachAppt) => {
  //     if (eachAppt.appointmentId == ourAppointmentId) {
  //       return {
  //         ...eachAppt,
  //         isChecked: !eachAppt.isChecked, //toggles what ever current value of isChecked property is if its true !eachAppt.isChecked makes it false and VV
  //       };
  //     } else {
  //       return eachAppt; //   Return unchanged object for all other appointments
  //     }
  //   });

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

//KEY THNING ABOUVE IS THE MAP ABOVE IS RETURNING A NEW ARRAY SO ITS AN ARRAY OF ALL THE APPOINTMENTS NOT JUST ONE
//SO YOU CANT JUST SET SETAPPOINTMENT TO THAT YOU HAVE TO FILTER TO GET THE ONE YOU WANT
  //   const updatedAppointment = ourAppointment.find(
  //     (eachAppt) => eachAppt.appointmentId === appointmentId
  //   );

    
  //   setAppointment(updatedAppointment);
  // };

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

  return (
    <>
      <section>
  <h1>Current Appointment Info</h1>
  <article>
    <p>Customer Name: {appointment?.customer?.name || "Unknown"}</p>
    <p>Stylist For Appointment: {appointment?.stylist?.name || "Not Assigned"}</p>
    <p>Time Of Appointment: {appointment?.timeOf || "Not Scheduled"}</p>
    {appointment?.services?.length > 0 ? (
      appointment.services.map((eachService) => (
        <li key={eachService.serviceId}>
          Service Info
          <ul>Service Type: {eachService.name}</ul>
          <ul>Service Price: {eachService.price}</ul>
          <ul>Service Duration: {eachService.durationMinutes}</ul>
        </li>
      ))
    ) : (
      <p>No services assigned to this appointment.</p>
    )}
  </article>
</section>


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
              {activeStylists.map((eachStylist) => (
                <option
                  key={eachStylist.stylistId}
                  value={eachStylist.stylistId}
                >
                  Name: {eachStylist.name} ID: {eachStylist.stylistId}
                </option>
              ))}
            </select>
          </div>
       
          {chosenStylistId !== 0 && ( // Render services only when a stylist is chosen
          <div>
            <label>Choose Services You Want</label>
            <div className="services-container">
              {filteredServices.map((eachService) => {
                return (
                  <div className="service-item" key={eachService.serviceId}>
                    <input
                      type="checkbox"
                      id={`eachService-${eachService.serviceId}`}
                      value={eachService.serviceId}
                      checked={selectedServiceOptions.includes(eachService.serviceId)}
                      onChange={() => handleServiceToggle(eachService.serviceId)}
                    />
                    <label htmlFor={`eachService-${eachService.serviceId}`}>
                      Service Name: {eachService.name} | Service Price:{" "}
                      {eachService.price} | Duration: {eachService.durationMinutes} Min
                    </label>
                  </div>
                );
              })}
            </div>
          </div>
        )}

          <button type="submit">Update Appointment Info</button>
        </form>

        <div>
          { isUpdateSuccessful && (
            <h1> Successfully Updated Appointment</h1>
          )}
          </div>
      </div>
    </>
  );
};