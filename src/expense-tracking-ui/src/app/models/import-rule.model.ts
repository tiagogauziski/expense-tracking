import { Category } from "./category.model";

export class ImportRule {
    id?: string;
    name?: string;
    condition?: string;
    categoryId?: string;
    category?: Category;
}