import React, { useState } from 'react';
import { Button, TextField, Typography } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';

const BASE_URL = import.meta.env.VITE_API_URL;

const useStyles = makeStyles({
  container: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    height: '100vh',
  },
  form: {
    width: '100%',
    maxWidth: 400,
    padding: 16,
    backgroundColor: '#fff',
    border: '2px solid #000',
    borderRadius: 8,
  },
  submitButton: {
    marginTop: 16,
  },
});

const Login: React.FC<{ onLogin: () => void }> = ({ onLogin }) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const classes = useStyles();

  const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const handleLogin = () => {
    onLogin();
    window.location.href = `${BASE_URL}/Authentication/GoogleSignInStart`;
  };

  return (
    <div className={classes.container}>
      <form className={classes.form}>
        <Typography variant="h2" align="center">Login</Typography>
        <TextField
          label="Username"
          type="text"
          value={username}
          onChange={handleUsernameChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Password"
          type="password"
          value={password}
          onChange={handlePasswordChange}
          fullWidth
          margin="normal"
        />
        <Button
          variant="contained"
          color="primary"
          fullWidth
          className={classes.submitButton}
          onClick={handleLogin}
        >
          Sign in with Google
        </Button>
      </form>
    </div>
  );
};

export default Login;
