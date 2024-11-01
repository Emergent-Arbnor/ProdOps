import { Outlet, useLocation } from 'react-router-dom';
import './css/App.css';
import { AuthProvider } from "./contexts";
import { Header, Footer } from "./components";
import { useEffect } from 'react';

export function App() {
  /*
  const location = useLocation(); // Hook to get the current route
  useEffect(() => {
    const logs = JSON.parse(localStorage.getItem('logs') || '[]');
    console.log(logs);
    
  }, [location])
  */
  return (
    <>
      <AuthProvider>
        <Header />
        <Outlet />
        <Footer />
      </AuthProvider>
    </>
    
      
  )
}

export default App
