using AutoMapper;
using CompanyMVC.ViewModels;
using FinalDAL.Models;

namespace CompanyMVC.MappingProfile
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
