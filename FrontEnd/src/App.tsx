import React from 'react';
import { BrowserRouter as Router, Route, Link, Routes, Navigate } from 'react-router-dom';
import { Button, Modal, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, Paper } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';

import Dashboard from './components/Dashboard';
import Login from './components/Login';
import Orders from './components/Orders';
import Settings from './components/Settings';
import Users from './components/Users';

const useStyles = makeStyles({
  modal: {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    backgroundColor: '#fff',
    border: '2px solid #000',
    padding: 16,
  },
});

const App: React.FC = () => {
  const classes = useStyles();
  const [open, setOpen] = React.useState(false);
  const [orderEvents, setOrderEvents] = React.useState([]);
  const [isLoggedIn, setIsLoggedIn] = React.useState(false);

  const handleOpenModal = () => {
    setOpen(true);
  };

  const handleCloseModal = () => {
    setOpen(false);
  };

  React.useEffect(() => {
    const fetchOrderEvents = async () => {
      try {
        const response = await fetch('/api/order-events');
        const data = await response.json();
        setOrderEvents(data);
      } catch (error) {
        console.error('Error fetching order events:', error);
      }
    };

    fetchOrderEvents();
  }, []);

  const handleLogin = () => {
    setIsLoggedIn(true);
  };

  return (
    <Router>
      <div>
        <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="/login" element={<Login onLogin={handleLogin} />} />
          {isLoggedIn ? (
            <>
              <Route path="/dashboard" element={<Dashboard />} />
              <Route path="/orders" element={<Orders handleOpenModal={handleOpenModal} />} />
              <Route path="/settings" element={<Settings />} />
              <Route path="/users" element={<Users />} />
            </>
          ) : null}
        </Routes>

        <Modal open={open} onClose={handleCloseModal}>
          <div className={classes.modal}>
            <h2>Order Events</h2>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Date</TableCell>
                    <TableCell>Event</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {orderEvents.map((event: any) => (
                    <TableRow key={event.id}>
                      <TableCell>{event.date}</TableCell>
                      <TableCell>{event.event}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </div>
        </Modal>
      </div>
    </Router>
  );
};

export default App;
