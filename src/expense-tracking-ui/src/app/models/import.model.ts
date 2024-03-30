import { Transaction } from "./transaction.model";

export interface Import {
    id?: string;
    name: string;
    layout: string;
    createdAt: string;
    transactions: Transaction[];
    isExecuted: boolean;
}