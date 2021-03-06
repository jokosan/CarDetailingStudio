﻿using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public async Task<JsonResult> GetName(string searchInput)
        {
            var washWorkersViews = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(await _clientsOfCarWash.GetAll(searchInput));

            List<ClientsOfCarWashView> washWorkersViewsList = washWorkersViews.Select(x => new ClientsOfCarWashView
            {
                id = x.id,
                NumberCar = x.NumberCar
            }).ToList();

            return new JsonResult { Data = washWorkersViewsList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // MarCar
        public async Task<JsonResult> GetCountryList(string searchTerm)
        {
            var CounrtyList = Mapper.Map<IEnumerable<CarMarkView>>(await _carMark.Get()).ToList();

            if (searchTerm != null)
            {
                CounrtyList = Mapper.Map<IEnumerable<CarMarkView>>(await _carMark.GetWhere(searchTerm)).ToList();
            }

            var modifiedData = CounrtyList.Select(x => new
            {
                id = x.id_car_mark,
                text = x.name
            });

            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }

        // Model
        public async Task<JsonResult> GetStateList(string CountryIDs)
        {
            int modelCar = Int32.Parse(CountryIDs);
            TempData["Mark"] = modelCar;

            List<CarModelView> StateList = new List<CarModelView>();

            var listDataByCountryID = Mapper.Map<IEnumerable<CarModelView>>(await _carModel.GetWhere(modelCar));

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