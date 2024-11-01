import { ReactNode } from "react";
import { useAuth } from "../contexts";
import "../css/forms.css";
import { removeUser } from "../api.ts";

export function MyAccountPage(): ReactNode {
  const userContext = useAuth();

  async function handleRemoveAccount(){
    try {
      const removeUserResponse = await removeUser(userContext.username);
      console.log(removeUserResponse);
      userContext.logout();
      
    } catch(error) {
      console.log(error);
    }
  }
  return (
    <div className="myAccountPage">
      
      <button onClick={handleRemoveAccount}>Remove account</button>
      
    </div>

  );
}