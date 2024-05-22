import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProfileService } from '../../../auth/services/profile.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrl: './security.component.css'
})
export class SecurityComponent implements OnInit{
  @Input({ required: true }) email!: string;
  isEmailValid = false;
  constructor(
    private profile: ProfileService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute,
    private auth : AuthService
  ) { }

  delete() {
    if (confirm("do you want to delete this accout?")) {
      this.profile.delete().subscribe({
        complete: () => {
          this.toastr.success("User Delete successfully");
          this.profile.logout();
          this.router.navigate(['/login'])
        }
      });
    }

  }
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
    });
    this.auth.forgetPassword(this.email).subscribe({
      complete:()=>{
        this.isEmailValid = true;
      },
      error:(err)=>this.toastr.error(err.error,err.status)
    });
  }
}
