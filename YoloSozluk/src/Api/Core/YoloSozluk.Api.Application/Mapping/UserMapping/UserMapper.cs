using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Api.Application.Mapping.UserMapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserLoginViewModel>().ReverseMap();
        }
    }
}
