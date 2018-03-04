import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Appcomponent = require("../app.component");
import Accident = Appcomponent.Accident;

@Component({
  selector: 'app-accidents',
  templateUrl: './accidents.component.html'
})

export class AccidentsComponent implements OnInit {

  public accidents: Accident[];

  displayedColumns = ['id', 'date', "lat", "lon", 'tags'];
  dataSource = this.accidents;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Accident[]>(baseUrl + 'api/accidents').subscribe(result => {
      this.accidents = result;
      this.refresh();
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  refresh() {
    this.dataSource = this.accidents;
  }
}
