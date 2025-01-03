import { Routes } from '@angular/router';
import { PublicSharedComponent } from './+public/public-shared/public-shared.component';
import { InventoryComponent } from './+public/+pages/inventory/inventory.component';
import { TransactionsComponent } from './+public/+pages/transactions/transactions.component';
import { ProductsComponent } from './+public/+pages/products/products.component';
import { CategoriesComponent } from './+public/+pages/categories/categories.component';

export const routes: Routes = [
    {
        path: 'public', component: PublicSharedComponent, children: [
            { path: 'inventory', component: InventoryComponent },
            { path: 'transactions', component: TransactionsComponent},
            { path: 'products', component: ProductsComponent},
            { path: 'categories', component: CategoriesComponent},
            { path: '', redirectTo: 'inventory', pathMatch: 'prefix' }
        ]
    },
    { path: '', redirectTo: 'public', pathMatch: 'full' }
];
