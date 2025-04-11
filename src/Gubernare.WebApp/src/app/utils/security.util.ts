import {User} from '../models/user.model';

export class Security {
  private static isBrowser(): boolean {
    return typeof window !== 'undefined' && !!window.localStorage;
  }

  public static set(user: User, token: string) {
    if (this.isBrowser()) {
      const data = JSON.stringify(user);
      localStorage.setItem('guabernare', btoa(data));
      localStorage.setItem('guabernaretoken', token);
    }
  }

  public static setUser(user: User) {
    if (this.isBrowser()) {
      const data = JSON.stringify(user);
      localStorage.setItem('guabernareuser', btoa(data));
    }
  }

  public static setToken(token: string) {
    if (this.isBrowser()) {
      localStorage.setItem('guabernaretoken', token);
    }
  }

  public static getUser(): User | null {
    if (this.isBrowser()) {
      const data = localStorage.getItem('guabernareuser');
      if (data) {
        return JSON.parse(atob(data));
      }
    }
    return null;
  }

  public static getToken(): string | null {
    if (this.isBrowser()) {
      const data = localStorage.getItem('guabernaretoken');
      if (data) {
        return data;
      }
    }
    return null;
  }

  public static hasToken(): boolean {
    return !!this.getToken();
  }

  public static clear() {
    if (this.isBrowser()) {
      localStorage.removeItem('guabernareuser');
      localStorage.removeItem('guabernaretoken');
    }
  }
}
