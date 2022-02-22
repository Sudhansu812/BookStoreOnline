import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Book } from 'src/app/Models/book-model';
import { UserStatus } from 'src/app/Models/user-status';
import { BookService } from 'src/app/services/book.service';
import { HomeService } from 'src/app/services/home.service';
import { CartComponent } from '../cart/cart.component';
import { OrderComponent } from '../order/order.component';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})

export class CategoriesComponent implements OnInit {

  books: Book[];
  public sessionData: UserStatus;
  public userStatus: boolean;

  constructor(
    private service: BookService,
    private snackBar: MatSnackBar,
    private homeService: HomeService,
    private dialog: MatDialog
  ) {
    this.onClick("All");
   }

  ngOnInit(): void {
    this.sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    this.userStatus = this.sessionData.logInStatus;
  }

  categoryList: Array<string> = [
    'All',
    'History',
    'Scientific', 
    'Management', 
    'Technical',
    'Fiction',
    'Non-Fiction'
  ];

  onClick(cat: string = "All")
  {
    console.log(cat);
    this.service.getBooksByCategory(cat).subscribe(
      res => {
        this.books = res;
      }
    );
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
    this.homeService.rateBook(val, bookId, this.sessionData.user).subscribe(res => {
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
        location.reload();
      }
    });
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
}
