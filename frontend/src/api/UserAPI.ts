import { BASE_URL } from "./api_config";
import { IUser } from "../interfaces/UserInterface.ts";
import { json } from "react-router-dom";

export async function registerUser(newUser: IUser) {
  console.log(JSON.stringify(newUser));
  let response = await fetch(`${BASE_URL}/user/new`,
    {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(newUser)
    }
  );
  
  const responseJSON = await response.json();

  if (!response.ok)
  {
    throw new Error(responseJSON.message);
  }

  return responseJSON.message;
  
}

export async function removeUser(userToRemove: string) {
  let response = await fetch(`${BASE_URL}/user/remove`,
    {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(userToRemove)
    }
  );
  
  const responseJSON = await response.json();

  if (!response.ok)
  {
    throw new Error(responseJSON.error);
  }

  return responseJSON.message;
  
}


export async function authenticateUser(userToAuthenticate: IUser) {
  let response = await fetch(`${BASE_URL}/user/authenticate`,
    {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(userToAuthenticate)
    }
  );
  
  const responseJSON = await response.json();

  if (!response.ok)
  {
    throw new Error(responseJSON.error);
  }

  return responseJSON;
}