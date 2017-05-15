import { Component, Input, OnInit } from '@angular/core';
import { Tenant } from '../shared/tenant';

@Component({
  selector: 'app-tenant-card',
  templateUrl: './tenant-card.component.html',
  styleUrls: ['./tenant-card.component.scss']
})
export class TenantCardComponent implements OnInit {

  @Input() item: Tenant;

  constructor() { }

  ngOnInit() {
  }
}
