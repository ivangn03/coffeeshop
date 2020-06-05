import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-milk-fetch',
  templateUrl: './milk-fetch.component.html',
  styleUrls: ['./milk-fetch.component.css']
})
export class MilkFetchComponent{
  public milkData: Milk[];
  private http: HttpClient;
  public token: string;
  constructor(http: HttpClient, public dialog: MatDialog) {
    this.getRequest(http);
    this.http = http;
  }
  public getRequest(http: HttpClient){
    this.token = window.localStorage.getItem('coffeeshopapitoken');
    http.get<Milk[]>('http://localhost:5000/api/milk').subscribe(
      result => {
        this.milkData = result
      }
    );
  }
  public delete(id:number):void{
    fetch(`http://localhost:5000/api/milk`, {
      method: 'DELETE',
      headers: {
          'Content-Type': 'application/json',
          'Authorization': `bearer ${this.token}`
      },
      body: JSON.stringify({"id": id})
      }).then(x=> this.getRequest(this.http)); 
  }
  public create():void{
    let milk= <Milk>{};
    const dialogRef = this.dialog.open(MilkFetchComponentCreateDialog, {
      width: '250px',
      data: milk,
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/milk`, {
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
            "milktype":parseInt(result['milkType']),
            "fattiness": parseFloat(result['fattiness']),
          }
      )
      }).then(x=> this.getRequest(this.http));        
      console.log('The dialog was closed');
    });
  }
  public edit(id:number):void{
    let milk  = this.milkData.find(x=>x.id == id)
    const dialogRef = this.dialog.open(MilkFetchComponentDialog, {
      width: '250px',
      data: milk
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/milk/${id}`, {
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
          "milktype":parseInt(result['milkType']),
          "fattiness": parseFloat(result['fattiness']),
      }
      )
      }).then(x=> this.getRequest(this.http));        
      console.log('The dialog was closed');
    });
  }
}
interface Milk {
  id: number,
  name: string,
  price: number,
  expirationDate: string,
  quantity: number,
  fattiness: number,
  rating: number,
  milkType: number,
}

@Component({
  selector: 'app-milk-fetch-dialog',
  templateUrl: 'app-milk-fetch-dialog.html',
})
export class MilkFetchComponentDialog {

  constructor(
    public dialogRef: MatDialogRef<MilkFetchComponentDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Milk) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}

@Component({
  selector: 'app-milk-fetch-dialog-create',
  templateUrl: 'app-milk-fetch-dialog-create.html',
})
export class MilkFetchComponentCreateDialog {

  constructor(
    public dialogRef: MatDialogRef<MilkFetchComponentCreateDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Milk) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}