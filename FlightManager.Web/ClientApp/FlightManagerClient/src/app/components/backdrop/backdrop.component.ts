import { Component, ContentChild, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-backdrop',
  templateUrl: './backdrop.component.html',
  styleUrl: './backdrop.component.scss'
})
export class BackdropComponent implements OnInit, OnDestroy {
  @ContentChild("ngContentElement", {static: true}) ngContentElement: ElementRef;

  constructor(private _location: Location) { }

  ngOnInit() {
    document.body.classList.add("hide-overflow");
  }

  ngOnDestroy() {
    document.body.classList.remove("hide-overflow");
  }

  onReturnToLastPage(event: Event) {
    if(!this.ngContentElement.nativeElement.contains(event.target)) {
      this._location.back();
    }
  }
}
