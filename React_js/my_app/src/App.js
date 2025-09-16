import React, { useState } from 'react';
import './App.css';

function App(props) {
  const containerStyle = {
    border: '2px solid black',
    padding: '20px',
    textAlign: 'center'
  };
  
  const [counter, setCounter] = useState(0);

  const incrementHandler = () => {
    console.log('Increment button clicked!');
    setCounter(counter + 1);
  }

  const decrementHandler = () => {
    console.log('Decrement button clicked!');
    setCounter(counter - 1);
  }
  
  return (
    <div className="container" style={containerStyle}>
      {props.name ? <p>{props.name}</p> : <p>No name</p>}
      <p style={{fontSize: '100px'}}>{counter}</p>
      <div>
        <button onClick={decrementHandler} style={{marginRight: '10px'}}>Decrement</button>
        <button onClick={incrementHandler}>Increment</button>
      </div>
    </div>
  );
}

export default App;