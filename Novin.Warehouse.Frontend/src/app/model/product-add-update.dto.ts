export interface CreateUpdateProductDTO {
    name: string;
    price: number;
    categoryGuid: string;
    description: string;
    minQuantity: number;
}