import { Contact } from './contact.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Filter, Pagination } from './configClasses.repository';

const contactsUrl = '/api/contacts';

@Injectable()
export class Repository {
  contact: Contact;
  contacts: Contact[];
  filter = new Filter();
  paginationObject = new Pagination();

  constructor(private http: HttpClient) {
    this.getContacts();
  }

  getContact(id: number) {
    this
      .http
      .get<Contact>(`${contactsUrl}/${id}`)
      .subscribe(c => this.contact = c);
  }

  getContacts() {
    let url = contactsUrl;
    if (this.filter.search) {
      url += `?filter=${this.filter.search}`;
    }
    this
      .http
      .get<Contact[]>(url)
      .subscribe(cs => this.contacts = cs);
  }

  createContact(con: Contact) {
    const data = {
      name: con.name,
      telephone: con.telephone
    };

    this
      .http
      .post<number>(contactsUrl, data)
      .subscribe(id => {
        con.contactId = id;
        this.contacts.push(con);
      });
  }

  replaceContact(con: Contact) {
    const data = {
      name: con.name,
      telephone: con.telephone
    };

    this
      .http
      .put(`${contactsUrl}/${[con.contactId]}`, data)
      .subscribe(() => this.getContacts());
  }

  deleteContact(id: number) {
    this
      .http
      .delete(`${contactsUrl}/${id}`)
      .subscribe(() => this.getContacts());
  }
}
