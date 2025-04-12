using FinalDAL.Models;

using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace CompanyMVC.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "MaxLength must be 50 Chars")]
        [MinLength(5, ErrorMessage = "MaxLength must be 5 Chars")]
        public string Name { get; set; }
        [Range(20, 30, ErrorMessage = "Age Must be between 20 to 30")]
        public int? Age { get; set; }
        public string Address { get; set; }
        
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
    
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
