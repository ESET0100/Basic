import { useState } from "react";

// Child component for Increment
function IncrementButton({ onIncrement }) {
  return (
    <button onClick={onIncrement}>Increment</button>
  );
}

// Child component for Decrement
function DecrementButton({ onDecrement }) {
  return (
    <button onClick={onDecrement}>Decrement</button>
  );
}

// Parent component
function ParentComponent() {
  const [count, setCount] = useState(0);

  const handleIncrement = () => {
    setCount(count + 1);
    console.log("Increment clicked");
  };

  const handleDecrement = () => {
    setCount(count - 1);
    console.log("Decrement clicked");
  };

  return (
    <div>
      <h2>Count: {count}</h2>
      <IncrementButton onIncrement={handleIncrement} />
      <DecrementButton onDecrement={handleDecrement} />
    </div>
  );
}

export default ParentComponent;