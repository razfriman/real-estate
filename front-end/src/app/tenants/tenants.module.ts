import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { TenantCardComponent } from './tenant-card/tenant-card.component';
import { TenantsComponent } from './tenants.component';
import { TenantService } from './shared/tenant.service';
import { TenantsRouting } from './tenants.routing';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        TenantsRouting
    ],
    declarations: [
        TenantsComponent,
        TenantCardComponent
    ],
    providers: [
        TenantService
    ]
})
export class TenantsModule { }
