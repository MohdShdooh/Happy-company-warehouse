import { ItemDto, UserDto, WareHouseDto } from "./Entities.model";

export class BaseResponse {
    Message!: string;
    ErrorCode!: number;
    Result!: boolean;

}

export class AuthenticateResponse extends BaseResponse { 

    User!: UserDto;
    Token! : string ;
}

export class GetAllUsersResponse extends BaseResponse { 
    Users! : UserDto[] ;
}

export class GetWareHousesResponse extends BaseResponse { 

    wareHouses! : WareHouseDto[];
}

export class GetItemsResponse extends BaseResponse { 
    items!:ItemDto[];
}