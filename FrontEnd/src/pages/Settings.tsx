import React, { useContext } from 'react';
import { NotificationContext } from '../contexts/NotificationContext';

type NotificationType = 'Email' | 'InApp' | 'None';

const Settings: React.FC = () => {
  const { notificationType, setNotificationType } = useContext(NotificationContext);

  const handleNotificationChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setNotificationType(e.target.value as NotificationType);
  };

  return (
    <div>
      <h1>Settings</h1>
      <select value={notificationType} onChange={handleNotificationChange}>
        <option value="None">None</option>
        <option value="Email">Email</option>
        <option value="InApp">In-App</option>
      </select>
    </div>
  );
};

export default Settings;