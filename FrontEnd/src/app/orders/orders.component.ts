import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  arrRentInfo;
  role = localStorage.role
  currentRent;
  isEdit = false;
  doneEdit = "";
  isAdd;
  errormsg = "";
  idErrorMsg;
  todayDate = new Date().toISOString().slice(0, 10);
   //geting date from server to the array
  constructor(private Connection: HttpClient,
     private ConnectionPut: HttpClient,
     private deleteConnection: HttpClient,
     private postConnection: HttpClient) {
    this.Connection.get("http://localhost:64130/rentinfoadmin").subscribe(t => this.arrRentInfo = t);
  }
  isId(id: string): boolean {
    if (parseInt(id) % 1 != 0 || parseInt(id) < 0 || !(Number.isInteger(parseFloat(id)))) {
      this.idErrorMsg ="full/positive number!";
      return true;
    }
    else{
      this.idErrorMsg = "";
      return false;
    }
  }
  //function that check if the string is blank
  errorMsg(value: string): boolean {
    if (value == "") {
      this.errormsg = "required field"
      return true;
    }
    return false;
  }
  //function that get the currect rentid
  getEdit(rentid: any) {
    this.isAdd = false;
    this.currentRent = rentid;
    this.isEdit = true;
  }
  //function that get all parameters and edit the currect rent 
  editRent(carid: string, userid: string, rentDate: string, returnDate: string, RealReturnDate: string): void {
    if (this.errorMsg(carid) || this.errorMsg(userid) || this.errorMsg(rentDate) || this.errorMsg(returnDate)) {
      return;
    }
    if (this.isId(userid)) {
      this.errormsg = ""
      return;
    }
    if (RealReturnDate == "") {
      RealReturnDate = null
    }
    this.ConnectionPut.put("http://localhost:64130/rentinfoadmin/" 
    + this.currentRent.rentId + "/" 
    + carid + "/" 
    + userid + "/"
    + rentDate + "/"
    + returnDate + "/"
    + RealReturnDate, { "observe": "response" }).subscribe();
     alert("Changes has done!")
     setTimeout(function(){ window.location.reload();; }, 1000);
  }
  //function  that get all parameters and add a new rent
  addRent(carid: string, userid: string, rentDate: string, returnDate: string, RealReturnDate: string): void {
    if (this.errorMsg(carid) || this.errorMsg(userid) || this.errorMsg(rentDate) || this.errorMsg(returnDate)) {
      return;
    }
    if (this.isId(userid)) {
      this.errormsg = ""
      return  
    }
    this.postConnection.post("http://localhost:64130/rentinfoadmin", { "carId": parseInt(carid),
                                                                       "userId": userid,
                                                                       "rentDate": rentDate,
                                                                       "returnDate": returnDate,
                                                                       "realReturnDate": RealReturnDate })
      .subscribe();
    this.ConnectionPut.put("http://localhost:64130/Carsinfo/" + carid + "/no", { "observe": "response" }) .subscribe();
    alert("Changes has done!")
    setTimeout(function(){ window.location.reload();; }, 1000);
  }
  //function that work when you want to add a new car and isAdd go true
  getAdd() {
    this.isEdit = false;
    this.isAdd = true;
  }
  //function that get the currect rentid and delete if the user confirm
  getDeleteRent(rentid: any) {
    this.currentRent = rentid
    this.isEdit = false
    var choice = confirm("Are you sure you want to delete Rent Id - " + this.currentRent.rentId + "?");
    if (choice == true) {
      this.deleteConnection.delete("http://localhost:64130/rentinfoadmin/" + this.currentRent.rentId, { "observe": "response" }).subscribe();
      alert("Rent deleted!")
      setTimeout(function(){ window.location.reload();; }, 1000);
    } else {
      alert("You pressed Cancel! Nothing changed!")
    }
  }
  //function that help reading local storang value
  readLocalStorageValue(key: string): string {
    return localStorage.getItem(key);
  }
  //read the currect value of localstorage key
  ngOnInit(): void {
    this.role = this.readLocalStorageValue('role');
  }
}
