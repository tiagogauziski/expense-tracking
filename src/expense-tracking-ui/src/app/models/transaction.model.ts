import { Category } from "./category.model";

export interface Transaction {
    id?: string;
    categoryId?: string;
    category?: Category;
}