import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Injectable } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { Observable, map } from 'rxjs';

@Injectable({ providedIn: 'root' }) // Mark as injectable service
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((auth) => {
        console.log('auth:', auth);
        if (auth && auth.token) {
          return true; // User is authenticated
        } else {
          // User is not authenticated, navigate to login page
          this.router.navigate(['Account/login'], {
            queryParams: { returnUrl: state.url },
          });
          console.log('Navigating to login page. returnUrl:', state.url);
          return false;
        }
      })
    );
  }
  
}
