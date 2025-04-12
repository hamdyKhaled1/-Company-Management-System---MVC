using AutoMapper;
using CompanyMVC.ViewModels;
using FinalBLL.Interfaces;
using FinalBLL.Repositories;
using FinalDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public DepartmentController(
            IUnitOfWork unitOfWork,IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var departments =await _unitOfWork.DepartmentRepository.GetAll();
                var mappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
                return View(mappedDept);
            }
            else
            {
                var deparments=_unitOfWork.DepartmentRepository.SearchDepartment(SearchValue);
                var mappedDept=_mapper.Map<IEnumerable<Department>,IEnumerable< DepartmentViewModel >> (deparments);
                return View(mappedDept);
            }
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDept=_mapper.Map<DepartmentViewModel,Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(mappedDept);
                await _unitOfWork.Complete();
                
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department =await _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null)
                return NotFound();
            var mappedDept=_mapper.Map<Department,DepartmentViewModel>(department);
            return View(mappedDept);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound();

            // التحويل الفعلي هنا
            var departmentVM = _mapper.Map<DepartmentViewModel>(department);

            return View(departmentVM);
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.Get(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDept= _mapper.Map<DepartmentViewModel,Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(mappedDept);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(departmentVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound();

            // التحويل الفعلي هنا
            var employeeVM = _mapper.Map<DepartmentViewModel>(department);

            return View(employeeVM);
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.Get(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
           
                try
                {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Delete(mappedDept);
                await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            
            return View(departmentVM);
        }
    }
}
