using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tools
{
    public enum ErrorCodes
    {
        Success = 0,
        Faild = 100,
        InvalidEmail = 101,
        InActiveUser = 102 ,
        InValidWareHouse =103,
        DoublicateWareHouse = 104 ,
        DoublicateItem = 105 ,
        InvalidItem = 106 ,
        InternalException = 200,
    }

    public enum UserRole
    {
        Admin = 0 ,
        Management =1 ,
        Auditor = 2 
    }
    public enum UserStatus
    {
        Active = 0 ,
        Inactive = 1 ,
    }
}
