import { Tenant } from '../../tenants/shared/tenant';

export interface Property {
    title: string;
    propertyID: number;
    tenant: Tenant;
}
