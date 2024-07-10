// service-list.component.ts
import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { IWorker } from '../../../../models/iworker';
import { CustomerService } from '../../services/customer.service';
import { HttpErrorResponse } from '@angular/common/http';
import { AlertService } from '../../../../services/alert.service';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.scss']
})
export class ServiceListComponent implements OnInit {
  @ViewChild('scrollContainer') scrollContainer!: ElementRef;

  workers: IWorker[] = [];

  filteredWorkers: IWorker[] = this.workers;
  searchTerm: string = '';
  selectedCategories: string[] = [];
  categories: string[] = [
    "Plumbing",
    "Wiring",
    "Construction",
    "Painting",
    "Landscaping",
    "Cleaning",
    "HVAC",
    "Carpentry",
    "Auto Repair",
    "IT Support",
    "Personal Training",
    "Tutoring",
    "Pet Grooming"
  ];

  customerService = inject(CustomerService)
  _alert = inject(AlertService)

  ngOnInit() {
    this.loadWorkers();
    this.applyFilters();
  }

  loadWorkers() {
    this.customerService.getAllWorkers().subscribe({
      next: (res: IWorker[]) => {
        this.workers = res.filter((w)=>w.approved);
        this.applyFilters(); // Apply filters after data is loaded
        console.log(this.workers);
      },
      error: (err: HttpErrorResponse) => {
        this._alert.error("Something went wrong");
      }
    });
  }

  applyFilters() {
    this.filteredWorkers = this.workers.filter(worker =>
      (this.searchTerm === '' || worker.serviceTitle?.toLowerCase().includes(this.searchTerm.toLowerCase())) &&
      (this.selectedCategories.length === 0 || this.selectedCategories.includes(worker.serviceTitle || ''))
    );
  }

  toggleCategory(category: string) {
    const index = this.selectedCategories.indexOf(category);
    if (index > -1) {
      this.selectedCategories.splice(index, 1);
    } else {
      this.selectedCategories.push(category);
    }
    this.applyFilters();
  }

  scrollLeft() {
    this.scrollContainer.nativeElement.scrollBy({ left: -200, behavior: 'smooth' });
  }

  scrollRight() {
    this.scrollContainer.nativeElement.scrollBy({ left: 200, behavior: 'smooth' });
  }

  loadMore() {
    // Implement load more functionality here
    console.log('Loading more services...');
  }
}