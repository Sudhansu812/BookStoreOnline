import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Book } from 'src/app/Models/book-model';
import { UserStatus } from 'src/app/Models/user-status';
import { HomeService } from 'src/app/services/home.service';
import { CartComponent } from '../cart/cart.component';
import { OrderComponent } from '../order/order.component';
import { ShowBookComponent } from '../show-book/show-book.component';
import { UploadComponent } from '../upload/upload.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  logInStatus: boolean = false;
  public sessionData: UserStatus;

  constructor(
    private route: ActivatedRoute,
    private service: HomeService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  )
  { 
    this.service.listen().subscribe((m:any)=>{
      this.refreshMostSoldBookList();
    });
  }
  
  public mostSoldBooks: Book[] = [];
  public trendingBooks: Book[] = [];
  public recentBooks: Book[] = [];
  

  async ngOnInit(): Promise<void> {
    this.sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    this.logInStatus = this.sessionData.logInStatus;
    this.refreshMostSoldBookList();
    this.refreshTrendingBookList();
    this.refreshRecentBookList();
  }

  
  refreshMostSoldBookList() {
    this.service.mostSoldBooks$.pipe(switchMap(
      (data: Book[]) => {
        this.mostSoldBooks = data;
        return this.route.queryParams;
      }
    )).subscribe();
  }

  refreshTrendingBookList() {
    this.service.trendingBooks$.pipe(switchMap(
      (data: Book[]) => {
        this.trendingBooks = data;
        return this.route.queryParams;
      }
    )).subscribe();
  }

  refreshRecentBookList() {
    this.service.recentBooks$.pipe(switchMap(
      (data: Book[]) => {
        this.recentBooks = data;
        console.log(data);
        return this.route.queryParams;
      }
    )).subscribe();
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

  onClick(book: Book)
  {
    console.log(book.bookTitle);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "auto";
    dialogConfig.height = "auto";
    dialogConfig.data = book;
    this.dialog.open(ShowBookComponent, dialogConfig);
  }

  onUpload()
  {
    if(this.logInStatus)
    {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;
      dialogConfig.width = "auto";
      dialogConfig.height = "auto";
      this.dialog.open(UploadComponent, dialogConfig);
    }
    else
    {
      this.snackBar.open('Please log in to Upload Books', '', {
        duration:4500,
        verticalPosition:'top'
      });
    }
  }
}
