using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizikaWeb.Interfaces.DAO.ArticleDAO
{
    interface IArticleDAO
    {
        //
        // Сводка:
        //     Добавляет новую запись в базу данных.
        //
        // Параметры:
        //     string mce_content - содержание записи из редактора TiniMCE.
        //     string mce_name - заголовок добавляемой записи.
        void insertArticle(string mce_content, string mce_name);

        //
        // Сводка:
        //     Удаляет запись из базы данных.
        // Параметры:
        //     string book_name - название записи (книги).
        void deleteRecord(string book_name);

        //
        // Сводка:
        //     Возвращает содержание записи: по ее имени.
        // Параметры:
        //     string name - название записи (книги).
        string getContentWhereName(string name);

        //
        // Сводка:
        //     Возвращает содержание записи: по параметру поиска.
        // Параметры:
        //     string search_param - параметр содержащий поисковую фразу.
        string getContentSearch(string search_param);

        //
        // Сводка:
        //     Выполняет запрос в базу данных и проверяет наличие в ней полей сущности администратора.
        // Параметры:
        //     string admin_login - логин администратора
        //     string admin_password - пароль администратора
        bool checkAdmin(string admin_login, string admin_password);

        //
        // Сводка:
        //     Возвращает список имен записей в Базе данных
        // Параметры:
        //     string admin_login - логин администратора
        //     string admin_password - пароль администратора
        List<string> getMenuNames();
    }
}
