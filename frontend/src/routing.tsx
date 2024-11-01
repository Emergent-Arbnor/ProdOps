import { BrowserRouter, Route, Routes } from "react-router-dom";
import { LoginPage, RegisterPage, OrderPage, MyAccountPage, HomePage, DashboardPage } from "./pages";
import { App } from "./App.tsx";
import { ProtectedRoute } from "./components/ProtectedRoute.tsx";

export function Router(){
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />}>
          <Route element={<HomePage />} index />
          <Route element={<RegisterPage />} path="register" />
          <Route element={<LoginPage />} path="login" />


          <Route element={
                    <ProtectedRoute> 
                      <DashboardPage />
                    </ProtectedRoute> 
                    } 
                    path="/dashboard"
          />
          

          <Route element={
                    <ProtectedRoute requiresAdmin={true}> 
                      <OrderPage />
                    </ProtectedRoute> 
                    } 
                    path="/order"
          />

          <Route element={
                    <ProtectedRoute> 
                      <MyAccountPage />
                    </ProtectedRoute> 
                    } 
                    path="/myaccount" 
          />
        </Route>
      </Routes>
    </BrowserRouter>
  );
} 