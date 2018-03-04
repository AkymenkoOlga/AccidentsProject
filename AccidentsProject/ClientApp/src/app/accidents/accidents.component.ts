import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Appcomponent = require("../app.component");
import Accident = Appcomponent.Accident;

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

