import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
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
  dataSource = new MatTableDataSource<Accident>(this.accidents);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Accident[]>(baseUrl + 'api/accidents').subscribe(result => {
      this.accidents = result;
      this.refresh();
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  refresh() {
    this.dataSource = new MatTableDataSource<Accident>(this.accidents);
    this.dataSource.paginator = this.paginator;
  }
}
