import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';

import { Security } from '../../../utils/security.util';
import { DataService } from '../../../services/data.service';
import {AuthResponse} from '../../../models/interfaces/auth-response.model';
import {User} from '../../../models/user.model';

// Interfaces para type safety




@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-page.component.html'
})
export class LoginPageComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public busy = false;
  public errorMessage = '';

  private destroy$ = new Subject<void>();
  private router = inject(Router);
  private service = inject(DataService);
  private fb = inject(FormBuilder);

  constructor() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit(): void {
    if (typeof window !== 'undefined' && Security.hasToken()) {
      const user = Security.getUser();
      user && this.router.navigate(['/']);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  submit(): void {
    this.errorMessage = '';

    if (this.form.invalid) {
      this.markFormAsTouched();
      return;
    }

    this.busy = true;

    this.service.authenticate({ data: this.form.value })
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response: AuthResponse) => this.handleAuthSuccess(response),
        error: (err) => this.handleAuthError(err)
      });
  }

  private handleAuthSuccess(response: AuthResponse): void {
    this.busy = false;

    if (response.isSuccess && response.data?.token) {
      const user: User = {
        id: response.data.id,
        name: response.data.name,
        email: response.data.email,
        roles: response.data.roles
      };

      Security.set(user, response.data.token);

      // Adicione este log para debug
      console.log('Dados armazenados:', {
        user: Security.getUser(),
        token: Security.getToken()
      });

      this.router.navigate(['/']);
    } else {
      this.errorMessage = response.message || 'Authentication failed';
    }
  }

  private handleAuthError(err: any): void {
    this.busy = false;
    this.errorMessage = err.error?.message || 'An unexpected error occurred';
    console.error('Authentication error:', err);
  }

  private markFormAsTouched(): void {
    Object.values(this.form.controls).forEach(control => {
      control.markAsTouched();
      control.updateValueAndValidity();
    });
  }

  protected readonly Security = Security;
}
