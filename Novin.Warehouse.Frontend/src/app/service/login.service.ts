import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginResponseDto } from '../model/login-response.dto';
import { LoginRequestDto } from '../model/login-request.dto';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = 'http://localhost:5166/api/SecurityApi/login';
  
  constructor(private http: HttpClient) { }

  login(loginRequest: LoginRequestDto): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(this.apiUrl, loginRequest);
  }
}
