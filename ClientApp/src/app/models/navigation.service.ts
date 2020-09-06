import { Injectable } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Repository } from './repository';
import { filter } from 'rxjs/operators';
import { Subject, Observable } from 'rxjs';

export interface NavigationUpdate {
  page: number;
}

@Injectable()
export class NavigationService {
  private changeSubject = new Subject<NavigationUpdate>();

  constructor(
    private repo: Repository,
    private router: Router,
    private active: ActivatedRoute) {

    router
      .events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(ev => this.handleNavigationChange());
  }

  private handleNavigationChange() {
    const active = this.active.firstChild.snapshot;
    if (active.url.length > 0 && active.url[0].path === 'phonebook') {
      this.repo.filter.search = '';
      if (active.params.page !== undefined) {
        const value = Number.parseInt(active.params.page, 10);
        if (!Number.isNaN(value)) {
          this.repo.paginationObject.currentPage = value;
        } else {
          this.repo.paginationObject.currentPage = 1;
        }
      } else {
        this.repo.paginationObject.currentPage = Number.parseInt(active.params.page, 10) || 1;
      }
      this.repo.getContacts();
      this.changeSubject.next({
        page: this.currentPage
      });
    }
  }

  get change(): Observable<NavigationUpdate> {
    return this.changeSubject;
  }

  get currentPage() {
    return this.repo.paginationObject.currentPage;
  }

  set currentPage(newPage: number) {
    this.router.navigateByUrl(`/phonebook/${newPage}`);
  }

  get contactsPerPage() {
    return this.repo.paginationObject.contactsPerPage;
  }

  get contactCount() {
    return (this.repo.contacts || []).length;
  }
}
