import { Component , Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
@Component({
  selector: 'app-tea-fetch',
  templateUrl: './tea-fetch.component.html',
  styleUrls: ['./tea-fetch.component.css']
})


export class TeaFetchComponent{
  public teaData: Tea[];
  private http: HttpClient;
  public token: string;
  constructor(http: HttpClient, public dialog: MatDialog) {
    this.getRequest(http);
    this.http = http;
  }
  public getRequest(http: HttpClient){
    this.token = window.localStorage.getItem('coffeeshopapitoken');
    http.get<Tea[]>('http://localhost:5000/api/tea').subscribe(
      result => {
        this.teaData = result
      }
    );
  }
  public delete(id:number):void{
    fetch(`http://localhost:5000/api/tea`, {
      method: 'DELETE',
      headers: {
          'Content-Type': 'application/json',
          'Authorization': `bearer ${this.token}`
      },
      body: JSON.stringify({"id": id})
      }).then(x=> this.getRequest(this.http)); 
  }
  public create():void{
    let tea= <Tea>{};
    const dialogRef = this.dialog.open(TeaFetchComponentCreateDialog, {
      width: '250px',
      data: tea,
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/tea`, {
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
          "teatype":parseInt(result['teaType']),
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
    let tea  = this.teaData.find(x=>x.id == id)
    const dialogRef = this.dialog.open(TeaFetchComponentDialog, {
      width: '250px',
      data: tea
    });
    
    dialogRef.afterClosed().subscribe(result => {
      fetch(`http://localhost:5000/api/tea/${id}`, {
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
          "teatype":parseInt(result['teaType']),
          "country": result['country'],
      }
      )
      }).then(x=> this.getRequest(this.http));        
      console.log('The dialog was closed');
    });
  }
}
interface Tea {
  id: number,
  name: string,
  price: number,
  expirationDate: string,
  quantity: number,
  country: string,
  rating: number,
  teaType: number,
}

@Component({
  selector: 'app-tea-fetch-dialog',
  templateUrl: 'app-tea-fetch-dialog.html',
})
export class TeaFetchComponentDialog {

  constructor(
    public dialogRef: MatDialogRef<TeaFetchComponentDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Tea) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}

@Component({
  selector: 'app-tea-fetch-dialog-create',
  templateUrl: 'app-tea-fetch-dialog-create.html',
})
export class TeaFetchComponentCreateDialog {

  constructor(
    public dialogRef: MatDialogRef<TeaFetchComponentCreateDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Tea) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}