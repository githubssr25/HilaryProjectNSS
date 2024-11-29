import { useContext } from 'react';
import { GlobalContext } from './GlobalContext';
import { createContext, useState, useEffect } from 'react';


export const CreateAppointment = () => {
    const { customers, services, createAppointment } = useContext(GlobalContext);

    const [listOfServiceId, setListOfServiceId] = useState([]);

    const onSubmitForm = async (event) => {
        event.preventDefault();
        // Handle form submission logic here
    };

    console.log("Current customers and services from context:", customers, services);

const addServiceIdToList = (event, serviceId) => {
  if (event.target.checked) {
    setListOfServiceId((prevList) => [...prevList, serviceId]);
  } else {
    setListOfServiceId((prevList) => prevList.filter((eachId) => eachId !== serviceId));
  }
};



return (
<>
<form onSubmit={onSubmitForm}>
    <label> Create A New Appointment </label>


    <div>
        <label htmlFor="customer"> Select Customer </label>
    <select id="customer">
    <option value="" disabled>
        Select Which User You Are
    </option>
    {customers.map((indCustomer) => (
        <option key={indCustomer.customerId} value={indCustomer.customerId}>
           Name: {indCustomer.name} ID: {indCustomer.customerId}
        </option>
        //    <li> </li>
    ))}
    </select>
    </div>

    <div>
        <label> Select Services You Want </label>
        <div>
            {services.map((eachService) => (
                <div key={eachService.serviceId}>
                    <input
                     type="checkbox"
                     id={`service-${eachService.serviceId}`}
                     value={eachService.serviceId}
                     onChange={(e) => addServiceIdToList(e, eachService.serviceId)}
                    />
                    <label
                    htmlFor={`service-${eachService.serviceId}`}
                    style={{ marginLeft: "8px", display: "inline-block" }}
                    >
                        Name: {eachService.name} Price: {eachService.price} Duration: {eachService.durationMinutes} mins
                    </label>

                </div>
            ))};


        </div>

    </div>







    <button type="submit"> Click Here to Create Appointment</button>
</form>





</>
)
}