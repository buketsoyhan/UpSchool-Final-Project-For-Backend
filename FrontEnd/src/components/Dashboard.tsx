import React, {useState} from 'react';
import { Link as RouterLink } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Button, makeStyles, FormControl, FormLabel, RadioGroup, FormControlLabel, Radio } from '@material-ui/core';

const useStyles = makeStyles({
  title: {
    flexGrow: 1,
  },
  appBar: {
    backgroundColor: '#7895CB', 
  },
  formContainer: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    marginTop: 40,
  },
  formInput:{
    marginTop:10,
    height:25,
    fontSize:14,
  },
  formControl: {
    marginBottom: 20,
  },
  formButton:{
    display:'flex',
    margin:'auto',
  }
});

const Dashboard: React.FC = () => {
  const classes = useStyles();

  const [productCount, setProductCount] = useState('');
  const [selectedOption, setSelectedOption] = useState('');

  const handleProductCountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const numericValue = event.target.value.replace(/[^0-9]/g, ''); 
    setProductCount(numericValue);
  };

  const handleOptionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSelectedOption(event.target.value);
  };

  const handleFormSubmit = (event: React.FormEvent) => {
    event.preventDefault();

    // Verileri işleme ve API isteği yapma işlemleri burada gerçekleştirilir
    // Örnek olarak, console'a yazdırıyoruz
    console.log('Product Count:', productCount);
    console.log('Selected Option:', selectedOption);

    // API isteği ve verilerin işlenmesi burada yapılabilir
    // ...

    // İşlem tamamlandıktan sonra formu sıfırla
    setProductCount('');
    setSelectedOption('');
  };

  return (
    <div>
      <AppBar position="static" className={classes.appBar}>
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            Dashboard
          </Typography>
          <Button color="inherit" component={RouterLink} to="/orders">Orders</Button>
          <Button color="inherit" component={RouterLink} to="/users">Users</Button>
          <Button color="inherit" component={RouterLink} to="/settings">Settings</Button>
        </Toolbar>
      </AppBar>
      <div>
        <div className={classes.formContainer}>
          <form onSubmit={handleFormSubmit}>
            <FormControl className={classes.formControl}>
              <FormLabel>How many products do you want?</FormLabel>
              <input className={classes.formInput}
                type="text"
                value={productCount}
                onChange={handleProductCountChange}
                inputMode="numeric"
                pattern="[0-9]*" />
            </FormControl>
            <br/>
            <FormControl className={classes.formControl} component="fieldset">
              <FormLabel component="legend">What types of products?</FormLabel>
              <RadioGroup value={selectedOption} onChange={handleOptionChange}>
                <FormControlLabel value="all" control={<Radio />} label="All" />
                <FormControlLabel value="discounted" control={<Radio />} label="On Sale" />
                <FormControlLabel value="regular" control={<Radio />} label="Regular Price" />
              </RadioGroup>
            </FormControl>
            <br/>
            <Button className={classes.formButton} type="submit" variant="contained" color="primary">
              Create
            </Button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;