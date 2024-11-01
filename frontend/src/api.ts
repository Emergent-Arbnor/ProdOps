import { IUser } from "./interfaces/interfaces.ts";

export async function removeUser(username: string) {
  let response = await fetch('http://localhost:5000/api/removeUser',
    {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(username)
    }
  );

  const responseJSON = await response.json();

  if(!response.ok){
    throw new Error(responseJSON.message);
  }

  return responseJSON.message;
}

export async function userAPI(endpoint: string, user: IUser) {
  
  let response = await fetch(`http://localhost:5000/api/${endpoint}`,
    {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
    }
  );
  
  const responseJSON = await response.json();

  if(!response.ok){
    throw new Error(responseJSON.message);
  }

  if(endpoint == "register"){
    return responseJSON.message;
  }

  if(endpoint == "validateUser") {
    // Return user with IUser structure
    const mappedUser: IUser = {
      username: responseJSON.username,
      password: responseJSON.password,
      isAdmin: responseJSON.isadmin
    };

    return mappedUser;
  }
};

export async function orderArticles(articleA: string, articleB: string) {
  const response = await fetch('http://localhost:5000/api/orderArticles',
    {
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ articleA, articleB })
    }
  );

  
  const responseJSON = await response.json();

  if(!response.ok){
    throw new Error(responseJSON.message);
  }

  return responseJSON.message;
}