import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Book } from 'src/app/Models/book-model';
import { Order } from 'src/app/Models/order-model';
import { UserStatus } from 'src/app/Models/user-status';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  public sessionData: UserStatus;
  public userId: number;
  public userStatus: boolean;
  public stock: number;
  public formdata: any;

  constructor(
    public dialogBox: MatDialogRef<OrderComponent>,
    @Inject(MAT_DIALOG_DATA) public book: Book,
    public service: OrderService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void { 
    this.sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    this.userStatus = this.sessionData.logInStatus;
    this.userId = this.sessionData.user;
    this.resetForm();
    this.getRemailningStock();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.service.formData = {
      userId: this.userId,
      bookId: this.book.bookId,
      orderQuantity: 0
    }
  }

  getRemailningStock()
  {
    this.service.getRemailningStock(this.book.bookId).subscribe(
      res => {
        this.stock = res;
      }
    );
  }

  onBuy(form: NgForm)
  {
    console.log(form.value['orderQuantity']);
    if(form.value['orderQuantity'] > this.stock)
    {
      this.snackBar.open('The order value is higher than the remaining stock.', '', {
        duration:4500,
        verticalPosition:'top'
      });
    }
    else
    {
      let order: Order = {
        userId: this.userId,
        bookId: this.book.bookId,
        orderQuantity: form.value['orderQuantity']
      }
      this.service.order(order).subscribe(
        res => {
          if(res == true)
          {
            this.snackBar.open('Order Successful', '', {
              duration:4500,
              verticalPosition:'top'
            });
          }
          else
          {
            this.snackBar.open('Failed to Order', '', {
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
