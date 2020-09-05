import { Component } from '@angular/core';
import { Repository } from "./models/repository";
import { Contact } from "./models/contact.model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PhoneBook';

  constructor(private repo: Repository) { }

  get contact(): Contact {
    return this.repo.contact;
  }

  get contacts(): Contact[] {
    return this.repo.contacts;
  }

  createContact() {
    this.repo.createContact(new Contact(
      0,
      'Test Contact',
      '+12345678901'
    ));
  }

  replaceContact() {
    const contact = this.repo.contacts[0];
    contact.name = 'Modified Contact';
    contact.telephone = 'Modified Telephone';
    this.repo.replaceContact(contact);
  }

  deleteContact() {
    this.repo.deleteContact(1);
  }
}
