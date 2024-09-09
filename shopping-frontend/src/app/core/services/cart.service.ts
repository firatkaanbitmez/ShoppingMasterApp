// core/services/cart.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart, CartItem } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5000/api/cart';

  constructor(private http: HttpClient) {}

  getCartByUserId(userId: number): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/${userId}`);
  }

  addToCart(cartItem: CartItem): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, cartItem);
  }

  removeFromCart(cartItem: CartItem): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/remove`, cartItem);
  }

  clearCart(cartId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/clear/${cartId}`);
  }
}
