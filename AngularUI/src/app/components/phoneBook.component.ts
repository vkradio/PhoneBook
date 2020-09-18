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

  get contacts() {
    if (this.repo.contacts != null && this.repo.contacts.length > 0) {
      const p = this.repo.paginationObject;
      const pageIndex = (p.currentPage - 1) * p.contactsPerPage;
      return this
        .repo
        .contacts
        .slice(pageIndex, pageIndex + p.contactsPerPage);
    }
  }

  selectContact(id: number) {
    this.repo.getContact(id);
  }

  saveContact() {
    if (this.repo.contact.id == null) {
      this.repo.createContact(this.repo.contact);
    } else {
      this.repo.replaceContact(this.repo.contact);
    }
    this.clearContact();

    this.repo.filter.search = '';
    this.repo.getContacts();
  }

  deleteContact(id: number) {
    this.repo.deleteContact(id);

    this.repo.filter.search = '';
    this.repo.getContacts();
  }

  clearContact() {
    this.repo.contact = new Contact();
    this.tableMode = true;
  }
}
