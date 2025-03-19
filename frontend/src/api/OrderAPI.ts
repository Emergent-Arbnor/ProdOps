import { BASE_URL } from "./api_config.ts";

export async function orderArticles(articleA: string, articleB: string) {
  const response = await fetch(`${BASE_URL}/order/new`,
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