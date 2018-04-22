import { Component, Input, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import Appcomponent = require("../app.component");
import Accident = Appcomponent.Accident;
declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: 'map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  private http: HttpClient;
  private baseUrl: string;

  private tags: string[];
  private accidents: Accident[];
  private accidentsMarkers: any[] = [];
  private markers: any[] = [];

  private map: any;

  private startDate: Date;
  private endDate: Date;

  ngOnInit() {
    this.initMap();
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.markers = [];

    http.get<string[]>(baseUrl + 'api/accidents/tags').subscribe(result => {
      this.tags = result;
      console.log(this.tags);
    }, error => console.error(error));
  }

  setStartDate(type: string, event: MatDatepickerInputEvent<Date>) {
    this.startDate = event.value;
  }

  setEndDate(type: string, event: MatDatepickerInputEvent<Date>) {
    this.endDate = event.value;
  }

  private initMap() {
    var mapProp = {
      center: new google.maps.LatLng(51.508742, -0.120850),
      zoom: 5,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    this.map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
  }

  loadAccidents(): void {

    if (!this.startDate || !this.endDate) {
      return;
    }

    this.resetAccidents();

    this.http.get<Accident[]>(this.baseUrl + 'api/accidents?startDate=' +
      this.startDate.toDateString() + '&endDate=' + this.endDate.toDateString())
      .subscribe(result => {
        this.accidents = result;
        console.log("this.accidents", this.accidents);

      this.accidents.forEach(accident => {
        var marker = new google.maps.Marker({
          position: new google.maps.LatLng(accident.location.coordinates.lat,
            accident.location.coordinates.lon),
          map: this.map,
          title: accident.date
        });
        this.markers.push(marker);
      });
    }, error => console.error(error));    
  }

  resetAccidents(): void {
    this.markers.forEach(marker => {
      marker.setMap(null);
    });
    this.markers = [];
  }
}
