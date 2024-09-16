import React, { useEffect, useState } from 'react';
import { getCustomers, createCustomer, updateCustomer, deleteCustomer } from '../services/customerService';
import '../assets/header.css';
import '../assets/footer.css';

const CustomerPage = () => {
  const [customers, setCustomers] = useState([]);
  const [newCustomer, setNewCustomer] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    address: {
      addressLine1: '',
      addressLine2: '',
      city: '',
      state: '',
      postalCode: '',
      country: ''
    }
  });

  const [selectedCustomerId, setSelectedCustomerId] = useState(null); // GÜNCELLEME MODU İÇİN MÜŞTERİ ID'Sİ

  useEffect(() => {
    // Müşteriler ilk yüklendiğinde getir
    fetchCustomers();
  }, []);

  const fetchCustomers = async () => {
    try {
      const response = await getCustomers();
      if (Array.isArray(response.data)) {
        setCustomers(response.data);
      } else {
        console.error('Data is not an array:', response);
        setCustomers([]); // Yanlış veri tipinde boş dizi döndür
      }
    } catch (error) {
      console.error('Error fetching customers:', error);
      setCustomers([]); // Hata durumunda boş dizi
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    if (name in newCustomer.address) {
      setNewCustomer(prevState => ({
        ...prevState,
        address: {
          ...prevState.address,
          [name]: value
        }
      }));
    } else {
      setNewCustomer({
        ...newCustomer,
        [name]: value
      });
    }
  };

  const handleCreateCustomer = async () => {
    try {
        if (selectedCustomerId) {
            await updateCustomer({ ...newCustomer, id: selectedCustomerId });
            setSelectedCustomerId(null); // Clear after update
        } else {
            await createCustomer(newCustomer);
        }
        fetchCustomers(); // Refresh customer list
        resetForm(); // Reset the form
    } catch (error) {
        console.error('Error creating/updating customer:', error);
    }
};


  const handleUpdateCustomer = (customer) => {
    setSelectedCustomerId(customer.id); // GÜNCELLEME MODUNA GİR
    setNewCustomer({
      firstName: customer.firstName,
      lastName: customer.lastName,
      email: customer.email,
      password: '',
      address: customer.address || {
        addressLine1: '',
        addressLine2: '',
        city: '',
        state: '',
        postalCode: '',
        country: ''
      }
    });
  };

  const handleDeleteCustomer = async (id) => {
    try {
      await deleteCustomer(id);
      fetchCustomers(); // Listeyi yenile
    } catch (error) {
      console.error('Error deleting customer:', error);
    }
  };

  const resetForm = () => {
    setNewCustomer({
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      address: {
        addressLine1: '',
        addressLine2: '',
        city: '',
        state: '',
        postalCode: '',
        country: ''
      }
    });
    setSelectedCustomerId(null);
  };

  return (
    <div className="customer-page">
      <h1>Customer Management</h1>

      <div className="form-section">
        <h2>{selectedCustomerId ? 'Update Customer' : 'Create Customer'}</h2>
        <input
          type="text"
          name="firstName"
          placeholder="First Name"
          value={newCustomer.firstName}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="lastName"
          placeholder="Last Name"
          value={newCustomer.lastName}
          onChange={handleInputChange}
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={newCustomer.email}
          onChange={handleInputChange}
        />
        <input
          type="password"
          name="password"
          placeholder="Password"
          value={newCustomer.password}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="addressLine1"
          placeholder="Address Line 1"
          value={newCustomer.address.addressLine1}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="addressLine2"
          placeholder="Address Line 2"
          value={newCustomer.address.addressLine2}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="city"
          placeholder="City"
          value={newCustomer.address.city}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="state"
          placeholder="State"
          value={newCustomer.address.state}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="postalCode"
          placeholder="Postal Code"
          value={newCustomer.address.postalCode}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="country"
          placeholder="Country"
          value={newCustomer.address.country}
          onChange={handleInputChange}
        />
        <button onClick={handleCreateCustomer}>
          {selectedCustomerId ? 'Update Customer' : 'Create Customer'}
        </button>
        <button onClick={resetForm}>Reset</button>
      </div>

      <div className="customer-list">
        <h2>Customer List</h2>
        {customers.length > 0 ? (
          <ul>
            {customers.map(customer => (
              <li key={customer.id}>
                {customer.firstName} {customer.lastName} ({customer.email})
                <div className="action-buttons">
                  <button className="update-btn" onClick={() => handleUpdateCustomer(customer)}>Update</button>
                  <button className="delete-btn" onClick={() => handleDeleteCustomer(customer.id)}>Delete</button>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p>No customers available.</p>
        )}
      </div>
    </div>
  );
};

export default CustomerPage;
