import { Component } from '@angular/core';
import { Router} from '@angular/router';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent{
  public login:string;
  public password: string;
  constructor(private router:Router,private _snackBar: MatSnackBar) { }

  public logIn():void{
      let credentials = {
        "login": this.login,
        "password": this.password
      };
    fetch('http://localhost:5000/api/auth/authenticate', {
      method: 'POST',
      body: JSON.stringify(credentials),
      headers: {
          'Content-Type': 'application/json'
      }
    })
      .then(x => {
          if (!x.ok) {
              x.text()
                  .then(res => {     
                      console.log(res);                
                  });
          } else {
              x.json().then(result => {
                if(!result.success)
                  this._snackBar.open(result.message,'Close',{
                    duration:2000
                  });
                else
                  this._snackBar.open('User authenticated','Close',{
                    duration:2000
                  });
                  console.log(result);
                  let userInfo = this.parseJwt(result.data.token);
                  console.log(userInfo);
                  window.localStorage.setItem('coffeeshopapitoken', result.data.token);
                  this.router.navigate(['./']);
                                });

          }

      });
  }


  public parseJwt(token:string):string {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};

}
