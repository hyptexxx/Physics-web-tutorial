using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FizikaWeb.Models
{
    public class SQLConfig
    {
        /// <summary>
        /// При запуске приложения присваивает полю строку подключения к базе данных OLEDB.
        /// Затем хранит ее до окончания работы приложения.
        /// </summary>
        public static string SQLConnectionString { get; set; }
    }
}