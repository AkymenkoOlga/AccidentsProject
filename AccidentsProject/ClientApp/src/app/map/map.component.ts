import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import Appcomponent = require("../app.component");
import Accident = Appcomponent.Accident;
declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: 'map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  private tags: string[];
  private accidents: Accident[];

  ngOnInit() {
    var mapProp = {
      center: new google.maps.LatLng(51.508742, -0.120850),
      zoom: 5,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    let startDate = new FormControl(new Date());
    let endDate = new FormControl(new Date());

  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<string[]>(baseUrl + 'api/accidents/tags').subscribe(result => {
      this.tags = result;
      console.log(this.tags);
    }, error => console.error(error));

    http.get<Accident[]>(baseUrl + 'api/accidents').subscribe(result => {
      this.accidents = result;
      console.log(this.accidents);
    }, error => console.error(error));
  }
}
