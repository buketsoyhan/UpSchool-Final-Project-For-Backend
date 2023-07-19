import React from 'react';

interface OrdersProps {
  handleOpenModal: () => void;
}

const Orders: React.FC<OrdersProps> = ({ handleOpenModal }) => {
  return (
    <div>
      <h1>Orders</h1>
      
      <button onClick={handleOpenModal}>Open Modal</button>
    </div>
  );
};

export default Orders;