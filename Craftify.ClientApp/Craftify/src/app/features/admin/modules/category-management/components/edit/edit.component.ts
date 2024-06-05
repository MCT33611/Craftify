import { Component, OnInit, inject } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { ICategory } from '../../../../../../models/icategory';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../../../../../services/alert.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent implements OnInit {

  cateService = inject(CategoryService)
  route = inject(ActivatedRoute);
  router = inject(Router)
  alertService = inject(AlertService)
  data: any = {}
  labels: string[] = [
    'categoryName',
    'minmumPrice',
    'maximumPrice',
  ];
  cateId!: string | null;
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.cateId = params.get('id');

      if (this.cateId) {
        this.cateService.get(this.cateId).subscribe(
          {
            next: (res) => {
              this.data = {
                ...res,
                imageUrls: [res.picture]
              };

            },
            error: (error: any) => {
              console.log(error);

              this.alertService.error(error.error.title)
            }
          }
        )
      } else {
        this.alertService.error("Id is not valid")

      }
    })
  }


  handleFormSubmit(data: any) {
    console.log('Form submitted:', data);
    if (this.cateId) {

      this.cateService.update(
        this.cateId,
        {
          id: this.cateId,
          picture: data?.imagesUrl && data.imagesUrl.length > 0 ? data.imagesUrl[0] : "null",
          categoryName: data.categoryName,
          minmumPrice: data.minmumPrice,
          maximumPrice: data.maximumPrice

        }
      ).subscribe({
        complete: () => this.alertService.success("category updated successfully"),
        error: (err: any) => this.alertService.error(err.error.title)
      })
    }
    else {
      this.alertService.error("Id is not valid")

    }
  }



}
