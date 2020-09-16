import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PhoneBookComponent } from './components/phoneBook.component';


const routes: Routes = [
  { path: 'phonebook/:page', component: PhoneBookComponent },
  { path: 'phonebook', redirectTo: 'phonebook/', pathMatch: 'full' },
  { path: '', redirectTo: 'phonebook/', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
