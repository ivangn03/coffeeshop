import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-topping-fetch',
  templateUrl: './topping-fetch.component.html',
  styleUrls: ['./topping-fetch.component.css']
})
export class ToppingFetchComponent{
  public toppingData: Topping[];
  private http: HttpClient;
  public token: string;
  constructor(http: HttpClient, public dialog: MatDialog) {
    this.getRequest(http);
    this.http = http;
  }
  public getRequest(http: HttpClient){
    this.token = window.localStorage.getItem('coffeeshopapitoken');
    http.get<Topping[]>('http://localhost:5000/api/topping').subscribe(
      result => {
        this.toppingData = result
      }
    );
  }
  public delete(id:number):void{
    fetch(`http://localhost:5000/api/topping`, {
      method: 'DELETE',
      headers: {
          'Content-Type': 'application/json',
          'Authorization': `bearer ${this.token}`
      },
      body: JSON.stringify({"id": id})
      }).then(x=> this.getRequest(this.http)); 
  }
  public create():void{
    let topping= <Topping>{};
    const dialogRef = this.dialog.open(ToppingFetchComponentCreateDialog, {
      width: '250px',
      data: topping,
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/topping`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `bearer ${this.token}`
        },
        body: JSON.stringify(
          {
            "name": result['name'],
            "price": parseFloat(result['price']),
            "expirationdate": result['expirationDate'],
            "quantity": parseFloat(result['quantity']),
            "rating": parseFloat(result['rating']),
          }
      )
      }).then(x=> this.getRequest(this.http));        
      console.log('The dialog was closed');
    });
  }
  public edit(id:number):void{
    let topping  = this.toppingData.find(x=>x.id == id)
    const dialogRef = this.dialog.open(ToppingFetchComponentDialog, {
      width: '250px',
      data: topping
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/topping/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `bearer ${this.token}`
        },
        body: JSON.stringify({
          "name": result['name'],
          "price": parseFloat(result['price']),
          "expirationdate": result['expirationDate'],
          "quantity": parseFloat(result['quantity']),
          "rating": parseFloat(result['rating']),
      }
      )
      }).then(x=> this.getRequest(this.http));        
      console.log('The dialog was closed');
    });
  }
}
interface Topping {
  id: number,
  name: string,
  price: number,
  expirationDate: string,
  quantity: number,
  rating: number,
}

@Component({
  selector: 'app-topping-fetch-dialog',
  templateUrl: 'app-topping-fetch-dialog.html',
})
export class ToppingFetchComponentDialog {

  constructor(
    public dialogRef: MatDialogRef<ToppingFetchComponentDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Topping) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}

@Component({
  selector: 'app-topping-fetch-dialog-create',
  templateUrl: 'app-topping-fetch-dialog-create.html',
})
export class ToppingFetchComponentCreateDialog {

  constructor(
    public dialogRef: MatDialogRef<ToppingFetchComponentCreateDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Topping) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}