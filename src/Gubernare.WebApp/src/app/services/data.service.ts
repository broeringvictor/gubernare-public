import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {AuthResponse} from '../models/interfaces/auth-response.model';
import {Observable} from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DataService {
  private http = inject(HttpClient);
  private url = 'https://localhost:7192/api/v1';

  private getAuthHeaders(): HttpHeaders | {} {
    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('guabernaretoken'); // Chave corrigida
      if (token) {
        return new HttpHeaders().set('Authorization', `Bearer ${token}`);
      }
    }
    return {};
  }
  authenticate({ data }: { data: any }): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.url}/account/authenticate`, data);
  }

  create({ data }: { data: any }) {
    const headers = this.getAuthHeaders();
    return this.http.post(`${this.url}/accounts`, data, { headers });
  }

  resetPassword({ data }: { data: any }) {
    return this.http.post(`${this.url}/accounts/reset-password`, data);
  }
}
