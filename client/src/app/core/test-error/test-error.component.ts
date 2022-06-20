import { environment } from 'src/environments/environment';

import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.baseUrl;
  validationErrors: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  get400Error() {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({
      next: (response) => console.log(response),
      error: (err) => console.log(err),
    });
  }

  get400ValidationError() {
    this.http.get(this.baseUrl + 'buggy/badrequest/fortytwo').subscribe({
      next: (response) => console.log(response),
      error: (err) => {
        console.log(err);
        this.validationErrors = err.errors;
      },
    });
  }

  get404Error() {
    this.http.get(this.baseUrl + 'buggy/notfound').subscribe({
      next: (response) => console.log(response),
      error: (err) => console.log(err),
    });
  }

  get500Error() {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe({
      next: (response) => console.log(response),
      error: (err) => console.log(err),
    });
  }

  get500MathError() {
    this.http.get(this.baseUrl + 'buggy/math').subscribe({
      next: (response) => console.log(response),
      error: (err) => console.log(err),
    });
  }
}
