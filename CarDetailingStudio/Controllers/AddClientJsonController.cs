using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CarDetailingStudio.Models;

namespace CarDetailingStudio.Controllers
{
    public class AddClientJsonController : Controller
    {
        private ICarModelServices _carModel;
        private ICarMarkServices _carMark;

        public AddClientJsonController(ICarMarkServices carMark, ICarModelServices carModel)
        {
            _carMark = carMark;
            _carModel = carModel;
        }

        public JsonResult GetCountryList(string searchTerm)
        {
            var CounrtyList = Mapper.Map<IEnumerable<CarMarkView>>(_carMark.Get()).ToList();
            
            if (searchTerm != null)
            {
                CounrtyList = Mapper.Map<IEnumerable<CarMarkView>>(_carMark.GetWhere(searchTerm)).ToList();
            }

            var modifiedData = CounrtyList.Select(x => new
            {
                id = x.id_car_mark,
                text = x.name
            });

            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStateList(string CountryIDs)
        {
            int modelCar = Int32.Parse(CountryIDs);
            TempData["Mark"] = modelCar;

            List<CarModelView> StateList = new List<CarModelView>();

            var listDataByCountryID = Mapper.Map<IEnumerable<CarModelView>>(_carModel.GetWhere(modelCar));

            foreach (var item in listDataByCountryID)
            {
                StateList.Add(item);
            }

            return Json(StateList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(string data)
        {
            TempData["Model"] = Int32.Parse(data);
            return Json("");
        }


    }
}