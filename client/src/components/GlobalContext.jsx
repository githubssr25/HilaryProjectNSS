import { createContext, useState, useEffect } from 'react';
import PropTypes from 'prop-types';

// Import API methods
import { getAllAppointments, createAppointment, updateAppointment, deleteAppointment } from '../data/appointmentData';
import { getAllCustomers, createCustomer, deleteCustomer } from '../data/customerData';
import { getAllServices } from '../data/serviceData';
import {getAllStylists} from '../data/stylistData';

export const GlobalContext = createContext();

export const GlobalProvider = ({ children }) => {
  const [appointments, setAppointments] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [services, setServices] = useState([]);
  const [stylists, setStylists] = useState([]);

    // Fetch initial data
  // Fetch initial data
  const fetchAppointments = async () => {
    try {
      const fetchedAppointments = await getAllAppointments();
      console.log("Fetched Appointments in GlobalProvider:", fetchedAppointments);
      setAppointments(fetchedAppointments);
    } catch (error) {
      console.error("Error fetching appointments:", error);
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        await fetchAppointments(); // Fetch appointments initially
        const fetchedCustomers = await getAllCustomers();
        console.log("Fetched Customers in GlobalProvider:", fetchedCustomers);// Debug log// Debug log
        const fetchedServices = await getAllServices();
        console.log("Fetched Services in GlobalProvider:", fetchedServices); // Debug log
        const fetchedStylists = await getAllStylists(); // Fetch stylists
        console.log("Fetched Stylists in GlobalProvider:", fetchedStylists); // Debug log
        setCustomers(fetchedCustomers);
        setServices(fetchedServices);
        setStylists(fetchedStylists); // Set stylists state
      } catch (error) {
        console.error("Error during fetchData in GlobalProvider:", error); // Log any error
      }
    };
    fetchData();
  }, []);

   // Context value to provide both data and manipulation methods
   const contextValue = {
    appointments,
    customers,
    services,
    stylists, // Add stylists to context
    setAppointments,
    setCustomers,
    setServices,
    setStylists, // Add setStylists for external updates
    createAppointment,
    updateAppointment,
    deleteAppointment,
    createCustomer,
    deleteCustomer,
    refreshAppointments: fetchAppointments, // Expose refreshAppointments
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
