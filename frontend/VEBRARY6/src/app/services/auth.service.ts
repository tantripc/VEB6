import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'; // Import environment

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl + '/auth'; // Sử dụng apiUrl từ environment

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    const body = { Username: username, Password: password };
    return this.http.post(`${this.apiUrl}/login`, body);
  }
}
