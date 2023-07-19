import React, { createContext, useState } from 'react';

export const AuthContext = createContext<{ isAuthenticated: boolean; login: () => void; logout: () => void }>({
  isAuthenticated: false,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  login: () => {},
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  logout: () => {},
});

interface AuthProviderProps {
  children: React.ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const login = () => {
    // Perform login operation
    setIsAuthenticated(true);
  };

  const logout = () => {
    // Perform logout operation
    setIsAuthenticated(false);
  };

  return <AuthContext.Provider value={{ isAuthenticated, login, logout }}>{children}</AuthContext.Provider>;
};
