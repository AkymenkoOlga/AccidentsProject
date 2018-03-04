import { Component, Inject, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accidents',
  templateUrl: './accidents.component.html'
})

export class TablePaginationExample {
  public accidents: Accident[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Accident[]>(baseUrl + 'api/accidents').subscribe(result => {
      this.accidents = result;
    }, error => console.error(error));
  }
  displayedColumns = ['id', 'date', 'severity'];
  dataSource = new MatTableDataSource<Accident>(this.accidents);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
}


export interface Accident {
  id: string;
  date: Date;
  severity: string;
}





