import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { WarehouseComponent } from './Components/warehouse/warehouse.component';
import { UsersComponent } from './Components/users/users.component';
import { StatsticsComponent } from './Components/statstics/statstics.component';
import { AddUserComponent } from './Components/users/add-user/add-user.component';
import { EditUserComponent } from './Components/users/edit-user/edit-user.component';
import { ListUsersComponent } from './Components/users/list-users/list-users.component';
import { changePasswordRequest } from './Models/RequestDto.model';
import { ChangePasswordComponent } from './Components/users/change-password/change-password.component';
import { ListWarehousesComponent } from './Components/warehouse/list-warehouses/list-warehouses.component';
import { AddWarehouseComponent } from './Components/warehouse/add-warehouse/add-warehouse.component';
import { ItemsComponent } from './Components/items/items.component';
import { AddItemComponent } from './Components/items/add-item/add-item.component';
import { ListItemsComponent } from './Components/items/list-items/list-items.component';

export const routes: Routes = [
    { 
        path:'', redirectTo:'login', pathMatch:'full'
    }, 
    { 
        path:'login', component :LoginComponent
    }, 
    {
        path: 'dashboard',
        component: DashboardComponent,
        children: [
          { path: '', redirectTo: 'statstics', pathMatch: 'full' },
          { path: 'warehouses', 
            component: WarehouseComponent,
            children: [
              { path: '', redirectTo: 'list', pathMatch: 'full' },
              { path: 'add', component: AddWarehouseComponent },
              { path: 'list', component: ListWarehousesComponent } ,
              { path: 'items', component: ItemsComponent, 
                children:[
                  { path: '', redirectTo: 'list/:id', pathMatch: 'full' },
                  { path: 'add/:id', component: AddItemComponent },
                  { path: 'list/:id', component: ListItemsComponent } ,
                ]
               } ,
          ]
           },
          { path: 'users', component: UsersComponent ,
            children: [
                { path: '', redirectTo: 'list', pathMatch: 'full' },
                { path: 'add', component: AddUserComponent },
                { path: 'edit/:id', component: EditUserComponent }, 
                { path: 'list', component: ListUsersComponent } ,
                { path: 'changePassword/:id', component: ChangePasswordComponent } 

            ]},
          { path: 'statstics', component: StatsticsComponent },
        ]
      },
];
