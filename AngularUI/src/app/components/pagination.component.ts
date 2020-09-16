import { Component } from '@angular/core';
import { NavigationService } from '../models/navigation.service';

@Component({
  selector: 'app-pagination',
  templateUrl: 'pagination.component.html'
})
export class PaginationComponent {
  constructor(public navigation: NavigationService) { }

  get pages(): number[] {
    if (this.navigation.contactCount > 0) {
      return Array(Math.ceil(this.navigation.contactCount / this.navigation.contactsPerPage))
        .fill(0)
        .map((x, i) => i + 1);
    } else {
      return [];
    }
  }
}
