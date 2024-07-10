import { Component } from '@angular/core';
import { ILogin } from '../models/login';
import { HttpService } from '../services/httpService';

@Component({
  selector: 'app-account-controller',
  templateUrl: './account-controller.component.html',
  styleUrls: ['./account-controller.component.css']
})
export class AccountControllerComponent {

  email: ILogin | undefined;
  password: ILogin |undefined;

  constructor(httpService: HttpService) {

  }

}
