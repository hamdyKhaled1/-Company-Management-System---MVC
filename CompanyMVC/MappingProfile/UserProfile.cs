using AutoMapper;
using CompanyMVC.ViewModels;
using FinalDAL.Models;

namespace CompanyMVC.MappingProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();
        }
    }
}
