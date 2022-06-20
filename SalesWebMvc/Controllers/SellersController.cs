using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

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
            if (!ModelState.IsValid)
            {
                var departments = _departmentServices.FindAll();
                var viewModel = new SellerFormViewModels { Seller = seller, Departments = departments };

                return View(viewModel);
            }

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not provided!" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found!" });
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
                return RedirectToAction(nameof(Error), new { message = "id not provided!" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {

                return RedirectToAction(nameof(Error), new { message = "id not found!" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not provided!" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found!" });
            }

            List<Department> departments = _departmentServices.FindAll();
            SellerFormViewModels viewModels = new SellerFormViewModels { Seller = obj, Departments = departments };
            return View(viewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid)
            {
                var departments = _departmentServices.FindAll();
                var viewModel = new SellerFormViewModels { Seller = seller, Departments = departments };

                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "id mismatch!" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }

        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }


    }


}


