using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentServices _departmentServices;
        public SellersController(SellerService sellerService, DepartmentServices departmentServices)
        {
            _sellerService = sellerService;
            _departmentServices = departmentServices;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }


        public IActionResult Create()
        {
            var department = _departmentServices.FindAll();
            var viewModel = new SellerFormViewModels { Departments = department };
            return View(viewModel);
        }




        //Anotatition
        [HttpPost]
        //prevenção contra CRF
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {

                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentServices.FindAll();
            SellerFormViewModels viewModels = new SellerFormViewModels { Seller = obj, Departments = departments };
            return View(viewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, Seller seller)
        {

            if (id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();

            }
            catch (DbConcurrencyException)
            {

                return BadRequest();
            }



        }

    }
}
