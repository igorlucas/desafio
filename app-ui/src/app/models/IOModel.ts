import { Resource } from './Resource';

export class IOModel {
    id: number;
    responsible: string;
    resourceId: number;
    resource: Resource;
    quantity: number;
}