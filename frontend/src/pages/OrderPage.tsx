import { ReactNode, useRef } from "react";
import { orderArticles } from "../api.ts"
import "../css/forms.css";

export function OrderPage(): ReactNode {
  const articleARef = useRef<HTMLInputElement>(null!);
  const articleBRef = useRef<HTMLInputElement>(null!);

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    try {
      const articleAToAdd = articleARef.current.value;
      const articleBToAdd = articleBRef.current.value;
      
      const orderResponse = await orderArticles(articleAToAdd, articleBToAdd);
      console.log(orderResponse);
    } catch(error) {
      console.log(error);
    }

  }

  return (
    <div className="orderPage">
      <h2>ORDER</h2>
      <form onSubmit={handleSubmit}>
        <div className="inputContainer">
          <label htmlFor="articleA">Article A</label>
          <input type="number" min="1" ref={articleARef} placeholder="Enter a positive integer."/>
        </div>

        <div className="inputContainer">
          <label htmlFor="articleB">Article B</label>
          <input type="number" min="1" ref={articleBRef} placeholder="Enter a positive integer."/>
        </div>

        <button type="submit">Order</button>
      </form>

    </div>

  );
}