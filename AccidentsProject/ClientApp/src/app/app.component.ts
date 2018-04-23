import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
}

export interface Accident {
  id: string;
  externalId: string;
  date: Date;
  location: Location;
  tags: string[];
}

export interface Location {
  coordinates: Coordinates;
}

export interface Coordinates {
  lat: number;
  lon: number;
}
