import { Category } from "./category.model";

export interface ImportRule {
    id?: string;
    name: string;
    condition: string;
    categoryId: string;
    category?: Category;
}