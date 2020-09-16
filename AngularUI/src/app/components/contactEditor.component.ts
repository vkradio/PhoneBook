import { Component } from '@angular/core';
import { Repository } from '../models/repository';

@Component({
  selector: 'app-contact-editor',
  templateUrl: 'contactEditor.component.html'
})
export class ContactEditorComponent {
  constructor(private repo: Repository) { }

  get contact() {
    return this.repo.contact;
  }
}
