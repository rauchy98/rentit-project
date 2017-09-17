import { Component, OnInit, Input} from '@angular/core';
import { CategoryService } from 'app/_services';
import { Category } from 'app/_models';

@Component({
	moduleId: module.id,
	selector: 'category-list',
	templateUrl: './category-list.component.html',
	styleUrls: [ './category-list.component.css' ]
})

export class CategoryListComponent implements OnInit {
	categories: Category[];
	condition: boolean = false;
  @Input() categoryId: number;
  
	constructor(private categoryService: CategoryService) {}

	ngOnInit() {
		this.categoryService.getAll().subscribe((categories) => {
			this.categories = categories;
		});
	}

	categorySelect(subcategory) {
		this.condition = true;
	}

	checkSelected(category: Category): boolean {
		return category.id === this.categoryId || category.subcategories.find((x) => x.id === this.categoryId) != null;
	}
}
