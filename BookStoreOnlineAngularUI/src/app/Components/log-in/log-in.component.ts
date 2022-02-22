import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UserLogin } from 'src/app/Models/user-login';
import { UserStatus } from 'src/app/Models/user-status';
import { RegisterService } from 'src/app/services/register.service';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  constructor(
    public service: RegisterService,
    public router:Router,
    public dialogBox: MatDialogRef<LogInComponent>,
    private snackBar: MatSnackBar
  ) { }

  invalidLogin: boolean;

  ngOnInit(): void {
  }

  onClose()
  {
    this.dialogBox.close();
    this.service.filter('Register Click');
    location.reload();
  }

  login(form: NgForm)
  {
    const credentials: UserLogin = {
      'email': form.value.email,
      'password': form.value.password
    }

    this.service.logUser(credentials).subscribe(response => {
      console.log(response);
      this.snackBar.open("Logged In Successfully", '', {
        duration: 4500,
        verticalPosition: 'top'
      });
      this.onClose();
    }, err => {
      this.invalidLogin = true;
    });
  }

}
