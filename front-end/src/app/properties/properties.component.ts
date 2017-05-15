import { Component, OnInit } from '@angular/core';
import { Property } from './shared/property';
import { PropertyService } from './shared/property.service';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.scss']
})
export class PropertiesComponent implements OnInit {

  properties: Property[];

  constructor(
    private propertyService: PropertyService
  ) {
    this.properties = [];
    this.getProperties();
  }

  getProperties() {
    this.propertyService.getProperties()
      .subscribe(resp => this.properties = resp,
      error => console.log(error));
  }

  ngOnInit() {
  }

}
