import { CUSTOM_ELEMENTS_SCHEMA, Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import * as LR from "@uploadcare/blocks";
import { OutputFileEntry } from '@uploadcare/blocks';
import "@uploadcare/blocks/web/lr-file-uploader-regular.min.css";
import { ProfileService } from '../../services/profile.service';
import { IUser } from '../../../../models/iuser';
import { AlertService } from '../../../../services/alert.service';
import { ProfileStore } from '../../../../shared/store/profile.store';
@Component({
  selector: 'app-uploader',
  standalone: true,
  imports: [],
  templateUrl: './uploader.component.html',
  styleUrl: './uploader.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class UploaderComponent implements OnInit {

  profile = inject(ProfileService)
  alert = inject(AlertService)
  profileStore = inject(ProfileStore)

  file: OutputFileEntry<'success'> | null = null;
  @ViewChild('ctxProvider', { static: true }) ctxProviderRef!: ElementRef<
    InstanceType<LR.UploadCtxProvider>
  >;

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
  }

  handleChangeEvent = (e: LR.EventMap['change']) => {
    console.log('change event payload:', e);

    const successfulEntries = e.detail.allEntries.filter(f => f.status === 'success') as OutputFileEntry<'success'>[];
    this.file = successfulEntries.length > 0 ? successfulEntries[0] : null;
    const user : IUser = {
      profilePicture : this.file?.cdnUrl
    }
    console.log(user);
    
    this.profile.update(user).subscribe({
      complete:()=>{
        this.alert.success("profile picture updated successfully");
        this.profileStore.loadAll();
      },
      error:(error:any)=>this.alert.error(`${error.status} : ${error.error[0].title}`)
    })

    console.log(this.file);
  //   {
  //     "uuid": "0203bb22-9013-452b-be48-49def20e146e",
  //     "internalId": "QuGQ4Q6e5-3Mq",
  //     "name": "1701489461691.jfif",
  //     "size": 103382,
  //     "isImage": true,
  //     "mimeType": "image/jpeg",
  //     "file": {},
  //     "externalUrl": null,
  //     "cdnUrlModifiers": "-/crop/429x600/190,20/-/preview/",
  //     "cdnUrl": "https://ucarecdn.com/0203bb22-9013-452b-be48-49def20e146e/-/crop/429x600/190,20/-/preview/",
  //     "fullPath": null,
  //     "uploadProgress": 100,
  //     "fileInfo": {
  //         "uuid": "0203bb22-9013-452b-be48-49def20e146e",
  //         "name": "1701489461691.jfif",
  //         "size": 103382,
  //         "isStored": true,
  //         "isImage": true,
  //         "mimeType": "image/jpeg",
  //         "cdnUrl": "https://ucarecdn.com/0203bb22-9013-452b-be48-49def20e146e/",
  //         "s3Url": null,
  //         "originalFilename": "1701489461691.jfif",
  //         "imageInfo": {
  //             "dpi": null,
  //             "width": 800,
  //             "format": "JPEG",
  //             "height": 800,
  //             "sequence": false,
  //             "colorMode": "RGB",
  //             "orientation": null,
  //             "geoLocation": null,
  //             "datetimeOriginal": null
  //         },
  //         "videoInfo": null,
  //         "contentInfo": {
  //             "mime": {
  //                 "mime": "image/jpeg",
  //                 "type": "image",
  //                 "subtype": "jpeg"
  //             },
  //             "image": {
  //                 "dpi": null,
  //                 "width": 800,
  //                 "format": "JPEG",
  //                 "height": 800,
  //                 "sequence": false,
  //                 "colorMode": "RGB",
  //                 "orientation": null,
  //                 "geoLocation": null,
  //                 "datetimeOriginal": null
  //             }
  //         },
  //         "metadata": {},
  //         "s3Bucket": null,
  //         "defaultEffects": null
  //     },
  //     "metadata": {},
  //     "isSuccess": true,
  //     "isUploading": false,
  //     "isFailed": false,
  //     "isRemoved": false,
  //     "errors": [],
  //     "status": "success"
  // }
  };

}
