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

    var user = {
        Fatal_Count: false, //my checkboxes
        Bicycle_Count: false,
        Motorcycle_Count: false,
        Automobile_Count: false,
        Ped_Count: false,
        Ped_Fatal: false,
        TFC_Detour_ind: false,
        Time_of_day_day: false,
        Time_of_day_night: false,
        Unb_death_count: false
      }
  }
}

interface Forms {
  Fatal_Count?: boolean; //checkboxes
  Bicycle_Count?: boolean;
  Motorcycle_Count?: boolean;
  Automobile_Count?: boolean;
  Ped_Count?: boolean;
  Ped_Fatal?: boolean;
  TFC_Detour_ind?: boolean;
  Time_of_day_day?: boolean;
  Time_of_day_night?: boolean;
  Unb_death_count?: boolean;
}
