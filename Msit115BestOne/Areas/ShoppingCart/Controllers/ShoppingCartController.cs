using Msit115BestOne.Areas.ShoppingCart.Models;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web.Mvc;

namespace Msit115BestOne.Areas.ShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private Repository<Cart> repository = new Repository<Cart>();

        public IEnumerable<Cart> GetCarts()
        {
            return repository.GetAll();
        }
        public Cart GetCarts(int id)
        {
            return repository.GetById(id);
        }

        public void PostCarts(Cart _cart)
        {
            repository.Create(_cart);
        }
        public void PutCarts(Cart _cart)
        {
            repository.Update(_cart);
        }
        public void DeleteShippers(int id)
        {
            repository.Delete(repository.GetById(id));
        }
    }
}
