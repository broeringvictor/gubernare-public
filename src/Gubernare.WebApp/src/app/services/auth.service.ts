import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Security } from '../utils/security.util';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = Security.getToken();
    if (!token) {
      this.router.navigate(['/autenticar']);
      return false;
    }
    return true;
  }
}
