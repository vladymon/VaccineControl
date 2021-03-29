using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VC.Common.Entities;
using VC.Web.Data;
using VC.Web.Data.Entities;
using VC.Web.Helpers;
using VC.Web.Models;

namespace VC.Web.Controllers
{
    [Authorize(Roles = "Admin, User, Attention")]
    public class VaccineRequestsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly ICalendarHelper _calendarHelper;
        public VaccineRequestsController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, ICalendarHelper calendarHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _calendarHelper = calendarHelper;
        }

        // GET: VaccineRequests
        public async Task<IActionResult> Index()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            return View(await _context.VaccineRequests.Include(v=>v.User).ToListAsync());
        }

        // GET: VaccineRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccineRequest = await _context.VaccineRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccineRequest == null)
            {
                return NotFound();
            }

            return View(vaccineRequest);
        }

        // GET: VaccineRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccineRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date")] VaccineRequest vaccineRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccineRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaccineRequest);
        }

        // GET: VaccineRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccineRequest = await _context.VaccineRequests.FindAsync(id);
            if (vaccineRequest == null)
            {
                return NotFound();
            }
            return View(vaccineRequest);
        }

        // POST: VaccineRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date")] VaccineRequest vaccineRequest)
        {
            if (id != vaccineRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccineRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineRequestExists(vaccineRequest.Id))
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
            return View(vaccineRequest);
        }

        // GET: VaccineRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccineRequest = await _context.VaccineRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccineRequest == null)
            {
                return NotFound();
            }

            return View(vaccineRequest);
        }

        // POST: VaccineRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaccineRequest = await _context.VaccineRequests.FindAsync(id);
            _context.VaccineRequests.Remove(vaccineRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineRequestExists(int id)
        {
            return _context.VaccineRequests.Any(e => e.Id == id);
        }

        //public async Task<IActionResult> ListClinics()
        //{
        //    var countries = await _context.Countries.Include(c => c.Departments).ThenInclude(d => d.Provinces).ThenInclude(s=>s.Districts).ThenInclude(l=>l.Clinics).ToListAsync();
        //    return View(countries);
        //}

        public async Task<IActionResult> ListClinics()
        {
            Country country = await _context.Countries.Where(c => c.Name == "Perú").FirstOrDefaultAsync();
            ListClinicViewModel model = new ListClinicViewModel
            {
                Departments = _combosHelper.GetComboDepartments(country.Id),
                Provinces = _combosHelper.GetComboProvinces(0),
                Districts = _combosHelper.GetComboDistricts(0),
                Clinics = null
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListClinics(ListClinicViewModel model)
        {
            Country country = await _context.Countries.Where(c => c.Name == "Perú").FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                var clinics = await _context.Clinic.Where(c => c.DistrictId == model.DistrictId).ToListAsync();
                model.Clinics = clinics;
                model.Departments = _combosHelper.GetComboDepartments(country.Id);
                model.Provinces = _combosHelper.GetComboProvinces(model.DepartmentId);
                model.Districts = _combosHelper.GetComboDistricts(model.ProvinceId);
            };
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

        public JsonResult GetClinics(int districtId)
        {
            District district = _context.Districts
                .Include(c => c.Clinics)
                .FirstOrDefault(c => c.Id == districtId);
            if (district == null)
            {
                return null;
            }

            return Json(district.Clinics.OrderBy(d => d.Name));
        }

        public async Task<IActionResult> ListCalendar(int clinicId)
        {
            var date = DateTime.Now;
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            
            var calendars = await _context.Calendars.Where(c => c.Year == date.Year && c.Month == date.Month).OrderBy(c => c.Day).ToListAsync();
            var calendar = await _context.Calendars.Where(c => c.Year == date.Year && c.Month == date.Month && c.Day == 1).FirstOrDefaultAsync();
            var _calendar = await _context.Calendars.Where(c => c.Year == date.Year && c.Month == date.Month && c.Day == daysInMonth).FirstOrDefaultAsync();
            var position = _calendarHelper.GetPosition(calendar.WeekName);
            var _position = _calendarHelper.GetPosition(_calendar.WeekName);
            ViewData["ClinicId"] = clinicId;
            ViewData["MonthName"] = calendar.MonthName;
            ViewData["Year"] = date.Year.ToString();
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            Clinic clinic = await _context.Clinic.Where(c => c.Id == clinicId).FirstOrDefaultAsync();
            List<AttentionPerDayCalendar> attentionPerDayCalendars = await _context.AttentionPerDayCalendars.FromSql("exec sp_GetCalendars @p0, @p1, @p2, @p3", clinic.Id, date.Year, date.Month, clinic.AttentionPerDay).ToListAsync();
            List<Calendar> _calendars = new List<Calendar>();
            for (int i = 0; i < position - 1; i++)
            {
                _calendars.Add(new Calendar { Day = 0, IsAviable = false });
            }
            foreach (var c in calendars)
            {
                c.Position = position;
                if (c.Day < date.Day)
                    c.IsAviable = false;
                else
                    c.IsAviable = true;
                _calendars.Add(c);
                position += 1;
            }
            for (int i = 0; i < 7-_position; i++)
            {
                _calendars.Add(new Calendar { Day = 0, IsAviable = false });
            }
            foreach (var c in calendars)
            {
                if (attentionPerDayCalendars.Any(a => a.CalendarId == c.Id) == true)
                    c.IsAviable = false;
            }
            return View(_calendars);
        }

        public async Task<IActionResult> AddVaccineRequest(int clinicId, int year, int month, int day)
        {
            DateTime calendarId = new DateTime(year, month, day);
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            VaccineRequest vaccineRequest = await _context.VaccineRequests.Include(v => v.User).Where(v => v.User.Id == user.Id).Include(v => v.Vaccine).Include(v => v.VaccineRequestDetails).FirstOrDefaultAsync();
            Vaccine vaccine = await _context.Vaccines.FirstOrDefaultAsync();
            Clinic clinic = await _context.Clinic.Where(c => c.Id == clinicId).FirstOrDefaultAsync();
            RequestStatus requestStatus = await _context.RequestStatuses.Where(r => r.Id == 2).FirstOrDefaultAsync();
            Calendar calendar = await _context.Calendars.Where(c => c.Id == calendarId).FirstOrDefaultAsync();
            VaccineRequestDetail vaccineRequestDetail = new VaccineRequestDetail
            {
                Clinic = clinic,
                RequestStatus = requestStatus,
                Calendar = calendar
            };
            if (vaccineRequest == null)
            {
                List<VaccineRequestDetail> vaccineRequestDetails = new List<VaccineRequestDetail>();
                vaccineRequestDetails.Add(vaccineRequestDetail);
                vaccineRequest = new VaccineRequest { 
                    Date = DateTime.Now,
                    User = user,
                    Vaccine = vaccine,
                    VaccineRequestDetails = vaccineRequestDetails,
                };
                _context.Add(vaccineRequest);
                await _context.SaveChangesAsync();
            }
            else
            {
                vaccineRequest.VaccineRequestDetails.Add(vaccineRequestDetail);
                _context.Update(vaccineRequest);
                await _context.SaveChangesAsync();
            }
            return View(vaccineRequestDetail);
        }
        public IActionResult ChangeVaccineRequest()
        {
            FindUserViewModel findUserViewModel = new FindUserViewModel
            {
                User = null,
            };
            return View(findUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeVaccineRequest(string document)
        {
            User user = await _userHelper.GetUserAsyncxDocument(document);
            if (user == null)
            {
                return View(null);
            }
            FindUserViewModel findUserViewModel = new FindUserViewModel
            {
                User = user,
                FindUserDetailViewModels = null,
            };
            List<FindUserDetailViewModel> findUserDetailViewModels = await _context.FindUserDetailViewModels.FromSql("exec sp_GetVaccineRequest @p0", document).ToListAsync();
            if (findUserDetailViewModels.Count!=0)
            {
                findUserViewModel.FindUserDetailViewModels = findUserDetailViewModels;
            }
            return View(findUserViewModel);
        }

        public async Task<IActionResult> ChangeStatus(int id, string document)
        {
            ViewData["ClinicId"] = document;
            var vaccineRequestDetail = await _context.vaccineRequestDetails.Include(v => v.RequestStatus).Where(v => v.Id == id).FirstOrDefaultAsync();
            var requestStatus = await _context.RequestStatuses.FindAsync(1);
            vaccineRequestDetail.RequestStatus = requestStatus;            
            _context.Update(vaccineRequestDetail);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> FindVaccine()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return View(null);
            }
            FindUserViewModel findUserViewModel = new FindUserViewModel
            {
                User = user,
                FindUserDetailViewModels = null,
            };
            List<FindUserDetailViewModel> findUserDetailViewModels = await _context.FindUserDetailViewModels.FromSql("exec sp_GetVaccineRequest @p0", user.Document).ToListAsync();
            if (findUserDetailViewModels.Count != 0)
            {
                findUserViewModel.FindUserDetailViewModels = findUserDetailViewModels;
            }
            return View(findUserViewModel);
        }
    }
}
