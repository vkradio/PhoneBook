import { Component } from '@angular/core';
import { Repository } from '../models/repository';
import { Contact } from '../models/contact.model';

@Component({
  selector: 'app-phonebook',
  templateUrl: 'phoneBook.component.html'
})
export class PhoneBookComponent {
  constructor(private repo: Repository) { }

  tableMode = true;

  get contact() {
    return this.repo.contact;
  }

  selectContact(id: number) {
    this.repo.getContact(id);
  }

  saveContact() {
    if (this.repo.contact.contactId == null) {
      this.repo.createContact(this.repo.contact);
    } else {
      this.repo.replaceContact(this.repo.contact);
    }
    this.clearContact();
  }

  deleteContact(id: number) {
    this.repo.deleteContact(id);
  }

  clearContact() {
    this.repo.contact = new Contact();
    this.tableMode = true;
  }

  get contacts() {
    return this.repo.contacts;
  }
}
