using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC.Common.Entities;
using VC.Web.Data;

namespace VC.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;

        }
        //public IEnumerable<SelectListItem> GetComboDepartments(int countryId);
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    Department department = _context.Departments
        //        .Include(d => d.Cities)
        //        .FirstOrDefault(d => d.Id == departmentId);
        //    if (department != null)
        //    {
        //        list = department.Cities.Select(t => new SelectListItem
        //        {
        //            Text = t.Name,
        //            Value = $"{t.Id}"
        //        })
        //            .OrderBy(t => t.Text)
        //            .ToList();
        //    }

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a city...]",
        //        Value = "0"
        //    });

        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboCountries()
        //{
        //    List<SelectListItem> list = _context.Countries.Select(t => new SelectListItem
        //    {
        //        Text = t.Name,
        //        Value = $"{t.Id}"
        //    })
        //        .OrderBy(t => t.Text)
        //        .ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a country...]",
        //        Value = "0"
        //    });

        //    return list;
        //}

        public IEnumerable<SelectListItem> GetComboDepartments(int countryId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Country country = _context.Countries.Include(c => c.Departments).FirstOrDefault(c => c.Id == countryId);
            if (country != null)
            {
                list = country.Departments.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionar...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProvinces(int departmentId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Department department = _context.Departments.Include(c => c.Provinces).FirstOrDefault(d => d.Id == departmentId);
            if (department != null)
            {
                list = department.Provinces.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionar...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboDistricts(int provinceId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Province province = _context.Provinces.Include(c => c.Districts).FirstOrDefault(p => p.Id == provinceId);
            if (province != null)
            {
                list = province.Districts.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionar...]",
                Value = "0"
            });
            return list;
        }
    }
}
