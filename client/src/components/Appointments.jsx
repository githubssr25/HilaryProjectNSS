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
        {appointments.map((appointment) => (
          <li key={appointment.id}>{appointment.time}</li>
        ))}
      </ul>
    </div>
  );
};
