using AutoMapper;
using CompanyMVC.Helpers;
using CompanyMVC.ViewModels;
using FinalBLL.Interfaces;
using FinalDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
    public class EmployeeController : Controller
    {
        //private IEmployeeRepository _employeeRepository;
        //private IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }
        public async Task<IActionResult> Index(string SearchValue )
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Employees =await _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp);
            }
            else
            {
                var employees =_unitOfWork.EmployeeRepository.SearchEmployee(SearchValue);
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View (mappedEmp);
            }
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments =_unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel EmployeeVM)
        {
            if (ModelState.IsValid)
            {
                EmployeeVM.ImageName = DocumentSettings.UpLoadFile(EmployeeVM.Image, "Images");
                var mappedEmp=_mapper.Map<EmployeeViewModel,Employee>(EmployeeVM);
               await _unitOfWork.EmployeeRepository.Add(mappedEmp);
               await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(EmployeeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var Employee =await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (Employee is null)
                return NotFound();
            var mappedEmp=_mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(mappedEmp);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound();

            // التحويل الفعلي هنا
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeVM);
            //if (id is null)
            //    return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null)
            //    return NotFound();
            //return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound();

            // التحويل الفعلي هنا
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeVM);
            //if (id is null)
            //    return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null)
            //    return NotFound();
            //return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                int count=await _unitOfWork.Complete();
                if (count > 0)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }


            return View(employeeVM);
        }
    }
}
