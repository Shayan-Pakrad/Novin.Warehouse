import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { LoginResponseDto } from '../model/login-response.dto';
import { LoginRequestDto } from '../model/login-request.dto';
import { User } from '../model/user.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = 'http://localhost:5166/api/SecurityApi/login';
  user = new Subject<User>();
  
  constructor(private http: HttpClient) { }

  login(loginRequest: LoginRequestDto): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(this.apiUrl, loginRequest)
      .pipe(tap((res) => {
        const expiresInTs = new Date().getTime() + res.expiresIn * 1000;
        const expiresIn = new Date(expiresInTs);
        const user = new User(loginRequest.username, res.accessToken, res.refreshToken, expiresIn);
        this.user.next(user);
      }));
  }
}
