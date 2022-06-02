import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorToastService {
  toasts: any[] = [];

  constructor() { }

  show(body: string) {
    this.toasts.pop();
    this.toasts.push({ body });
  }
}
