import { createContext, useState, useEffect } from 'react';
import PropTypes from 'prop-types';

// Import API methods
import { getAllAppointments, createAppointment, updateAppointment, deleteAppointment } from '../data/appointmentData';
import { getAllCustomers, createCustomer, deleteCustomer } from '../data/customerData';
import { getAllServices } from '../data/serviceData';

export const GlobalContext = createContext();

export const GlobalProvider = ({ children }) => {
  const [appointments, setAppointments] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [services, setServices] = useState([]);

  // Fetch initial data
  useEffect(() => {
    const fetchData = async () => {
      const fetchedAppointments = await getAllAppointments();
      const fetchedCustomers = await getAllCustomers();
      const fetchedServices = await getAllServices();
      setAppointments(fetchedAppointments);
      setCustomers(fetchedCustomers);
      setServices(fetchedServices);
    };
    fetchData();
  }, []);

  // Context value to provide both data and manipulation methods
  const contextValue = {
    appointments,
    customers,
    services,
    setAppointments,
    setCustomers,
    setServices,
    createAppointment,
    updateAppointment,
    deleteAppointment,
    createCustomer,
    deleteCustomer,
  };

  return (
    <GlobalContext.Provider value={contextValue}>
      {children}
    </GlobalContext.Provider>
  );
};

GlobalProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
