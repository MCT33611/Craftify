import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-service-form',
  templateUrl: './service-form.component.html',
  styleUrl: './service-form.component.css'
})
export class ServiceFormComponent implements OnInit {
  serviceForm: FormGroup;
  serviceId: string | null;

  constructor(
    private fb: FormBuilder,
    // private serviceService: ServiceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.serviceForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      price: [null, [Validators.required, Validators.min(0)]],
      availability: [false, Validators.required],
      zipCode: ['', Validators.required],
      categoryId: ['', Validators.required],
      providerId: ['', Validators.required]
    });
    this.serviceId = null;
  }

  ngOnInit(): void {
    this.serviceId = this.route.snapshot.paramMap.get('id');
    // if (this.serviceId) {
    //   this.serviceService.getServiceById(this.serviceId).subscribe(service => {
    //     this.serviceForm.patchValue(service);
    //   });
    // }
  }

  onSubmit(): void {
    if (this.serviceForm.invalid) {
      return;
    }
    const service: any = this.serviceForm.value;

    if (this.serviceId) {
      service.id = this.serviceId;
      // this.serviceService.updateService(service).subscribe(() => {
      //   this.router.navigate(['/services']);
      // });
      
    } else {
      // this.serviceService.createService(service).subscribe(() => {
      //   this.router.navigate(['/services']);
      // });
    }
    console.log(service);

  }
}