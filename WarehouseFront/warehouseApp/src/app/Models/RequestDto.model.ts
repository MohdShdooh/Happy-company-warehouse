export class signInDto { 
    Email!: string ; 
    Password!:string ;
}

export class AddUserRequest { 
    Name!:string ; 
    Email!:string ; 
    Password!:string;
    Role!:number ; 
    status!:number ;
}

export class EditUserRequest extends AddUserRequest{ 
 Id! : number ;
}

export class changePasswordRequest { 
    Id!:number ; 
    Password!:string ;
}

export class DeleteUserRequest{ 
    Id!:number ;
}

export class AddWareHouseRequest { 
    Name!:string;
    Address!: string ;
    City!: string ;
    Country!: string ;
}

export class GetItemsRequest { 
    WarehouseId!: number;
    PageNumber: number = 1;
    PageSize: number = 10;
}

export class CreateItemRequest { 
    Name!:string ;
    SKUCode? : string ;
    Quantity!:number ; 
    Cost!:number ; 
    MSRPPrice?:number ; 
    WarehouseId!:number ;
}