export class Product {
  id: number;
  price: number;
  title: string;
  name: string;
  description: string;
  picture: string[];
  available: AvailableStatus;
  sellerId: number; 
  requestCount: number;
};

export enum AvailableStatus
    {
        NotAvailable, WaitForAvailable, Available 
    };