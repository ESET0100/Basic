import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

// Reusable Button component
function Button({ label, onClick }) {
  return <button onClick={onClick}>{label}</button>
}
 
export default function App() {
  // Function to increment
  const increment = () => {
    console.log("Increment clicked");
    const element = document.getElementById('counter');
    let curr = parseInt(element.textContent);
    element.textContent = curr + 1;
  };
 
  // Function to decrement
  const decrement = () => {
    console.log("Decrement clicked");
    const element = document.getElementById('counter');
    let curr = parseInt(element.textContent);
    element.textContent = curr - 1;
  };
 
  return (
<div>
<h1 id="counter">0</h1>
<div>
<Button label="Increment" onClick={increment} />
<Button label="Decrement" onClick={decrement} />
</div>
</div>
  );
}