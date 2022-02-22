import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RegisterService } from 'src/app/services/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(
    public dialogBox: MatDialogRef<RegisterComponent>,
    public service: RegisterService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.resetForm();
  }

  resetForm(form?: NgForm)
  {
    if(form != null)
    {
      form.resetForm();
    }
    this.service.formData = {
      userId: 0,
      userFirstName: '',
      userLastName: '',
      userName: '',
      email: '',
      password: ''
    }
  }

  onClose() {
    this.dialogBox.close();
    this.service.filter('Register Click');
  }

  onSubmit(form: NgForm) {
    console.log(form.value['userFirstName']);
    this.service.addUser(form.value).subscribe(res => {
      this.resetForm(form);
      this.snackBar.open("Added Successfully", '', {
        duration: 4500,
        verticalPosition: 'top'
      }
      );
    });;
  }
}
