import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  arrUsersInfo;
  role = localStorage.role
  currentUser;
  isEdit;
  errormsg;
  isAdd;
  idErrorMsg;
  fullNameErrorMsg;
  emailErrorMsg;
  pattern = /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/;
  doneEdit = "";
   //geting date from server to the array
  constructor(private Connection: HttpClient,
    private ConnectionPut: HttpClient,
    private deleteConnection: HttpClient,
    private postConnection: HttpClient) {
    this.Connection.get("http://localhost:64130/Users").subscribe(t => this.arrUsersInfo = t);
  }
  //function to change error msg if user didnt enter value
  errorMsg(value: string): boolean {
    if (value == "") {
      this.errormsg = "required field"
      return true;
    }
    return false;
  }

  //function to check if the user enter a valid email
  isEmailAddress(str: string): boolean {
    if (str.match(this.pattern)) {
      this.emailErrorMsg = ""
      return false;
    }
    else {
      this.emailErrorMsg = "please currect email"
      return true;
    }
  }
    //function the check if id is valid
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
  //function the check if full name contain mumbers
  isString(str: any): boolean {
    var matches = str.match(/\d+/g);
    if (matches != null) {
      this.fullNameErrorMsg = "Cant contain numbers";
      return true
    }
    else {
      this.fullNameErrorMsg = "";
      return false;
    }
  }

  //function that get the currect user id
  getEdit(userid: any) {
    this.isAdd = false;
    this.currentUser = userid
    this.isEdit = true
  }
  //function that work if the user want to add a new car and isAdd go true
  getAdd() {
    this.isAdd = true;
    this.isEdit = false
  }
  //function that get al parameters and add a new user
  addUser(id: string,
    fullName: string,
    userName: string,
    gender: string,
    email: string,
    password: string,
    role: string,
    date: string = null,
    pic: string = null): void {
    if (this.errorMsg(id) || this.errorMsg(fullName) || this.errorMsg(email) || this.errorMsg(password)) {

      return;
    }
    if (this.isString(fullName)) {
      this.errormsg = ""
      return;
    }
    if (this.isId(id)) {
      this.errormsg = ""
      return;
    }
    if (this.isEmailAddress(email)) {
      this.errormsg = ""
      return;
    }
    else {
      password = password.trim();
      this.postConnection.post("http://localhost:64130/Users/AddUser", {
        "id": id,
        "fullName": fullName,
        "userName": userName,
        "dateOfBirth": date,
        "gender": gender,
        "email": email,
        "password": password.trim(),
        "picture": pic,
        "role": role
      }).subscribe();
       alert("Changes has done");
       setTimeout(function(){ window.location.reload();; }, 1000);
    }
  }

  //function that get all paremters and edit the currect user
  editUser(fullName: string,
    userName: string,
    gender: string,
    email: string,
    password: string,
    role: string,
    date: string = "0000-00-00",
    pic: string = "null"): void {
    if (date == "") {
      date = null
    }
    if (pic == "") {
      pic = null
    }
    if (this.errorMsg(fullName) || this.errorMsg(email) || this.errorMsg(password)) {
      return;
    }
    if (this.isString(fullName)) {
      this.errormsg = ""
      return;
    }
    if (this.isEmailAddress(email)) {
      this.errormsg = ""
      return;
    }

    this.postConnection.put("http://localhost:64130/Users/UpdateUser", {
      "id": this.currentUser.id,
      "fullName": fullName,
      "userName": userName,
      "dateOfBirth": date,
      "gender": gender,
      "email": email,
      "password": password.trim(), "picture": pic, "role": role
    }).subscribe();
    alert("Changes has done");
    setTimeout(function(){ window.location.reload();; }, 1000);
  }
  //function that get the currect userid and delete if the user confirm
  getDeleteUser(userid: any) {
    this.currentUser = userid
    this.isEdit = false
    var choice = confirm("Are you sure you want to delete - " + this.currentUser.fullName + "?");
    if (choice == true) {
      this.deleteConnection.delete("http://localhost:64130/Users/" + this.currentUser.id, { "observe": "response" }).subscribe();
      alert("User deleted!")
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
