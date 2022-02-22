import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { HomeService } from 'src/app/services/home.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Book } from 'src/app/Models/book-model';
import { UserStatus } from 'src/app/Models/user-status';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderComponent } from '../order/order.component';
import { CartComponent } from '../cart/cart.component';

@Component({
  selector: 'app-show-book',
  templateUrl: './show-book.component.html',
  styleUrls: ['./show-book.component.css']
})
export class ShowBookComponent implements OnInit {

  public userStatus: boolean;
  public sessionData: UserStatus;

  constructor(
    public dialogBox: MatDialogRef<ShowBookComponent>,
    private service: HomeService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public book: Book
  ) { }

  ngOnInit(): void {
    this.sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    console.log(this.book.category);
    this.userStatus = this.sessionData.logInStatus;
  }

  onBuy(book: Book)
  {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "auto";
    dialogConfig.height = "auto";
    dialogConfig.data = book;
    this.dialog.open(OrderComponent, dialogConfig);
  }

  onAddToCart(book: Book)
  {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "auto";
    dialogConfig.height = "auto";
    dialogConfig.data = book;
    this.dialog.open(CartComponent, dialogConfig);
  }

  onRate(val:number,bookId:number)
  {
    console.log(val + "  " + bookId + "   " + this.sessionData.user);
    this.service.rateBook(val, bookId, this.sessionData.user).subscribe(res => {
      console.log("Response: " + res);
      if(!res)
      {
        this.snackBar.open('You have already rated', '', {
          duration:4500,
          verticalPosition:'top'
        });
      }
      else
      {
        this.snackBar.open('Rated Successfully', '', {
          duration:4500,
          verticalPosition:'top'
        });
      }
    });
  }

  onClose()
  {
    this.dialogBox.close();
    this.service.filter('Register Click');
  }
}
