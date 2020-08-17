import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CarsWarehouseService } from '../services/cars-warehouse.service';
import { NotificationHubService } from '../services/notification-hub.service';

@Component({
  selector: "app-fetch-data",
  templateUrl: "./fetch-data.component.html",
})
export class FetchDataComponent implements OnInit {
  cars: Car[];

  constructor(
    private readonly carWarehouseService: CarsWarehouseService,
    private readonly notificationHubService: NotificationHubService
  ) {}

  ngOnInit(): void {
    this.notificationHubService.notifications$.subscribe((message) => {
      console.log("ngOnInit notifications$ subscribe");
      console.log(message);
      if(message)
      {
        this.cars = JSON.parse(message);
      }
    });
  }

  load(){
    this.cars = [];
    this.getCars();
  }

  getCars(){
    this.carWarehouseService.getCars().subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }
}

interface Car {
  Name: string;
}
