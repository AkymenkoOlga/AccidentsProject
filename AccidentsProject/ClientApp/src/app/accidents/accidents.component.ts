import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accidents',
  templateUrl: './accidents.component.html'
})
export class AccidentsComponent {
  public accidents: Accident[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Accident[]>(baseUrl + 'api/accidents').subscribe(result => {
      this.accidents = result;
    }, error => console.error(error));
  }
}

interface Accident {
  id: string;
  date: Date;
  severity: string;
}
