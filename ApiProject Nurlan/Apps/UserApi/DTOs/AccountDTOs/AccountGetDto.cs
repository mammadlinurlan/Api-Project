using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.UserApi.DTOs.AccountDTOs
{
    public class AccountGetDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
