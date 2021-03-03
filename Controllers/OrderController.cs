using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCoreProject_Iqbal.Data;
using MvcCoreProject_Iqbal.Models;
using MvcCoreProject_Iqbal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MvcCoreProject_Iqbal.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            AppResult result = null;
            IEnumerable<OrderMaster> lst = null;
            try
            {
                lst = await _context.OrderMasters.ToListAsync();
            }
            catch (Exception ex)
            {
                result = new AppResult { ResultType = ResultType.Failed, Message = "Exception occur with the system. please contact to vendor." };
                return Json(result);
            }
            return PartialView(lst);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AppResult result = new AppResult();
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                result = new AppResult { ResultType = ResultType.Failed, Message = "Exception occur with the system. please contact to vendor." };
                return Json(result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderMastViewModel data)
        {
            AppResult result = new AppResult();
            try
            {
                if (ModelState.IsValid)
                {
                    OrderMaster model = new OrderMaster
                    {
                        CustomerName = data.CustomerName,
                        OrderDate = data.OrderDate,
                        OrderDetails = data.OrderDetlViewModel.Select(a => new OrderDetails
                        {
                            ProductName = a.ProductName,
                            Qty = a.Qty,
                            Rate = a.Rate
                        }).ToList()
                    };
                    _context.Add(model);
                    await _context.SaveChangesAsync();

                    result = new AppResult { ResultType = ResultType.Success, Message = "Successfully Added!" };

                    //return Json(result);
                    return RedirectToAction("Index");

                }
                else
                {
                    result = new AppResult { ResultType = ResultType.Failed, Message = string.Join(";", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)) };
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                result = new AppResult { ResultType = ResultType.Failed, Message = "Exception occur with the system. please contact to vendor." };
                return Json(result);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            AppResult result = new AppResult();
            OrderMastViewModel modelvm = null;
            try
            {
                OrderMaster model = await _context.OrderMasters.Where(a => a.Id == id).Include(a => a.OrderDetails).FirstOrDefaultAsync();
                modelvm = new OrderMastViewModel
                {
                    Id = model.Id,
                    CustomerName = model.CustomerName,
                    OrderDate = model.OrderDate,
                    OrderDetlViewModel = model.OrderDetails.Select(a => new OrderDetlViewModel
                    {
                        Id = a.Id,
                        MastId = a.MastId,
                        ProductName = a.ProductName,
                        Qty = a.Qty,
                        Rate = a.Rate
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                result = new AppResult { ResultType = ResultType.Failed, Message = "Exception occur with the system. please contact to vendor." };
                return Json(result);
            }
            return PartialView("Details", modelvm);
        }


        public IActionResult Delete(int id)
        {
            OrderMaster order = _context.OrderMasters.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.OrderMasters.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSingle(int id)
        {
            OrderDetails order = _context.OrderDetails.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.OrderDetails.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AppResult result = new AppResult();
            try
            {
                OrderMaster model = await _context.OrderMasters.Where(a => a.Id == id).Include(a => a.OrderDetails).FirstOrDefaultAsync();
                OrderMastViewModel modelvm = new OrderMastViewModel
                {
                    Id = model.Id,
                    CustomerName = model.CustomerName,
                    OrderDate = model.OrderDate,
                    OrderDetlViewModel = model.OrderDetails.Select(a => new OrderDetlViewModel
                    {
                        Id = a.Id,
                        MastId = a.MastId,
                        ProductName = a.ProductName,
                        Qty = a.Qty,
                        Rate = a.Rate
                    }).ToList()
                };
                return PartialView(modelvm);
            }
            catch (Exception ex)
            {
                result = new AppResult { ResultType = ResultType.Failed, Message = "Exception occur with the system. please contact to vendor." };
                return Json(result);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderMastViewModel data)
        {
            AppResult result = new AppResult();
            try
            {
                if (ModelState.IsValid)
                {
                    OrderMaster model = new OrderMaster
                    {
                        Id = data.Id,
                        CustomerName = data.CustomerName,
                        OrderDate = data.OrderDate,
                        OrderDetails = data.OrderDetlViewModel.Where(a => a.Flag == Flag.New).Select(a => new OrderDetails
                        {
                            Id = a.Id,
                            MastId = a.MastId,
                            ProductName = a.ProductName,
                            Qty = a.Qty,
                            Rate = a.Rate
                        }).ToList()
                    };
                    List<OrderDetails> detl = data.OrderDetlViewModel.Where(a => a.Flag == Flag.Deleted).Select(c => new OrderDetails
                    {
                        Id = c.Id,
                        MastId = c.MastId,
                        ProductName = c.ProductName,
                        Qty = c.Qty,
                        Rate = c.Rate
                    }).ToList();
                    _context.RemoveRange(detl);
                    await _context.SaveChangesAsync();
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    result = new AppResult { ResultType = ResultType.Success, Message = "Successfully Updated !!" };
                    //return Json(result);
                    return RedirectToAction("Index");
                }
                else
                {
                    result = new AppResult { ResultType = ResultType.Failed, Message = string.Join(";", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)) };
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                result = new AppResult { ResultType = ResultType.Failed, Message = "Exception occur with the system. please contact to vendor." };
                return Json(result);
            }
        }

    }
}