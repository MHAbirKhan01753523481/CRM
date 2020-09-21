using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using ProjectMaking.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Helper
{
    public class GetDropdownList
    {
       internal List<SelectListItem> GetCountry()
        {
            List<SelectListItem> CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem() { Text = "Bangladesh", Value = "Bangladesh" });
            CountryList.Add(new SelectListItem() { Text = "India", Value = "India" });
            CountryList.Add(new SelectListItem() { Text = "USA", Value = "USA" });
            return CountryList;
        }
       internal List<SelectListItem> GetIconColor()
        {
            List<SelectListItem> IconColorList = new List<SelectListItem>();
            IconColorList.Add(new SelectListItem() { Text = "icon-warning", Value = "icon-warning" });
            IconColorList.Add(new SelectListItem() { Text = "icon-success", Value = "icon-success" });
            IconColorList.Add(new SelectListItem() { Text = "icon-pink", Value = "icon-pink" });
            IconColorList.Add(new SelectListItem() { Text = "icon-primary", Value = "icon-primary" });
            return IconColorList;
        }
       internal List<SelectListItem> GetIconClass()
        {

            List<SelectListItem> IconClassList = new List<SelectListItem>();
            IconClassList.Add(new SelectListItem() { Text = "mdi mdi-checkbox-marked-circle-outline", Value = "mdi mdi-checkbox-marked-circle-outline" });
            IconClassList.Add(new SelectListItem() { Text = "mdi mdi-timer-off", Value = "mdi mdi-timer-off" });
            IconClassList.Add(new SelectListItem() { Text = "mdi mdi-alert-decagram", Value = "mdi mdi-alert-decagram" });
            IconClassList.Add(new SelectListItem() { Text = "mdi mdi-clipboard-alert", Value = "mdi mdi-clipboard-alert" });
            IconClassList.Add(new SelectListItem() { Text = "mdi mdi-alert-decagram", Value = "mdi mdi-alert-decagram" });
            return IconClassList;
        }
        internal List<SelectListItem> GetGender()
        {
            List<SelectListItem> dropdownList = new List<SelectListItem>();
            dropdownList.Add(new SelectListItem() { Text = "Male", Value = "Male" });
            dropdownList.Add(new SelectListItem() { Text = "Female", Value = "Female" });
            dropdownList.Add(new SelectListItem() { Text = "Others", Value = "Others" });
            return dropdownList;
        }
        internal List<SelectListItem> GetMaritailStatus()
        {
            List<SelectListItem> getmarried = new List<SelectListItem>();
            getmarried.Add(new SelectListItem() { Text = "Married", Value = "Married" });
            getmarried.Add(new SelectListItem() { Text = "UnMarried", Value = "UnMarried" });
            return getmarried;
        }
        public List<SelectListItem> GetTotalDay()
        {
            List<SelectListItem> getDay = new List<SelectListItem>();
            getDay.Add(new SelectListItem() { Text = "SatDay", Value = "SatDay" });
            getDay.Add(new SelectListItem() { Text = "SunDay", Value = "SunDay" });
            getDay.Add(new SelectListItem() { Text = "MonDay", Value = "MonDay" });
            getDay.Add(new SelectListItem() { Text = "TuesDay", Value = "TuesDay" });
            getDay.Add(new SelectListItem() { Text = "WednesDay", Value = "WednesDay"});
            getDay.Add(new SelectListItem() { Text = "ThusDay", Value = "ThusDay" });
            getDay.Add(new SelectListItem() { Text = "FriDay", Value = "FriDay" });
            return getDay;
        }
    }
}
