export interface Product {
    guid: string;
    name: string;
    price: number;
    description: string | null;
    minQuantity: number;
    categoryName: string | null;
}
