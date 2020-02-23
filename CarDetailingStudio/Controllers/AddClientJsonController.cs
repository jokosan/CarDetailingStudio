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
        private IClientsOfCarWashServices _clientsOfCarWash;

        public AddClientJsonController(ICarMarkServices carMark, ICarModelServices carModel, IClientsOfCarWashServices clientsOfCarWash)
        {
            _carMark = carMark; 
            _carModel = carModel;
            _clientsOfCarWash = clientsOfCarWash;
        }

        // Autocomplete Textbox - NumberCar
        public JsonResult GetName(string searchName)
        {
            List<ClientsOfCarWashView> washWorkersViews = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(_clientsOfCarWash.GetAll()).Select(x => new ClientsOfCarWashView
            {
                id = x.id,
                NumberCar = x.NumberCar
            }).ToList();

            return new JsonResult { Data = washWorkersViews, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // MarCar
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

        // Model
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