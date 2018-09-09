﻿using System;

namespace MailLibrary.BaseClasses
{
    public class BaseSqlServerConnections : BaseExceptionProperties
    {
        /// <summary> 
        /// This points to your database server 
        /// </summary> 
        protected string DatabaseServer = "KARENS-PC"; // Change to your database server name or for SQL-Express .\SQLEXPRESS
        /// <summary> 
        /// Name of database containing required tables 
        /// </summary> 
        protected string DefaultCatalog = "";
        public string ConnectionString => $"Data Source={DatabaseServer};Initial Catalog={DefaultCatalog};Integrated Security=True";

        /// <summary> 
        /// Determines if running on Karen Payne's computer 
        /// </summary> 
        public bool IsKarenMachine => Environment.UserName == "Karens";
        /// <summary> 
        /// Determine if server name has been set from the default on Karen Payne's computer 
        /// </summary> 
        public bool IsKarensDatabaseServer => DatabaseServer == "KARENS-PC";
    }
}
