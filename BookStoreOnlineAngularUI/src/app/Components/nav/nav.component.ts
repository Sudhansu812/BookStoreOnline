import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UserStatus } from 'src/app/Models/user-status';
import { BookService } from 'src/app/services/book.service';
import { LogInComponent } from '../log-in/log-in.component';
import { RegisterComponent } from '../register/register.component';
import { ShowBookComponent } from '../show-book/show-book.component';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  logInStatus: boolean = false;
  public sessionData: UserStatus;
  
  constructor(
    private dialog: MatDialog,
    private router: Router,
    private service: BookService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    this.logInStatus = this.sessionData.logInStatus;
  }

  onLogout() {
    sessionStorage.removeItem("jwt");
    this.router.navigate(["login"]);
    location.reload();
  }

  onLogIn()
  {
    console.log(this.logInStatus);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "30%";
    this.dialog.open(LogInComponent, dialogConfig);
  }

  onRegister()
  {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "30%";
    this.dialog.open(RegisterComponent, dialogConfig);
  }

  onSearch(title: string)
  {
    console.log(title);
    this.service.getBookByTitle(title).subscribe(
      res => {
        if(res)
        {
          console.log(res.bookTitle);
          const dialogConfig = new MatDialogConfig();
          dialogConfig.disableClose = true;
          dialogConfig.autoFocus = true;
          dialogConfig.width = "auto";
          dialogConfig.height = "auto";
          dialogConfig.data = res;
          this.dialog.open(ShowBookComponent, dialogConfig);
        }
        else
        {
          this.snackBar.open('No Such Book Found', '', {
            duration:4500,
            verticalPosition:'top'
          });
        }
      }
    );
  }

}
