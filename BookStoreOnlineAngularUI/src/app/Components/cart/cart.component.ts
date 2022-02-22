import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Book } from 'src/app/Models/book-model';
import { Cart } from 'src/app/Models/cart-model';
import { UserStatus } from 'src/app/Models/user-status';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  public sessionData: UserStatus;
  public userId: number;
  public userStatus: boolean;
  public stock: number;
  public formdata: any;

  constructor(
    public dialogBox: MatDialogRef<CartComponent>,
    @Inject(MAT_DIALOG_DATA) public book: Book,
    public service: CartService,
    public oService: OrderService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    this.userStatus = this.sessionData.logInStatus;
    this.userId = this.sessionData.user;
    this.getRemailningStock();
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.service.formData = {
      userId: this.userId,
      bookId: this.book.bookId,
      cartQuantity: 0
    }
  }

  getRemailningStock()
  {
    this.oService.getRemailningStock(this.book.bookId).subscribe(
      res => {
        this.stock = res;
      }
    );
  }

  onAddToCart(form: NgForm)
  {
    console.log(form.value['cartQuantity']);
    if(form.value['cartQuantity'] > this.stock)
    {
      this.snackBar.open('The cart value is higher than the remaining stock.', '', {
        duration:4500,
        verticalPosition:'top'
      });
    }
    else
    {
      let cart: Cart = {
        userId: this.userId,
        bookId: this.book.bookId,
        cartQuantity: form.value['cartQuantity']
      }
      this.service.addToCart(cart).subscribe(
        res => {
          if(res == true)
          {
            this.snackBar.open('Added to Cart', '', {
              duration:4500,
              verticalPosition:'top'
            });
          }
          else
          {
            this.snackBar.open('Failed to add to cart', '', {
              duration:4500,
              verticalPosition:'top'
            });
          }
        },
        err =>
        {
          console.log(err);
        }
      );
    }
  }

  onClose()
  {
    this.dialogBox.close();
  }

}
