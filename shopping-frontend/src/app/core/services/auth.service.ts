// core/services/auth.service.ts
@Injectable({
    providedIn: 'root'
  })
  export class AuthService {
    private apiUrl = 'http://localhost:5000/api/auth';
  
    constructor(private http: HttpClient) {}
  
    login(credentials: { email: string; password: string }): Observable<any> {
      return this.http.post(`${this.apiUrl}/login`, credentials);
    }
  
    storeToken(token: string): void {
      localStorage.setItem('authToken', token);
    }
  
    getToken(): string | null {
      return localStorage.getItem('authToken');
    }
  }
  