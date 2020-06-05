import { Component, Inject} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
@Component({
  selector: 'app-coffee-fetch',
  templateUrl: './coffee-fetch.component.html',
  styleUrls: ['./coffee-fetch.component.css']
})
export class CoffeeFetchComponent{
  public coffeeData: Coffee[];
  private http: HttpClient;
  public token: string;
  constructor(http: HttpClient, public dialog: MatDialog) {
    this.getRequest(http);
    this.http = http;
  }
  public getRequest(http: HttpClient){
    this.token = window.localStorage.getItem('coffeeshopapitoken');
    http.get<Coffee[]>('http://localhost:5000/api/coffee').subscribe(
      result => {
        this.coffeeData = result
      }
    );
  }
  public delete(id:number):void{
    fetch(`http://localhost:5000/api/coffee`, {
      method: 'DELETE',
      headers: {
          'Content-Type': 'application/json',
          'Authorization': `bearer ${this.token}`
      },
      body: JSON.stringify({"id": id})
      }).then(x=> this.getRequest(this.http)); 
  }
  public create():void{
    let coffee= <Coffee>{};
    const dialogRef = this.dialog.open(CoffeeFetchComponentCreateDialog, {
      width: '250px',
      data: coffee,
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/coffee`, {
        method: 'POST',
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
          "coffeetype":parseInt(result['coffeeType']),
          "country": result['country'],
          "sour":parseFloat(result['sour']),
          "strength": parseFloat(result['strength']),
          "saturation": parseFloat(result['saturation']),
          "aroma":  parseFloat(result['aroma'])
      }
      )
      }).then(x=> this.getRequest(this.http));        
      console.log('The dialog was closed');
    });
  }
  public edit(id:number):void{
    let coffee  = this.coffeeData.find(x=>x.id == id)
    const dialogRef = this.dialog.open(CoffeeFetchComponentDialog, {
      width: '250px',
      data: coffee
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/coffee/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `bearer ${this.token}`
        },
        body: JSON.stringify({
          "id": id,
          "name": result['name'],
          "price": parseFloat(result['price']),
          "expirationdate": result['expirationDate'],
          "quantity": parseFloat(result['quantity']),
          "rating": parseFloat(result['rating']),
          "coffeetype":parseInt(result['coffeeType']),
          "country": result['country'],
          "sour":parseFloat(result['sour']),
          "strength": parseFloat(result['strength']),
          "saturation": parseFloat(result['saturation']),
          "aroma":  parseFloat(result['aroma'])
      }
      )
      }).then(x=> this.getRequest(this.http));        
      console.log('The dialog was closed');
    });
  }
}

interface Coffee {
  id: number,
  name: string,
  price: number,
  expirationDate: string,
  quantity: number,
  country: string,
  rating: number,
  coffeeType: number,
  sour: number,
  strength: number,
  saturation: number,
  aroma: number
}

@Component({
  selector: 'app-coffee-fetch-dialog',
  templateUrl: 'app-coffee-fetch-dialog.html',
})
export class CoffeeFetchComponentDialog {

  constructor(
    public dialogRef: MatDialogRef<CoffeeFetchComponentDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Coffee) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}
@Component({
  selector: 'app-coffee-fetch-dialog-create',
  templateUrl: 'app-coffee-fetch-dialog-create.html',
})
export class CoffeeFetchComponentCreateDialog {

  constructor(
    public dialogRef: MatDialogRef<CoffeeFetchComponentCreateDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Coffee) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}