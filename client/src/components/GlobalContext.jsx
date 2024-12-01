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

  const fetchStylists = async () => {
    try {
      const fetchedStylists = await getAllStylists();
      console.log("Fetched Stylists:", fetchedStylists); // Debug log
      setStylists(fetchedStylists); // Update the state
      return fetchedStylists; // Return for optional use
    } catch (error) {
      console.error("Error fetching stylists:", error);
      return []; // Return empty array on error
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        await fetchAppointments(); // Fetch appointments initially
        const fetchedCustomers = await getAllCustomers();
        console.log("Fetched Customers in GlobalProvider:", fetchedCustomers); // Debug log
        const fetchedServices = await getAllServices();
        console.log("Fetched Services in GlobalProvider:", fetchedServices); // Debug log
        const fetchedStylists = await fetchStylists(); // Fetch stylists
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
    stylists,
    setAppointments,
    setCustomers,
    setServices,
    setStylists,
    fetchStylists, // Expose fetchStylists for manual refresh
    refreshAppointments: fetchAppointments,
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