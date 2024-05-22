import { Component, Input, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryManagementService } from '../../services/category-management.service';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrl: './category-form.component.css'
})
export class CategoryFormComponent {
  @Input() categoryId = "";
  categoryService = inject(CategoryManagementService);

  categoryForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.categoryForm = this.fb.group({
      id: [this.categoryId],
      name: ['', Validators.required],
      minPrice: ['', [Validators.required, Validators.min(0)]],
      maxPrice: ['', [Validators.required, Validators.min(0)]],
    });
  }

  ngOnInit(): void { }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      console.log(this.categoryForm.value);
      if (this.categoryId == ("" || null || undefined)) {

        this.categoryService.createCategory(this.categoryForm.value)
      }
      else{
        this.categoryService.updateCategory(this.categoryId,this.categoryForm.value)
      }
    }
  }
}
