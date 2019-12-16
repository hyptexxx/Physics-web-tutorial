using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FizikaWeb.Models;
using System.Web.SessionState;
using System.Web.Services;
using FizikaWeb.Implementation.DAO;

namespace FizikaWeb.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Экземпляр  
        /// <see cref="FizikaWeb.Implementation.DAO.ArticleDAOImpl"/> 
        /// реализующий интерфейс
        /// <see cref="FizikaWeb.Interfaces.DAO.ArticleDAO.IArticleDAO"/>
        /// </summary>
        private ArticleDAOImpl articleDAOimpl = new ArticleDAOImpl();

        /// <summary>
        ///    Если не обнаружено методов, способных обратотать текущий запрос- возвращает представление с ошибкой 404.
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorNotFoud() {
            Response.StatusCode = 404;
            return View();
        }



        /// <summary>
        ///    Возвращает представление Error forbidden.
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorForbidden() { return View(); }


    
        /// <summary>
        ///    Возвращает начальное, главное представление со списком записей.
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public ActionResult Index(Menu menu, int count = 0) {
            List <string> menuNames = articleDAOimpl.getMenuNames();
            return View(menuNames);
        }


    
        /// <summary>
        ///    Возвращает список записей по Ajax запросу.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SectionSelector(Book book) {
            if (Request.IsAjaxRequest()) {
                return Json(Server.HtmlDecode(articleDAOimpl.getContentWhereName(book.name)));
            }
            return Json(null);
        }


    
        /// <summary>
        ///    Возвращает представление добавления новой записи в БД.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRecordViewShow() { return View(); }


  
        /// <summary>
        ///    Добавляет новую запись в БД по Ajax запросу.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddRecordDB(Book book) {
            string response_mce_content = Request.Form["content"];
            string response_mce_name = Request.Form["book_name"];
            articleDAOimpl.insertArticle(Server.HtmlEncode(response_mce_content), response_mce_name);
            return Redirect("/");
        }


 
        /// <summary>
        ///    Возвращает содержание записи по Ajax запросу (для поиска).
        /// </summary>
        /// <param name="search"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult sql_search(Search search, Book book) {
            List <string> search_results = new List<string>();
            if (Request.IsAjaxRequest()) {
                search.result = Server.HtmlDecode(articleDAOimpl.getContentSearch(search.param));
                search_results.Add(search.result);
                return Json(search.result);
            }
            return View();
        }


    
        /// <summary>
        ///    Возвращает представление теста, со списком имен записей в БД (для меню).
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public ActionResult test(Menu menu) {
            List <string> names = articleDAOimpl.getMenuNames();
            return View(names);
        }


   
        /// <summary>
        ///    Возвращает представление галереи, со списком имен записей в БД (для меню).
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public ActionResult gallery(Menu menu) {
            List<string> names = articleDAOimpl.getMenuNames();
            return View(names);
        }



        /// <summary>
        ///    Возвращает представление удаления записей, со списком имен записей в БД (для меню).
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveRecord(Menu menu) {
            List<string> names = articleDAOimpl.getMenuNames();
            return View(names);
        }
        


        /// <summary>
        ///    Удаляет запись по Ajax зыпросу из базы данных.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult remove_record(Book book) {
            articleDAOimpl.deleteRecord(book.name);
            return Json(book.name + " has removed");
        }



        /// <summary>
        ///    Возвращает представление администратора.
        /// </summary>
        /// <param name="adminParams"></param>
        /// <returns></returns>
        public ActionResult adminPanel(Admin adminParams) { return View(); }



        /// <summary>
        ///    Осуществляет проверку вводимых данных при авторизации и возвращает нужное представление.
        /// </summary>
        /// <param name="adminParams"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult admin_panel_loginned(Admin adminParams) {
            string Login = Request.Form["Login"];
            string Password = Request.Form["Password"];

            if (articleDAOimpl.checkAdmin(Login, Password)) {
                return View("admin_panel_loggined");
            }
            else {
                ViewBag.Message = "Повторите попытку еще раз";
                return View("adminPanel", adminParams);
            }
        }
    }
}