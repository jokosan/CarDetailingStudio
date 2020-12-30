using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Utilities
{
    public class CartModelBinder : IModelBinder
    {
        private const string seesionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Получить объект Cart из сеанса
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[seesionKey];
            }

            // Создать экземрляр Cart если он не обноружен в данных сеанса
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[seesionKey] = cart;
                }
            }

            // Возвратить объект Cart
            return cart;
        }
    }
}