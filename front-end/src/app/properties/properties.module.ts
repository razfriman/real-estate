import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { PropertyCardComponent } from './property-card/property-card.component';
import { PropertiesComponent } from './properties.component';
import { PropertyService } from './shared/property.service';
import { PropertiesRouting } from './properties.routing';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        PropertiesRouting
    ],
    declarations: [
        PropertiesComponent,
        PropertyCardComponent
    ],
    providers: [
        PropertyService
    ]
})
export class PropertiesModule { }
