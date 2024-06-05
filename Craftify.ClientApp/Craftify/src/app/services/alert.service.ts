import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  // Method to show success toast
  success(message: string) {
    Swal.fire({
      icon: 'success',
      title: message,
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000 // 3 seconds
    });
  }

  // Method to show error toast
  error(message: string) {
    Swal.fire({
      icon: 'error',
      title: message,
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000 // 3 seconds
    });
  }

  // Method to show warning toast
  warning(message: string) {
    Swal.fire({
      icon: 'warning',
      title: message,
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000 // 3 seconds
    });
  }

  // Method to show regular notification
  notification(title: string, message: string, icon: 'success' | 'error' | 'warning' = 'success') {
    Swal.fire({
      icon: icon,
      title: title,
      text: message
    });
  }

  // Method to show confirmation dialog centered on screen with red color
  confirm(title: string, message: string): Observable<boolean> {
    const resultSubject = new Subject<boolean>();

    Swal.fire({
      title: title,
      text: message,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!'
    }).then((result) => {
      if (result.isConfirmed) {
        resultSubject.next(true);
      } else {
        resultSubject.next(false);
      }
      resultSubject.complete();
    });

    return resultSubject.asObservable();
  }
}
