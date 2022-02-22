import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { UserStatus } from 'src/app/Models/user-status';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart-table',
  templateUrl: './cart-table.component.html',
  styleUrls: ['./cart-table.component.css']
})
export class CartTableComponent implements OnInit {

  public sessionData: UserStatus;
  public userId: number;
  public userStatus: boolean;

  listData: MatTableDataSource<any> = new MatTableDataSource<any>();

  displayedColumns: string[] = ['Options', 'cartId', 'bookCoverPath', 'bookTitle', 'bookQuantity', 'bookPrice', 'bookSumTotal'];
  totalCartValue: number = 0;

  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: CartService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.service.listen().subscribe((m: any) => {
      console.log();
      this.refreshCartList();
    });
   }

  ngOnInit(): void {
    this.sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    this.userStatus = this.sessionData.logInStatus;
    this.userId = this.sessionData.user;
    this.listData.sort = this.sort;
    this.refreshCartList();
  }

  refreshCartList() {
    this.service.getCartList(this.userId).subscribe(data => {
      this.listData = new MatTableDataSource(data);
      this.listData.sort = this.sort;
      for(let i=0;i<data.length;i++)
      {
        this.totalCartValue = this.totalCartValue + data[i].bookPrice;
      }
    });
  }

  applyFilter(filtervalue: string) {
    this.listData.filter = filtervalue.trim().toLowerCase();
  }

  onDelete(id: number)
  {
    if (confirm('Are you sure, the data will be deleted permanently!')) {
      this.service.deleteFromCart(id).subscribe(res => {
        this.refreshCartList();
        this.snackBar.open("Deleted Successfully", '', {
          duration: 4500,
          verticalPosition: 'top'
        }
        );
      });
    }
  }

  onOrder()
  {
    this.service.orderCartItems(this.userId).subscribe(
      res => {
        if(res)
        {
          this.refreshCartList();
          this.snackBar.open("Ordered Successfully", '', {
            duration: 4500,
            verticalPosition: 'top'
          }
          );
        }
        else
        {
          this.refreshCartList();
        this.snackBar.open("Something went wrong", '', {
          duration: 4500,
          verticalPosition: 'top'
        }
        );
        }
      }
    );
  }

}
