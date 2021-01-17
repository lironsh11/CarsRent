import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-stock',
  templateUrl: './edit-stock.component.html',
  styleUrls: ['./edit-stock.component.css']
})
export class EditStockComponent implements OnInit {
  errormsg = "";
  arrInfoCars;
  isEdit;
  isAdd;
  currentCar;
  doneEdit = "";
  role: any = localStorage.role
  //geting date from server to the array
  constructor(private Connection: HttpClient,
              private ConnectionPut: HttpClient,
              private ConnectionPut2: HttpClient,
              private postConnection: HttpClient, 
              private deleteConnection: HttpClient, 
              private deleteConnection2: HttpClient) {
    this.Connection.get("http://localhost:64130/carsinfoadmin").subscribe(t => this.arrInfoCars = t);
  }

  //get the currect car to delete and ask if to delete
  getDeleteCar(carid: any) {
    this.currentCar = carid
    this.isEdit = false
    var choice = confirm("Are you sure you want to delete Car - " +  this.currentCar.manufacturer +" "+  this.currentCar.model+ "?");
    if (choice == true) {
      this.deleteConnection.delete("http://localhost:64130/carsinfoadmin/" + this.currentCar.carId, { "observe": "response" }).subscribe();
      this.deleteConnection2.delete("http://localhost:64130/carstype/" + this.currentCar.carId, { "observe": "response" }).subscribe();
      alert("Car deleted!")
      setTimeout(function(){ window.location.reload();; }, 1000);
    } else {
      alert("You pressed Cancel! Nothing changed!")
    }

  }
  //function that get all parameters and edit the currect car
  editCar(carid: string,
          model: string,
          Manufacturer: string, 
          ManufacturerYear: string, 
          GearType: string, 
          DayValue: string, 
          LateDayValue: string, 
          carType: string, 
          AvailableForRent: string, 
          ProperForRent: string, 
          CurrectMileage: string, 
          pic: string): void {
    if (pic == "") {
      pic = null
    }
    if (this.errorMsg(carid) ||
        this.errorMsg(model) ||
        this.errorMsg(Manufacturer) || 
        this.errorMsg(ManufacturerYear) || 
        this.errorMsg(DayValue) || 
        this.errorMsg(LateDayValue) || 
        this.errorMsg(CurrectMileage)) {
      return;
    }
    this.ConnectionPut.put("http://localhost:64130/carsinfoadmin/" + this.currentCar.carId + "/"
                                                                   + carid + "/"
                                                                   + carType + "/" 
                                                                   + AvailableForRent + "/"
                                                                   + ProperForRent + "/"
                                                                   + CurrectMileage + "/"
                                                                   + pic, { "observe": "response" }).subscribe();
    this.ConnectionPut2.put("http://localhost:64130/carstype/" + this.currentCar.carId + "/"
                                                               + carid + "/" 
                                                               + model + "/" 
                                                               + Manufacturer + "/" 
                                                               + ManufacturerYear + "/" 
                                                               + GearType + "/" 
                                                               + DayValue + "/" 
                                                               + LateDayValue, { "observe": "response" }).subscribe();
    alert("Changes has done!")
    setTimeout(function(){ window.location.reload();; }, 1000);
  }

  //function that get all parameters and add a new car
  AddCar(carid: string,
         model: string,
         Manufacturer: string,
         ManufacturerYear: string, 
         GearType: string, 
         DayValue: string, 
         LateDayValue: string, 
         carType: string, 
         AvailableForRent: string, 
         ProperForRent: string, 
         CurrectMileage: string, 
         pic: string): void {
    if (this.errorMsg(carid) || 
        this.errorMsg(model) || 
        this.errorMsg(Manufacturer) || 
        this.errorMsg(ManufacturerYear) || 
        this.errorMsg(DayValue) || 
        this.errorMsg(LateDayValue) || 
        this.errorMsg(CurrectMileage)) {
      return;
    }
    this.postConnection.post("http://localhost:64130/carsinfoadmin/", { "carId": parseInt(carid), 
                                                                        "carType": carType, 
                                                                        "currectMileage": parseInt(CurrectMileage), 
                                                                        "picture": pic, "properForRent": ProperForRent, 
                                                                        "availableForRent": AvailableForRent })
      .subscribe();
    this.postConnection.post("http://localhost:64130/carstype/", { "model": model,
                                                                    "manufacturer": Manufacturer,
                                                                     "dayValue": DayValue,
                                                                    "lateDayValue": LateDayValue,
                                                                    "manufacturerYear": ManufacturerYear, 
                                                                    "gearType": GearType, 
                                                                    "carId": parseInt(carid) }).subscribe();
    alert("Changes has done!")
    setTimeout(function(){ window.location.reload();; }, 1000);
  }
  //function that check if the string is blank
  errorMsg(value: string): boolean {
    if (value == "") {
      this.errormsg = "required field"
      return true;
    }
    return false;
  }

  //function that get the currect car
  getEdit(carid: any) {
    this.isAdd = false;
    this.currentCar = carid
    this.isEdit = true
  }
  //function that work if the add button got click and isAdd go true
  getAdd() {
    this.isEdit = false;
    this.isAdd = true;
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
