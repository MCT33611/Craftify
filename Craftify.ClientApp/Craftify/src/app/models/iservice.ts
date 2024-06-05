export interface IService {
    providerId: string;
    title: string;
    description: string;
    categoryId: string;
    price?: number;
    availability: boolean;
    zipCode: string;
    newPicUrls?:string[];
}
