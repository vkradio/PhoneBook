import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ModelModule } from './models/model.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorHandlerService } from './errorHandler.service';
import { ContactEditorComponent } from './components/contactEditor.component';
import { PhoneBookComponent } from './components/phoneBook.component';
import { PaginationComponent } from './components/pagination.component';
import { NavigationService } from './models/navigation.service';

@NgModule({
  declarations: [
    AppComponent,
    ContactEditorComponent,
    PhoneBookComponent,
    PaginationComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    ModelModule,
    FormsModule
  ],
  providers: [
    NavigationService,
    ErrorHandlerService,
    {
      provide: HTTP_INTERCEPTORS,
      useExisting: ErrorHandlerService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
