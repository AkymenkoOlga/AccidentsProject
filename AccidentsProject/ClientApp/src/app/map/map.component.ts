import { Component, Input, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import Appcomponent = require("../app.component");
import Accident = Appcomponent.Accident;
declare var google: any;
declare var MarkerClusterer: any;

@Component({
  selector: 'app-map',
  templateUrl: 'map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  private http: HttpClient;
  private baseUrl: string;

  private accidents: Accident[];
  private accidentsMarkers: any[] = [];
  private markers: any[] = [];
  private markerCluster: any;
  private map: any;

  private startDate: Date;
  private endDate: Date;
  private tags: any = {};

  ngOnInit() {
    this.initMap();
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.markers = [];
  }

  private initMap() {
    var mapProp = {
      center: new google.maps.LatLng(40.884800, -77.765863),
      zoom: 7,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    this.map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
  }

  setStartDate(type: string, event: MatDatepickerInputEvent<Date>) {
    this.startDate = event.value;
  }

  setEndDate(type: string, event: MatDatepickerInputEvent<Date>) {
    this.endDate = event.value;
  }

  tagClicked(event) {
    this.tags[event.source.value] = event.checked;
  }

  loadAccidents(): void {

    if (!this.startDate || !this.endDate) {
      return;
    }

    this.resetAccidents();

    var request = this.baseUrl + 'api/accidents?startDate=' +
      this.startDate.toDateString() + '&endDate=' + this.endDate.toDateString() + '&';

    for (let key in this.tags) {
      if (this.tags[key]) {
        request += 'tags=' + key + '&';
      }
    }

    this.http.get<Accident[]>(request)
      .subscribe(result => {
        this.accidents = result;

      this.accidents.forEach(accident => {
        var marker = new google.maps.Marker({
          position: new google.maps.LatLng(accident.location.coordinates.lat,
            accident.location.coordinates.lon),
          map: this.map,
          title: accident.date
        });
        this.markers.push(marker);
        });

      // Add a marker clusterer to manage the markers.
      this.markerCluster = new MarkerClusterer(this.map, this.markers,
        { imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m' });

    }, error => console.error(error));    
  }

  resetAccidents(): void {
    if (!!this.markerCluster) {
      this.markerCluster.clearMarkers();
    }

    this.markers.forEach(marker => {
      marker.setMap(null);
    });
    this.markers = [];
  }
}
