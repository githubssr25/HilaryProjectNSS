// import { useState } from 'react'
import { Routes, Route } from 'react-router-dom';
import NavBar from './components/Navbar';
import Home from './components/Home';
import {Appointments} from './components/Appointments';
import Customers from './components/Customers';
import { GlobalProvider } from './components/GlobalContext';






function App() {
  return (
    <GlobalProvider>
      <div>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/appointments" element={<Appointments />} />
          <Route path="/customers" element={<Customers />} />
        </Routes>
      </div>
    </GlobalProvider>
  );
}

export default App;
