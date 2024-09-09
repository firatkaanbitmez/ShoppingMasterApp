import React, { useState } from 'react';

const Notification = ({ message }) => {
  const [visible, setVisible] = useState(true);

  if (!visible) return null;

  return (
    <div className="notification">
      <p>{message}</p>
      <button onClick={() => setVisible(false)}>Close</button>
    </div>
  );
};

export default Notification;
