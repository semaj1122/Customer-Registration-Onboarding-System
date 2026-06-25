import { useRef, useState } from "react";
import axios from "axios";
import SignatureCanvas from "react-signature-canvas";

function App() {
  const sigRef = useRef();

  const [customerDetails, setCustomerDetails] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  });
  const [errorMessage, setErrorMessage] = useState("");

  const [message, setMessage] = useState({statusCode:200, message:""});

  const handleChange = (e) => {
    setCustomerDetails({
      ...customerDetails,
      [e.target.name]: e.target.value
    });
  };

  const clearSignature = () => {
    sigRef.current.clear();
  };

  const submit = async () => {
    try {
      if (sigRef.current.isEmpty()) {
        setMessage({statusCode:400, message:"Signature is required."});
        return;
      }

      const signature =sigRef.current.getCanvas().toDataURL("image/png");

      await axios.post(
        "https://localhost:7210/api/customers",
        {
          ...customerDetails,
          signature
        }
      );

      setMessage({statusCode:200, message:"Customer registered successfully."});

      setCustomerDetails({
        firstName: "",
        lastName: "",
        email: "",
        phoneNumber: ""
      });

      clearSignature();
    } catch (error) {
      if (error.response?.status === 400) {

      const errors = error.response.data.errors;

      const messages = Object.values(errors)
        .flat()
        .join(", ");

      setErrorMessage(messages);
    }
    else {
      setErrorMessage("An unexpected error occurred.");
    }
    console.error(errorMessage);
      setMessage({statusCode:400, message:"Registration failed."});
    }
  };

  return (
    <div style={{ padding: "20px", maxWidth: "600px" }}>
      <h1>Customer Onboarding</h1>

      <input
        name="firstName"
        placeholder="First Name"
        value={customerDetails.firstName}
        onChange={handleChange}
      />

      <br /><br />

      <input
        name="lastName"
        placeholder="Last Name"
        value={customerDetails.lastName}
        onChange={handleChange}
      />

      <br /><br />

      <input
        name="email"
        placeholder="Email"
        value={customerDetails.email}
        onChange={handleChange}
      />

      <br /><br />

      <input
        name="phoneNumber"
        placeholder="Phone Number"
        value={customerDetails.phoneNumber}
        onChange={handleChange}
      />

      <br /><br />

      <p>Signature</p>

      <SignatureCanvas
        ref={sigRef}
        canvasProps={{
          width: 500,
          height: 200,
          style: {
            border: "1px solid black"
          }
        }}
      />

      <br />

      <button onClick={clearSignature}>
        Clear Signature
      </button>

      <button
        onClick={submit}
        style={{ marginLeft: "10px" }}
      >
        Register
      </button>

      {message && (
        <>
          <br /><br />
          <span style={{ color: message.statusCode === 200 ? "green" : "red" }}>
            <strong>{message.message}</strong>
          </span>
        </>
      )}
    </div>
  );
}

export default App;