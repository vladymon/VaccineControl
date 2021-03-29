using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VC.Common.Entities;
using VC.Web.Data;
using VC.Web.Helpers;
using VC.Web.Models;

namespace VC.Web.Controllers
{
    public class ClinicsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        public ClinicsController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        // GET: Clinics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clinic.ToListAsync());
        }

        // GET: Clinics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // GET: Clinics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,AttentionPerDay")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }



        // GET: Clinics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinic.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,AttentionPerDay")] Clinic clinic)
        {
            if (id != clinic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicExists(clinic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clinic = await _context.Clinic.FindAsync(id);
            _context.Clinic.Remove(clinic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicExists(int id)
        {
            return _context.Clinic.Any(e => e.Id == id);
        }

        public IActionResult Register()
        {
            Country country = _context.Countries.Where(c => c.Name == "Perú").FirstOrDefault();
            AddClinicViewModel model = new AddClinicViewModel
            {                
                Departments = _combosHelper.GetComboDepartments(country.Id),
                Provinces = _combosHelper.GetComboProvinces(0),
                Districts = _combosHelper.GetComboDistricts(0),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddClinicViewModel model)
        {
            Country country = _context.Countries.Where(c => c.Name == "Perú").FirstOrDefault();
            if (ModelState.IsValid)
            {                              
                Clinic clinic = new Clinic
                {
                    Name = model.Name,
                    Address = model.Address,
                    AttentionPerDay = model.AttentionPerDay,
                    IdDistrict = model.DistrictId,
                };
                //User user = await _userHelper.AddUserAsync(model, imageId, UserType.User);
                //if (user == null)
                //{
                //    ModelState.AddModelError(string.Empty, "This email is already used.");
                //    model.Countries = _combosHelper.GetComboCountries();
                //    model.Departments = _combosHelper.GetComboDepartments(model.CountryId);
                //    model.Cities = _combosHelper.GetComboCities(model.DepartmentId);
                //    return View(model);
                //}

                _context.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            model.Departments = _combosHelper.GetComboDepartments(country.Id);
            model.Provinces = _combosHelper.GetComboProvinces(model.DepartmentId);
            model.Districts = _combosHelper.GetComboDistricts(model.ProvinceId);
            return View(model);

        }


        public JsonResult GetDepartments(int countryId)
        {
            Country country = _context.Countries
                .Include(c => c.Departments)
                .FirstOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return null;
            }

            return Json(country.Departments.OrderBy(d => d.Name));
        }

        public JsonResult GetProvinces(int departmentId)
        {
            Department department = _context.Departments
                .Include(c => c.Provinces)
                .FirstOrDefault(c => c.Id == departmentId);
            if (department == null)
            {
                return null;
            }

            return Json(department.Provinces.OrderBy(d => d.Name));
        }

        public JsonResult GetDistricts(int provinceId)
        {
            Province province = _context.Provinces
                .Include(c => c.Districts)
                .FirstOrDefault(c => c.Id == provinceId);
            if (province == null)
            {
                return null;
            }

            return Json(province.Districts.OrderBy(d => d.Name));
        }

    }
}
