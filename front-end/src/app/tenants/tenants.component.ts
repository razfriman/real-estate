import { Component, OnInit } from '@angular/core';
import { Tenant } from './shared/tenant';
import { TenantService } from './shared/tenant.service';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Component({
  selector: 'app-tenants',
  templateUrl: './tenants.component.html',
  styleUrls: ['./tenants.component.scss']
})
export class TenantsComponent implements OnInit {

  tenants: Tenant[];

  constructor(
    private tenantService: TenantService
  ) {
    this.tenants = [];
    this.getTenants();
  }

  getTenants() {
    this.tenantService.getTenants()
      .subscribe(resp => this.tenants = resp,
      error => console.log(error));
  }

  ngOnInit() {
  }

}
