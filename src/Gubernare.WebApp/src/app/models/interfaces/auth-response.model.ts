export interface AuthResponse {
  data: {
    token: string;
    id: string;
    name: string;
    email: string;
    roles: string[];
  };
  message: string;
  status: number;
  isSuccess: boolean;
  notifications: any;
}
