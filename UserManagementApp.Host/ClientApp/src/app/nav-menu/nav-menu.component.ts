import {Component, Inject} from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(@Inject('BASE_URL') private baseUrl: string) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  getSwaggerLink(): string {
    return `${this.baseUrl}swagger`;
  }
}
