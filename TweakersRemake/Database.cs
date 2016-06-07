using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using ASP.NET_MVC_Application.Models;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace TweakersRemake
{
    public static class Database
    {
        private static string Connection = "Data Source=Localhost;User Id=system;Password=mmk55mmk100;";

        private static OracleConnection Conn;

       static Database()
        {
           try
           {
                Conn = new OracleConnection(Connection);
           }
           catch (OracleException Ex)
           {
               throw;
           }
        }

        public static bool Openconnecion()
        {
            if (Conn.State == ConnectionState.Open)
            {


                return true;
            }
            else
            {
                try
                {
                    Conn.Open();
                    return true;
                }
                catch (OracleException ex)
                {

                    return false;
                }
            }
        }

        public static OracleDataReader GetReader(string str)
        {
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;

                return command.ExecuteReader();
            }
            return null;
            //Dit is commentaar

        }

        public static List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();
            string str = "Select * From Product";
            OracleDataReader Data = GetReader(str);

            
            while (Data.Read())
            {
                Product Pr = new Product();
                Pr.Id = Data.GetInt32(0);
                Pr.Naam = Data.GetString(1);
                Pr.Categorie = Data.GetString(2);
                Pr.Foto_Url = Data.GetString(3);
                list.Add(Pr);
                
            }
            Conn.Close();
            return list;
            

        }

        public static List<Product_Link> GetLinks(int id)
        {
            //Op basis van de id van de producten, Alle gelinkte waardes uit de database halen
            List<Product_Link> list = new List<Product_Link>();

            string str = "Select * From Product_Link where Product_Id = @Id";
            
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("@Id", OracleDbType.Int16);
                command.Parameters["@Id"].Value = id;
              OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                    Product_Link Pr = new Product_Link();
                    Pr.Uitgever.Id = Data.GetInt32(0);
                    Pr.Prijs = Data.GetDouble(3);
                    Pr.Url = Data.GetString(4);
                    Pr.Levertijd = Data.GetString(5);
                    list.Add(Pr);

                }

            }
           
            //Dit is commentaar



          
            Conn.Close();
            return list;



        }


    }

  
}