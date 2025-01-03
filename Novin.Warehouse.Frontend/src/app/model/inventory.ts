import { Product } from "./product";

export interface Inventory {
    guid: string;
    product: Product;
    quantity: number;
    location: string;
}
