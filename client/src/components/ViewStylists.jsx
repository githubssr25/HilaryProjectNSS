import { useContext, useState, useEffect } from "react";
import { GlobalContext } from './GlobalContext';
import { deactivateStylist } from "../data/stylistData";

export const ViewAllStylists = () => {
  const { stylists, setStylists, fetchStylists } = useContext(GlobalContext); 
  const [message, setMessage] = useState("");
  const [activeStylists, setActiveStylists] = useState([]); // Default to empty array
  const [deactivateStylistId, setDeactivateStylistId] = useState("");

  console.log("what are the stylists in ViewAllStylists", stylists);

  // maybe we need idk 
  // useEffect(() => {
  //   const loadStylists = async () => {
  //     if (!stylists || stylists.length === 0) {
  //       const fetchedStylists = await fetchStylists();
  //       setStylists(fetchedStylists); // Ensure stylists are set in the context
  //     }
  //   };

  //   loadStylists();
  // }, [stylists, setStylists, fetchStylists]); 

  useEffect(() => {
    // Update activeStylists whenever stylists change
    if (stylists.length > 0) {
      const filteredStylists = stylists.filter((eachStylist) => eachStylist.isActive);
      setActiveStylists(filteredStylists);
    }
  }, [stylists]); // Depend only on `stylists`

  
  const submitDeactivate = async () => {
    const didWeDeactivate = deactivateStylist(parseInt(deactivateStylistId));
    if(didWeDeactivate){
      const ourStylist = stylists.find(eachStylist => parseInt(eachStylist.stylistId) == parseInt(deactivateStylistId));
      setMessage(`we successfully deactivated stylist with ID ${ourStylist.stylistId} with name ${ourStylist.name} `)
    }


      // Reset the input field for stylist ID
      setDeactivateStylistId("");
      await fetchStylists();
  }

  const handleDeactivate = (e) => {
    e.preventDefault();
    setDeactivateStylistId(e.target.value);
  };

  return (
    <>
    <form onSubmit={submitDeactivate}>
      <label htmlFor="stylistId">Stylist to Deactivate:</label>
      <select 
      id="stylist"
      value={deactivateStylistId}
      onChange={(e) => handleDeactivate(e)}
      >
        <option value="" disabled>
          Select Which Stylist To Deactivate 
        </option>
        {activeStylists.map((eachStylist) => (
          <option
          key={eachStylist.stylistId}
          value={eachStylist.stylistId}
          >
          Name: {eachStylist.name} Id: {eachStylist.stylistId}
          </option>
        ))};
  </select>
      <button type="submit">Click Here to Deactivate Stylist</button>
      {message && <p>{message}</p>}
    </form>

<div>
<h3>All Stylists</h3>
{message && <p>{message}</p>}
<ul>
  {stylists
    .filter((stylist) => stylist.isActive) // Only show active stylists
    .map((stylist) => (
      <li key={stylist.id}>
        <strong>Name:</strong> {stylist.name} <br />
        <strong>Is Active:</strong> {stylist.isActive ? "Yes" : "No"} <br />
      </li>
    ))}
</ul>
</div>

<div>
   {/* Display Message */}
   {message && (
        <div className={`message ${message.type}`}>
          <p>{message.text}</p>
        </div>
      )}
</div>
</>
  )
};


//<input
// type="number"
// id="stylistId"
// value={deactivateStylistId}
// onChange={(e) => setDeactivateStylistId(e.target.value)}
// required
// />