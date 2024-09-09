// cart/cart.component.ts
import { Component, OnInit } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { Cart } from '../../core/models/cart.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
})
export class CartComponent implements OnInit {
  cart: Cart | undefined;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.getCartByUserId(1).subscribe(data => {
      this.cart = data;
    });
  }

  removeItem(productId: number): void {
    this.cartService.removeFromCart({ productId, userId: 1 }).subscribe(() => {
      this.ngOnInit(); // Refresh cart
    });
  }
}
