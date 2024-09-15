export class UserDto {
    Id!: number;
    Name!: string;
    Email!: string;
    UserStatus!: UserStatus;
    Role!: UserRole;
}


export enum UserStatus { 
    Active = 0, 
    InActive = 1
}


export enum UserRole { 
    Admin = 0 ,
    Management =1 ,
    Auditor = 2 
}


export class userPreview { 
    Id!: number;
    Name!: string;
    Email!: string;
    UserStatus!: string;
    Role!: string;

}

export interface LookupItem {
    value: UserStatus | UserRole;
    label: string;
  }
  
  export class WareHouseDto { 
    Id!:number ; 
    Name!:string ; 
    Address!:string;
    City!:string ; 
    Country!:string ; 
  }

  export class Countries { 
    coutries! : string[];
  }

  export class ItemDto {
    Id!: number;
    Name!: string;
    SKUCode?: string;
    Quantity!: number;
    Cost!: number;
    MSRPPrice?: number;
  }