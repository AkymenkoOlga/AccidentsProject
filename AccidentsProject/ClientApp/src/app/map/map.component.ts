import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
declare var google: any;
import { Forms } from './map.forms';

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

    let startDate = new FormControl(new Date());
    let endDate = new FormControl(new Date());

    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
  }
}
@Component({
  selector: 'app-forms',
  templateUrl: 'map.component.html'
})
export class FormsComponent implements OnInit {
  public user: Forms;
  ngOnInit() {
      // initialize my forms here
      this.user = {
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
