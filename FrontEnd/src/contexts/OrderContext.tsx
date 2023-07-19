import React, { createContext, useState } from 'react';

export interface Order {
  id: number;
  status: string;
  events: string[];
}

interface OrderProviderProps {
  children: React.ReactNode;
}

export const OrderContext = createContext<{ orders: Order[]; addOrder: (order: Order) => void }>({
  orders: [],
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  addOrder: () => {},
});

export const OrderProvider: React.FC<OrderProviderProps> = ({ children }) => {
  const [orders, setOrders] = useState<Order[]>([]);

  const addOrder = (order: Order) => {
    setOrders((prevOrders) => [...prevOrders, order]);
  };

  return <OrderContext.Provider value={{ orders, addOrder }}>{children}</OrderContext.Provider>;
};
