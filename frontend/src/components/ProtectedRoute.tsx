import { ReactNode } from "react";
import { useAuth } from "../contexts";
import { Navigate } from "react-router-dom";

interface ProtectedRouteProps {
  children: ReactNode;
  requiresAdmin?: boolean;
}

export function ProtectedRoute( props :ProtectedRouteProps) {
  const authContext = useAuth();
  // Default requiresAdmin to false
  const requiresAdmin = props.requiresAdmin ?? false;
  
  /*
  const logs = JSON.parse(localStorage.getItem('logs') || '[]');
  const message = `----------------------
  Now in Protected Route with the following data:
  authContext.isLoggedIn, ${authContext.isLoggedIn}
  requiresAdmin, ${requiresAdmin}
  authContext.isAdmin  , ${authContext.isAdmin}
  ----------------------`;
  logs.push(message);
  localStorage.setItem('logs', JSON.stringify(logs));
  */

  if(!authContext.isLoggedIn){
    return <Navigate to="/login" />;
  }

  
  if(requiresAdmin && !authContext.isAdmin){
    return <Navigate to="/dashboard" />;
  } 

  return <>{props.children}</>;  
}
