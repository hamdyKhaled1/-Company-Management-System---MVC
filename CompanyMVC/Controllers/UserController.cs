using AutoMapper;
using CompanyMVC.Helpers;
using CompanyMVC.ViewModels;
using FinalDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
	public class UserController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var user =await _userManager.Users.Select(U => new UserViewModel()
                {
                    Id= U.Id,
                    FName=U.FName,
                    LName=U.LName,
                    Email=U.Email,
                    PhoneNumber=U.PhoneNumber,
                    Roles=_userManager.GetRolesAsync(U).Result
                }).ToListAsync();
                return View(user);
            }
            else
            {
                var user=await _userManager.FindByNameAsync(email);
            
            var mappedUser = new UserViewModel()
            {
				Id = user.Id,
				FName = user.FName,
				LName = user.LName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Roles = _userManager.GetRolesAsync(user).Result
			};
                return View(new List<UserViewModel>() { mappedUser });
			}
		}

        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
            return View(mappedUser);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id is null) return BadRequest();

            var user = _userManager.FindByIdAsync(id);
            if (user is null) 
                return NotFound();

            // التحويل الفعلي هنا
            var uservm = _mapper.Map<UserViewModel>(user);

            return View(uservm);
            //if (id is null)
            //    return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null)
            //    return NotFound();
            //return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] string id, UserViewModel UpdaetUser)
        {
            if (id != UpdaetUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappeduser = _mapper.Map<UserViewModel, ApplicationUser>(UpdaetUser);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(UpdaetUser);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id is null)
                return BadRequest();

            var user = _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();

            // التحويل الفعلي هنا
            var userVM = _mapper.Map<UserViewModel>(user);

            return View(userVM);
            //if (id is null)
            //    return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null)
            //    return NotFound();
            //return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel deleteuser)
        {

            try
            {
                var mappuser = _mapper.Map<UserViewModel, ApplicationUser>(deleteuser);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }


            return View(deleteuser);
        }
    }
}
