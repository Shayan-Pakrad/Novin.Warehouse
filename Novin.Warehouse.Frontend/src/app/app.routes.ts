import { Routes } from '@angular/router';
import { PublicSharedComponent } from './+public/public-shared/public-shared.component';
import { InventoryComponent } from './+public/+pages/inventory/inventory.component';

export const routes: Routes = [
    {
        path: 'public', component: PublicSharedComponent, children: [
            { path: 'inventory', component: InventoryComponent },
            { path: '', redirectTo: 'home', pathMatch: 'prefix' }
        ]
    },
    { path: '', redirectTo: 'public', pathMatch: 'full' }
];
