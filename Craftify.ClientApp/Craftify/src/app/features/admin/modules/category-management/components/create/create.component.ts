import { Component, inject } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../../../../../services/alert.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent {
  cateService = inject(CategoryService)
  router = inject(Router)
  alertService = inject(AlertService)
  data: any = {}
  labels: string[] = [
    'categoryName',
    'minmumPrice',
    'maximumPrice',
  ];


  handleFormSubmit(data: any) {
    console.log('Form submitted:', data);

    this.cateService.create(
      {
        picture: data?.imageUrls && data.imageUrls.length > 0 ? data.imageUrls[0] : 'null',
        categoryName: data.categoryName,
        minmumPrice: data.minmumPrice,
        maximumPrice: data.maximumPrice

      }
    ).subscribe({
      complete: () => this.alertService.success("category updated successfully"),
      error: (err: any) => this.alertService.error(err?.error?.title ?? err)
    })

  }


}
