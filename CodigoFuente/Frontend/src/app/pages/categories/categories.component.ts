import { Component } from '@angular/core';
import { HotToastService } from '@ngneat/hot-toast';
import { FormsModule } from '@angular/forms';
import { NgIf, NgFor } from '@angular/common';
import { LoadingService } from '../../services/loading.service';
import { CategoryModel } from '../../services/types';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [FormsModule, NgIf, NgFor],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})

export class CategoriesComponent {

  constructor(
    private _categoryService : CategoryService, 
    private _toastService: HotToastService, 
    private _loadingService: LoadingService
  ) { }

  modalVisible: boolean = false;
  name: string = '';
  emptyFields: boolean = false;

  categories: CategoryModel[] = [];
  relatedCategory: number = -1;

  ngOnInit(): void {
    this.getCategories();
  }

  createCategoryModal() {
    this.modalVisible = true;
  }

  hideModal() {
    this.modalVisible = false;
  }

  selectRelatedCategory(categoryId: number) {
    this.relatedCategory = categoryId;
  }

  submitModal() {
    this.checkEmptyFields();
    if (this.emptyFields) {
      return;
    }
    if (this.relatedCategory !== -1) {
      this.createCategory(this.relatedCategory);
      return;
    }
    this.createCategory();
  }

  findCategoryById(id: number): string | undefined {
    return this.categories.find((category) => category.id === id)?.name;
  }

  checkEmptyFields() {
    this.emptyFields = this.name === '';
    if (this.emptyFields) {
      this._toastService.error('No se admiten campos vacios');
    }
  }

  getCategories() {
    this._loadingService.loadingOn();
    this._categoryService.getCategories()
      .subscribe((response) => {
        this.categories = response;

        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }

  createCategory(relatedCategory?: number) {
    this._categoryService.createCategory(this.name, relatedCategory)
      .pipe(
        this._toastService.observe({
          loading: 'Creando categoria',
          success: 'Categoria creada con exito',
          error: (e) => e?.error || 'Error al crear categoria',
        })
      )
      .subscribe(
        (response) => {
          this.hideModal();
          this.getCategories();
        },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }
}
