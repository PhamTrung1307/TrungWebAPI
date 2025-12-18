import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { UtilityService } from '../../../shared/services/utility.service'; 
import { AdminApiPostApiClient, AdminApiPostCategoryApiClient, PostCategoryDTO, PostDTO }  from '../../../api/admin-api.service.generated';
import { UploadService } from '../../../shared/services/upload.service';
import { environment } from '../../../../environments/environment'; 
@Component({
  templateUrl: 'post-detail.component.html',
})
export class PostDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form!: FormGroup;
  public title!: string;
  public btnDisabled = false;
  public saveBtnName!: string;
  public postCategories: any[] = [];
  public contentTypes: any[] = [];
  public series: any[] = [];

  selectedEntity = {} as PostDTO;
  public thumbnailImage;

  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private postApiClient: AdminApiPostApiClient,
    private postCategoryApiClient: AdminApiPostCategoryApiClient,
    private uploadService: UploadService
  ) { }
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public generateSlug() {
    var slug = this.utilService.makeSeoTitle(this.form.get('name').value);
    this.form.controls['slug'].setValue(slug);
  }
  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    slug: [{ type: 'required', message: 'Bạn phải URL duy nhất' }],
    description: [{ type: 'required', message: 'Bạn phải nhập mô tả ngắn' }],
  };

ngOnInit() {
  this.buildForm();

  const categories = this.postCategoryApiClient.getPostCategories();

  this.toggleBlockUI(true);
  forkJoin({ categories })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: any) => {
        response.categories.forEach(element => {
          this.postCategories.push({
            value: element.id,
            label: element.name,
          });
        });

        if (this.config.data?.id) {
          this.loadFormDetails(this.config.data.id);
        } else {
          this.toggleBlockUI(false);
        }
      },
      error: () => this.toggleBlockUI(false)
    });
}

loadFormDetails(id: string) {
  this.postApiClient.getPostById(id)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: PostDTO) => {
        this.selectedEntity = response;

        this.form.patchValue({
          name: response.name,
          slug: response.slug,
          categoryId: response.categoryId,
          description: response.description,
          seoDescription: response.seoDescription,
          tags: response.tags,
          content: response.content,
          thumbnail: response.thumbnail
        });

        if (response.thumbnail) {
          this.thumbnailImage = environment.API_URL + response.thumbnail;
        }

        this.toggleBlockUI(false);
      },
      error: () => this.toggleBlockUI(false)
    });
}



  onFileChange(event) {
    if (event.target.files && event.target.files.length) {
      this.uploadService.uploadImage('posts', event.target.files)
        .subscribe({
          next: (response: any) => {
            this.form.controls['thumbnail'].setValue(response.path);
            this.thumbnailImage = environment.API_URL + response.path;
          },
          error: (err: any) => {
            console.log(err);
          }
        });
    }
  }
  saveChange() {
    this.toggleBlockUI(true);
    this.saveData();
  }

  private saveData() {
    this.toggleBlockUI(true);
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.postApiClient
        .createPost(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.ref.close(this.form.value);
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    } else {
      this.postApiClient
        .updatePost(this.config.data?.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);

            this.ref.close(this.form.value);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    }
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanelDetail = false;
      }, 1000);
    }
  }
buildForm() {
  this.form = this.fb.group({
    name: [null, [Validators.required, Validators.maxLength(255), Validators.minLength(3)]],
    slug: [null, Validators.required],
    categoryId: [null, Validators.required],
    description: [null, Validators.required],
    seoDescription: [null],
    tags: [null],
    content: [null],
    thumbnail: [null],
  });
}

}