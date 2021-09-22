using Bibliothek.UI.Areas.Web.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bibliothek.UI.Areas.Web.Models
{
    public class FavoriteProductCart
    {
        private Dictionary<Guid, FavoriteProductVM> _favoriteCart = null;
        public FavoriteProductCart()
        {
            _favoriteCart = new Dictionary<Guid, FavoriteProductVM>();
        }

        public List<FavoriteProductVM> FavoriteProductList
        {
            get
            {
                return _favoriteCart.Values.ToList();
            }
        }

        public void AddFavorite(FavoriteProductVM item)
        {
            
                if (!_favoriteCart.ContainsKey(item.ID))
                {
                _favoriteCart.Add(item.ID, item);
                }
        }

        public void RemoveFavorite(Guid id)
        {
            _favoriteCart.Remove(id);
        }
    }
}