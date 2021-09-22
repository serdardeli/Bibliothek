using Bibliothek.Model.Entity;
using Bibliothek.Service.Option;
using Bibliothek.UI.Areas.Admin.Models.DTO;
using Bibliothek.UI.Attributes;
using Bibliothek.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibliothek.UI.Areas.Admin.Controllers
{
    public class CMSController : Controller
    {
        SliderService _sliderService;
        MenuService _menuService;
        CategoryService _categoryService;

        public CMSController()
        {
            _sliderService = new SliderService();
            _menuService = new MenuService();
            _categoryService = new CategoryService();
        }

        [CustomAuthorize(Role.Admin)]
        public ActionResult SliderList()
        {
            List<Slider> model = _sliderService.GetActive();
            return View(model);
        }

        public ActionResult SliderAdd()
        {
            List<Category> model = _categoryService.GetActive();
            return View(model);
        }

        [HttpPost]
        public ActionResult SliderAdd(Slider data, HttpPostedFileBase Image)
        {
            data.SliderPath = ImageUploader.UploadSingleImage("/Sliders/", Image);

            if (data.SliderPath == "0" || data.SliderPath == "1" || data.SliderPath == "2")
                data.SliderPath = "/Content/Images/Web/Home/product.jpg";            

                _sliderService.Add(data);
            


            return Redirect("/Admin/CMS/SliderList");
        }

        public ActionResult SliderUpdate(Guid id)
        {
            Slider slider = _sliderService.GetByID(id);
            SliderDTO model = new SliderDTO();
            model.ID = slider.ID;
            model.Queue = slider.Queue;
            return View(model);
        }

        [HttpPost]
        public ActionResult SliderUpdate(SliderDTO data)
        {
            Slider slider = _sliderService.GetByID(data.ID);
            slider.Queue = data.Queue;
            _sliderService.Update(slider);
            return Redirect("/Admin/CMS/SliderList");
        }



        public RedirectResult SliderDelete(Guid id)
        {
            _sliderService.Remove(id);
            return Redirect("/Admin/CMS/SliderList");
        }

        public ActionResult MenuList()
        {
            List<Menu> model = _menuService.GetActive();
            return View(model);
        }

        public ActionResult MenuAdd()
        {
            List<Menu> model = _menuService.GetActive();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuAdd(Menu data)
        {
            var model = _menuService.Any(x => x.MenuName == data.MenuName);
            if (model == false)
            {
                if (data.MenuName != null)
                {
                    data.ParentMenuID = data.ID;
                    _menuService.Add(data);
                }
            }

            return Redirect("/Admin/CMS/MenuList");
        }

        public ActionResult MenuUpdate(Guid id)
        {
            Menu menu = _menuService.GetByID(id);
            MenuDTO model = new MenuDTO();
            model.ID = menu.ID;
            model.ParentMenuID = menu.ParentMenuID;
            model.MenuName = menu.MenuName;
            model.MenuContent = menu.MenuContent;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuUpdate(MenuDTO data)
        {
            Menu menu = _menuService.GetByID(data.ID);
            menu.ParentMenuID = data.ParentMenuID;
            menu.MenuName = data.MenuName;
            menu.MenuContent = data.MenuContent;
            _menuService.Update(menu);
            return Redirect("/Admin/CMS/MenuList");
        }

        public RedirectResult MenuDelete(Guid id)
        {
            _menuService.Remove(id);
            return Redirect("/Admin/CMS/MenuList");
        }
    }
}