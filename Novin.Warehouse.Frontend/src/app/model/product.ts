export interface Product {
    guid: string;
    name: string;
    price: string;
    description: string | null;
    minQuantity: number;
    categoryName: string | null;
}
