import React, { useContext } from "react";
import ThemeProvider from "./context/ThemeProvider";
import { ThemeContext } from "./context/ThemeContext";
import './App.css';

function Content() {
  const { theme, toggleTheme } = useContext(ThemeContext);

  return (
    <div style={{ background: theme.background, color: theme.color, minHeight: "100vh", padding: "2rem" }}>
      <button onClick={toggleTheme}>Toggle Theme</button>
      <h2>Hello Students!</h2>
    </div>
  );
}

function App() {
  return (
    <ThemeProvider>
      <Content />
    </ThemeProvider>
  );
}

export default App;