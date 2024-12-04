import { useContext } from 'react';
import { GlobalContext } from './GlobalContext';
import { Link } from 'react-router-dom'; // Import Link from react-router-dom

export const Appointments = () => {
  const { appointments } = useContext(GlobalContext);

  return (
    <div>
      <h1>Appointments</h1>
      <ul>
        {appointments.map((appointment) => (
          <div key={appointment.appointmentId}>
            <li>Appointment Info</li>
            <li>Appointment ID: {appointment.appointmentId}</li>
            <li>Time Of Appointment: {appointment.timeOf}</li>
            <li>Customer Name: {appointment.customer.name}</li>
            <li>Has the Appointment Been Cancelled: {appointment.isCancelled ? 'Yes' : 'No'}</li>
            <li>Stylist for the Appointment: {appointment.stylist.name}</li>
            <Link to={`/updateAppointment/${appointment.appointmentId}`} >
            Click Here To Update Appointment
            </Link>

         
          </div>
        ))}
      </ul>
    </div>
  );
};
