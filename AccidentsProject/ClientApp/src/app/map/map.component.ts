import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: 'map.component.html'
})
export class MapComponent implements OnInit {
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
}
