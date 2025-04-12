using AutoMapper;
using CompanyMVC.ViewModels;
using FinalDAL.Models;

namespace CompanyMVC.MappingProfile
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
