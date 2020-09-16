import { Component } from '@angular/core';
import { Repository } from '../models/repository';

@Component({
  selector: 'app-filter',
  templateUrl: 'filter.component.html'
})
export class FilterComponent {
  constructor(private repo: Repository) { }

  searchModel: string;

  doSearch(searchTerm: string) {
    this.repo.filter.search = searchTerm;
    this.repo.paginationObject.currentPage = 1;
    this.repo.getContacts();
  }

  handleKeyboardEvent(event) {
    if (event.keyCode === 13) {
      this.doSearch(this.searchModel);
    }
  }
}
