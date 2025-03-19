export interface IAuthContext {
  isLoggedIn: boolean;
  login: (username: string, isAdmin: boolean) => void;
  logout: () => void;
  username: string;
  isAdmin: boolean;
}