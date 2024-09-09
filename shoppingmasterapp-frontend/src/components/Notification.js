import React, { useState, useEffect } from 'react';

const Notification = ({ message, autoClose = true, duration = 3000 }) => {
  const [visible, setVisible] = useState(true);

  useEffect(() => {
    if (autoClose) {
      const timer = setTimeout(() => {
        setVisible(false);
      }, duration);
      return () => clearTimeout(timer);
    }
  }, [autoClose, duration]);

  if (!visible) return null;

  return (
    <div className="notification">
      <p>{message}</p>
      <button onClick={() => setVisible(false)}>Close</button>
    </div>
  );
};

export default Notification;
