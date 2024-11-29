import { useContext } from 'react';
import { GlobalContext } from './GlobalContext';


export const Appointments = () => {
  const { appointments, createAppointment } = useContext(GlobalContext);

  const handleCreate = async () => {
    const newAppointment = { /* your new appointment data */ };
    await createAppointment(newAppointment);
  };

  return (
    <div>
      <h1>Appointments</h1>
      <button onClick={handleCreate}>Create Appointment</button>
      <ul>
  {appointments.map((appointment) => {
    console.log('rendering appointment:', appointment);
    return (
      <div key={appointment.appointmentId}>
          <li>Appointment Info</li>
          <li> AppointmentID: {appointment.appointmentId}</li>
        <li> Time Of Appointment {appointment.timeOf}</li>
        <li> Customer Name {appointment.customer.name} </li>
        <li> Has the Appointment Been Cancelled: {appointment.IsCancelled ? 'yes' : 'no'} </li>
        <li> Stylist for the Appointment: {appointment.stylist.name} </li>
       
      
      </div>
    );
  })}
</ul>

    </div>
  );
};
