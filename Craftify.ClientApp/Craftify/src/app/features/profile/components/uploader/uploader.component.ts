import { CUSTOM_ELEMENTS_SCHEMA, Component, ElementRef, OnInit, OnDestroy, ViewChild, inject } from '@angular/core';
import * as LR from "@uploadcare/blocks";
import { OutputFileEntry } from '@uploadcare/blocks';
import "@uploadcare/blocks/web/lr-file-uploader-regular.min.css";
import { ProfileService } from '../../services/profile.service';
import { IUser } from '../../../../models/iuser';
import { AlertService } from '../../../../services/alert.service';
import { ProfileStore } from '../../../../shared/store/profile.store';
import { HttpErrorResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-uploader',
  standalone: true,
  imports: [],
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.css',], // fixed typo styleUrl -> styleUrls
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class UploaderComponent implements OnInit, OnDestroy {

  profile = inject(ProfileService)
  alert = inject(AlertService)
  profileStore = inject(ProfileStore)

  file: OutputFileEntry<'success'> | null = null;
  @ViewChild('ctxProvider', { static: true }) ctxProviderRef!: ElementRef<
    InstanceType<LR.UploadCtxProvider>
  >;

  private subscriptions: Subscription = new Subscription(); // Subscription object to manage subscriptions

  ngOnInit(): void {
    LR.registerBlocks(LR);
    this.ctxProviderRef.nativeElement.addEventListener(
      'change',
      this.handleChangeEvent,
    );
  }

  ngOnDestroy() {
    this.ctxProviderRef.nativeElement.removeEventListener(
      'change',
      this.handleChangeEvent,
    );
    this.subscriptions.unsubscribe(); // Unsubscribe from all subscriptions
  }

  handleChangeEvent = (e: LR.EventMap['change']) => {
    const successfulEntries = e.detail.allEntries.filter(f => f.status === 'success') as OutputFileEntry<'success'>[];
    this.file = successfulEntries.length > 0 ? successfulEntries[0] : null;
    const user: IUser = {
      profilePicture: this.file?.cdnUrl ?? ''
    };

    const sub = this.profile.update(user).subscribe({
      complete: () => {
        this.alert.success("Profile picture updated successfully");
        this.profileStore.loadAll();
      },
      error: (error: HttpErrorResponse) => this.alert.error(`${error.status} : ${error.error[0].title}`)
    });
    this.subscriptions.add(sub); // Add subscription to the Subscription object
  };

}
