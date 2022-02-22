import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UploadService } from 'src/app/services/upload.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  constructor(
    public dialogBox: MatDialogRef<UploadComponent>,
    public service: UploadService,
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
      bookTitle: '',
      bookAuthorName: '',
      bookDescription: '',
      category: '',
      bookPrice: 0,
      bookQuantity: 0,
      bookCoverPath: ''
    }
  }

  onClose() {
    this.dialogBox.close();
    this.service.filter('Register Click');
    location.reload();
  }

  onSubmit(form: NgForm)
  {
    console.log(form.value['bookCoverPath']);
    this.service.uploadBook(form.value).subscribe(res => {
    this.resetForm(form);
      this.snackBar.open("Added Successfully", '', {
        duration: 4500,
        verticalPosition: 'top'
      }
      );
    }, err => {
      console.log(err);
      this.snackBar.open("Something went wrong", '', {
        duration: 4500,
        verticalPosition: 'top'
      }
      );
    });;
  }

}
