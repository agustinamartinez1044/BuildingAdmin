import { Component } from '@angular/core';
import { NgIf } from '@angular/common';
import { ConstructionCompanyService } from '../../services/construction-company.service';
import { LoadingService } from '../../services/loading.service';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-construction-company',
  standalone: true,
  imports: [ 
    NgIf
  ],
  templateUrl: './construction-company.component.html',
  styleUrl: './construction-company.component.css'
})
export class ConstructionCompanyComponent {
  constructor(private _constructionCompanyService: ConstructionCompanyService, private _loadingService: LoadingService, private _toastService: HotToastService) { }

  userHasCompany: boolean = false;
  companyName: string = "";
  companyId: number = -1;
  
  ngOnInit() {
    this.getUserCompany();
  }
  
  saveCompany(name: string) {
    this._constructionCompanyService.saveConstructionCompany(name).pipe(
      this._toastService.observe({
        loading: 'Creating construction company',
        success: 'Company created successfully',
        error: 'Error creating company',
      })
    ).subscribe((data) => {
      this.getUserCompany();
      this.userHasCompany = true;
    });
  }

  getUserCompany(){
    this._loadingService.loadingOn();
    this._constructionCompanyService.getConstructionCompany().subscribe(data => {
      this.companyId = data.id;
      this.companyName = data.name;
      this.userHasCompany = true;
      this._loadingService.loadingOff();
    }, (error) => {
      this.userHasCompany = false;
      console.log("User has no company");
      console.log(error);
      this._loadingService.loadingOff();
    });
  }

}
