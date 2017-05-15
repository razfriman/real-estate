import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
    {
        path: 'home',
        loadChildren: 'app/home/home.module#HomeModule'
    },
    {
        path: 'properties',
        loadChildren: 'app/properties/properties.module#PropertiesModule',
        canActivate: [AuthGuard]

    },
    {
        path: 'tenants',
        loadChildren: 'app/tenants/tenants.module#TenantsModule',
        canActivate: [AuthGuard]

    },
    {
        path: '**',
        redirectTo: '/home', pathMatch: 'full'
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    providers: [
        AuthGuard
    ],
    exports: [
        RouterModule
    ],
})
export class AppRouting { }