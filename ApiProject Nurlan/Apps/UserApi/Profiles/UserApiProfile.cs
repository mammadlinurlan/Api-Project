using ApiProject_Nurlan.Apps.UserApi.DTOs.AccountDTOs;
using ApiProject_Nurlan.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.UserApi.Profiles
{
    public class UserApiProfile : Profile
    {
        public UserApiProfile()
        {
            CreateMap<AppUser, AccountGetDto>();
        }
    }
}
