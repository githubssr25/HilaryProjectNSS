// import { useState } from 'react'
import { Routes, Route } from 'react-router-dom';
import NavBar from './components/Navbar';
import Home from './components/Home';
import {Appointments} from './components/Appointments';
import Customers from './components/Customers';
import { GlobalProvider } from './components/GlobalContext';
import {CreateAppointment} from './components/CreateAppointment';
import {GetBalance} from './components/GetBalance';
import {ViewAllStylists} from './components/ViewStylists';
import {UpdateAppointment} from './components/UpdateAppointment';






function App() {
  return (
    <GlobalProvider>
      <div>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/viewAppointments" element={<Appointments />} />
          <Route path="/customers" element={<Customers />} />
          <Route path="/createAppointment" element={<CreateAppointment />}/>
          <Route path="/getBalance" element={<GetBalance />} />
          <Route path="/viewAllStylists" element={<ViewAllStylists />} />
          <Route path="/updateAppointment/:appointmentId" element={<UpdateAppointment />} /> 
        </Routes>
      </div>
    </GlobalProvider>
  );
}

export default App;
