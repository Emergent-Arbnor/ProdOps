import { ReactNode, useRef } from "react";
import { userAPI } from "../api";
import { IUser } from "../interfaces/interfaces.ts";
import "../css/forms.css";


export function RegisterPage(): ReactNode {
  const usernameRef = useRef<HTMLInputElement>(null!);
  const passwordRef = useRef<HTMLInputElement>(null!);
  const adminRef = useRef<HTMLInputElement>(null!);
  
  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const userToRegister : IUser = {
      username: usernameRef.current.value,
      password: passwordRef.current.value,
      isAdmin: adminRef.current.checked
    }

    try {
      const apiResponse = await userAPI("register", userToRegister);
      console.log(apiResponse);
    } catch(error){
      console.error(error);
    }
  }

  return (
    <div className="registerPage">
      <h2>REGISTER</h2>
      <form className="registerForm" onSubmit={handleSubmit}>
        <div className="inputContainer">
          <label htmlFor="regUsername">Username</label>
          <input type="text" ref={usernameRef} id="regUsername" required/>
        </div>

        <div className="inputContainer">
          <label htmlFor="regPassword">Password</label>
          <input type="password" ref={passwordRef} id="regPassword" required/>
        </div>

        <div className="adminContainer">
          <label htmlFor="regIsAdmin">Administrator</label>
          <input type="checkbox" ref={adminRef} id="regIsAdmin" defaultChecked={false} />
        </div>

        <button type="submit">Register</button>
      </form>
    </div>

  );
}