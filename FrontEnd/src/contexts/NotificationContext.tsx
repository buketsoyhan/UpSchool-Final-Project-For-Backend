import React, { createContext, useState } from 'react';

type NotificationType = 'Email' | 'InApp' | 'None';

export const NotificationContext = createContext<{
  notificationType: NotificationType;
  setNotificationType: (type: NotificationType) => void;
}>({
  notificationType: 'None',
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  setNotificationType: () => {},
});

interface NotificationProviderProps {
  children: React.ReactNode;
}

export const NotificationProvider: React.FC<NotificationProviderProps> = ({ children }) => {
  const [notificationType, setNotificationType] = useState<NotificationType>('None');

  return (
    <NotificationContext.Provider value={{ notificationType, setNotificationType }}>
      {children}
    </NotificationContext.Provider>
  );
};
