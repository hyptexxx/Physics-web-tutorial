using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FizikaWeb.Interfaces.DAO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web.Mvc;
using FizikaWeb.Models;

namespace FizikaWeb.Implementation.DAO
{
    public class ArticleDAOImpl : Interfaces.DAO.ArticleDAO.IArticleDAO
    {                                 
        private static string sqlConnectionString = SQLConfig.SQLConnectionString;
        private OleDbConnection connection = new OleDbConnection(sqlConnectionString);
        private OleDbCommand cmd = new OleDbCommand();



        /// <summary>
        /// Выполняет запрос в базу данных и проверяет наличие в ней полей сущности администратора.
        /// </summary>
        /// <param name="admin_login">получиеа</param>
        /// <param name="admin_password">ываыва</param>
        /// <returns></returns>
        public bool checkAdmin(string admin_login, string admin_password) {
            cmd.CommandText = "SELECT Login, Password FROM Admin WHERE Login ='" + admin_login + "' AND Password ='" + admin_password + "'";
            try {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                OleDbDataReader dbReader = cmd.ExecuteReader();
                if (dbReader.HasRows) { return true; }
                else { return false; }
            }
            catch (SqlException e) { Console.WriteLine("SQL_Exeption---------> " + e.Message); }
            finally { connection.Close(); }
            return false || true;
        }



        /// <summary>
        /// Удаляет запись из базы данных.
        /// </summary>
        /// <param name="book_name"></param>
        public void deleteRecord(string book_name){
            cmd.CommandText = "DELETE FROM Books WHERE name ='" + book_name + "'";
            try {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e) {  Console.WriteLine("SQL_Exeption---------> " + e.Message); }
            finally  { connection.Close(); }
        }



        /// <summary>
        /// Возвращает содержание записи: по параметру поиска.
        /// </summary>
        /// <param name="search_param"></param>
        /// <returns></returns>
        public string getContentSearch(string search_param) {
            cmd.CommandText = "SELECT content FROM Books WHERE name like '%" + search_param + "%'";
            try {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                OleDbDataReader dbReader = cmd.ExecuteReader();
                if (dbReader.HasRows) {
                    while (dbReader.Read()) {
                        return dbReader.GetString(0);
                    }
                }
                else { return "по запросу: <h1>" + search_param + " </h1> ничего не найдено"; }
            }
            catch (SqlException e) { Console.WriteLine("SQL_Exeption---------> " + e.Message); }
            finally {connection.Close(); }
            return "";
        }



        /// <summary>
        /// Возвращает содержание записи: по ее имени.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string getContentWhereName(string name) {
            Book book = new Book();
            cmd.CommandText = "SELECT content FROM Books WHERE(name = '" + name + "')";
            try {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                OleDbDataReader dbReader = cmd.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while (dbReader.Read()) { return  dbReader.GetString(0); }
                }
            }
            catch (SqlException e) { Console.WriteLine("SQL_Exeption---------> " + e.Message); }
            finally { connection.Close(); }
            return "";
        }



        /// <summary>
        /// Возвращает список имен записей в Базе данных.
        /// </summary>
        /// <returns></returns>
        public List<string> getMenuNames() {
            List<string> names = new List<string>();
            cmd.CommandText = "SELECT name FROM Books";
            try {
                //  HomeController.SQLCountSender();
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                OleDbDataReader dbReader = cmd.ExecuteReader();
                if (dbReader.HasRows) {
                    while (dbReader.Read()) {
                        names.Add(dbReader.GetString(0));
                    }
                }
            }
            catch (SqlException e) {
                Console.WriteLine("SQL_Exeption---------> " + e.Message);
            }
            finally {
                connection.Close();                
            }
            return names;
        }



        /// <summary>
        /// Добавляет новую запись в базу данных.
        /// </summary>
        /// <param name="mce_content"></param>
        /// <param name="mce_name"></param>
        public void insertArticle(string mce_content, string mce_name) {
            try {
                cmd.CommandText = "INSERT INTO Books (content, name) VALUES ('" + mce_content + "', '" + mce_name + "')";
            }
            catch (NotImplementedException e) { Console.WriteLine("NotImplementedException was called " + e.Message); }

            try {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e) { Console.WriteLine("SQL_Exeption---------> " + e.Message); }
            connection.Close();
        }
    }
}