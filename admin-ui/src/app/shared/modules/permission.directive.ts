import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { TokenStorageService } from './../services/token-storage.service';

@Directive({
  selector: '[appPermission]'
})
export class PermissionDirective implements OnInit {

  @Input() appPolicy!: string;

  constructor(
    private el: ElementRef,
    private tokenService: TokenStorageService
  ) { }

  ngOnInit(): void {
    setTimeout(() => {
      const loggedInUser = this.tokenService.getUser();

      if (!loggedInUser || !loggedInUser.permissions) {
        this.el.nativeElement.remove();
        return;
      }

      const hasPermission = loggedInUser.permissions
        .some((x: string) => x === this.appPolicy);

      if (!hasPermission) {
        this.el.nativeElement.remove(); 
      }
    });
  }
}
