import { createRoot } from 'react-dom/client';
import './css/index.css';
import { Router } from "./routing.tsx";

createRoot(document.getElementById('root')!).render(
  <Router />
)
