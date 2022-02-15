using ApiProject_Nurlan.Apps.AdminApi.DTOs;
using ApiProject_Nurlan.Apps.AdminApi.DTOs.BookDtos;
using ApiProject_Nurlan.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.AdminApi.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Genre,GenreInBookGetDto>();
            CreateMap<Author, AuthorInBookGetDto>();
            CreateMap<Book, BookGetDto>().ForMember(dest => dest.Profit, map => map.MapFrom(src => src.SalePrice - src.CostPrice));
        }
    }
}
