import { useState } from "react";

function LoginComponent() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleUsernameChange = (event) => {
    setUsername(event.target.value);
    console.log("Username:", event.target.value);
  };

  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
    console.log("Password:", event.target.value);
  };

  const handleLogin = (event) => {
    event.preventDefault();
    console.log("Login clicked");
    console.log("Username:", username);
    console.log("Password:", password);
    // Add login logic here if needed
  };

  const handleReset = () => {
    setUsername("");
    setPassword("");
    console.log("Reset clicked");
  };

  return (
    <form onSubmit={handleLogin}>
      <div>
        <input
          type="text"
          value={username}
          onChange={handleUsernameChange}
          placeholder="Username"
        />
      </div>
      <div>
        <input
          type="password"
          value={password}
          onChange={handlePasswordChange}
          placeholder="Password"
        />
      </div>
      <button type="submit">Login</button>
      <button type="button" onClick={handleReset}>Reset</button>
    </form>
  );
}

export default LoginComponent;