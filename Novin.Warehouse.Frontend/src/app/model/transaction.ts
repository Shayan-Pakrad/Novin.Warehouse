import { Product } from "./product";

export interface Transaction {
    guid: string;
    transactionType: 'Receive' | 'Dispatch';
    quantity: number;
    transactionDate: Date;
    product: Product
}