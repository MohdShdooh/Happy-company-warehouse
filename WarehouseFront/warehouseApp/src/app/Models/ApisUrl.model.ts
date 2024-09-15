export class  Apis{ 
    // i make this approch because if we want to change the evn we will change the base only in one place . 
    protected baseUrl = "https://localhost:7252/api/" ; 
}

export class AuthenticateApi extends Apis { 
     private readonly controllerName = "Auth";
     readonly signInApi = this.baseUrl + this.controllerName + "/SignIn" ;
}

export class UsersApi extends Apis { 
    private readonly controllerName = "Users";
    readonly GetAllUsers = this.baseUrl + this.controllerName + "/GetAllUsers" ;
    readonly CreateUser = this.baseUrl + this.controllerName + "/CreateUser" ;
    readonly GetUserById = this.baseUrl + this.controllerName + "/GetUserById" ;
    readonly UpdateUser = this.baseUrl + this.controllerName + "/UpdateUser" ;
    readonly ChangePasswordById = this.baseUrl + this.controllerName + "/ChangePasswordById" ;
    readonly DeleteUser = this.baseUrl + this.controllerName + "/DeleteUserById" ;
}

export class WareHouseApi extends Apis { 
    private readonly controllerName = "WareHouse";
    readonly GetWareHousesByUserID = this.baseUrl + this.controllerName + "/GetWareHousesByUserID" ;
    readonly GetAllCountries = this.baseUrl + this.controllerName + "/GetAllCountries" ;
    readonly CreateWareHouse = this.baseUrl + this.controllerName + "/CreateWareHouse" ;

}

export class ItemsApi extends Apis { 
    private readonly controllerName = "Items";
    readonly GetItems = this.baseUrl + this.controllerName + "/GetItemsByWareHouseId" ;
    readonly CreateItem = this.baseUrl + this.controllerName + "/CreateNewItem" ;
    readonly GetTopItems = this.baseUrl + this.controllerName + "/GetTopItems" ;
    readonly GetLessItems = this.baseUrl + this.controllerName + "/GetLessItems" ;



}