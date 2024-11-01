import { ReactNode, useRef } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { IUser } from "../interfaces/interfaces.ts";
import { useAuth } from "../contexts";
import { userAPI } from "../api.ts";
import "../css/forms.css";

export function LoginPage(): ReactNode {
  const usernameRef = useRef<HTMLInputElement>(null!);
  const passwordRef = useRef<HTMLInputElement>(null!);
  const navigate = useNavigate();
  const location = useLocation();
  
  const userContext = useAuth();

  if(userContext.isLoggedIn){
    navigate("dashboard");
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    
    //Look whether the user exists in database.
    const userToAuthenticate: IUser =  {
      username: usernameRef.current.value,
      password: passwordRef.current.value
    }
      

    try {
      let user = await userAPI("validateUser", userToAuthenticate);
      userContext.login(user.username, user.isAdmin);
      navigate("/dashboard");

    } catch(error){
      console.error(error);
    }
  
  }

  return (
    <div className="loginPage">
      <h2>LOGIN</h2>
      <form onSubmit={handleSubmit}>
        <div className="inputContainer">
          <label htmlFor="loginUsername">Username</label>
          <input type="text" ref={usernameRef} />
        </div>

        <div className="inputContainer">
          <label htmlFor="loginPassword">Password</label>
          <input type="password" ref={passwordRef} />
        </div>

        <button type="submit">Login</button>
      </form>
    </div>
  );
}