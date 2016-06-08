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

            string str = "Select Product_Id, Prijs,Url,Levertijd,Naam From Product_Link P, Uitgever U where Product_Id = :Id And U.Id = P.Product_Id" ;
            
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = id;
              OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                    Product_Link Pr = new Product_Link();
                    Pr.Uitgever = new Uitgever();
                    Pr.Uitgever.Id = Data.GetInt32(0);
                    Pr.Prijs = Data.GetFloat(1);
                    Pr.Url = Data.GetString(2);
                    Pr.Levertijd = Data.GetInt32(3);
                    Pr.Uitgever.Naam = Data.GetString(4);
                    list.Add(Pr);

                }

            }
           
            //Dit is commentaar



          
            Conn.Close();
            return list;



        }

        public static List<Preview> GetPreviews(string c, int id)
        {
            List<Preview> list = new List<Preview>();
            string str = "Select a.id, p.Pluspunten,P.Minpunten,P.Prijs,P.Text,a.naam, c.* from Acca a join Product_Review P on a.id = P.Acca_Id join "+"Review_" + c + "Crit"+ " C on c.Review_id = p.id where P.Product_ID = :Id";

            if (Openconnecion()&& id != 0)
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = id;

               // command.Parameters.Add("Crit", OracleDbType.Varchar2);
              //  command.Parameters["Crit"].Value = "Review_"+c+"Crit";

                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                   Preview Pr = new Preview();
                    Pr.Acc = new Account();
                    Pr.Acc.Id = Data.GetInt32(0);
                    Pr.Pluspunten = Data.GetString(1);
                    Pr.Minpunten = Data.GetString(2);
                    Pr.Prijs = Data.GetInt32(3);
                    Pr.Text = Data.GetString(4);
                    Pr.Acc.Naam = Data.GetString(5);
                    Pr.Review = new string[Data.FieldCount-5,2];
                    
                    //Omdat ik niet zeker weet hoeveel data er in zit 
                    for (int i = 8; i < Data.FieldCount; i++)
                    {
                        Pr.Review[i - 8, 0] = Data.GetName(i);
                        int k = Data.GetInt32(i);
                        Pr.Review[i-8,1] =  k.ToString();
                    }
                    
                    


                    list.Add(Pr);

                }

            }
            return list;
        }

        public static Account GetAccount(int id)
        {
            Account A = new Account();

            string str = "Select * from Acca where id = :Id";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = id;
                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                    A.Id = Data.GetInt32(0);
                    A.Naam = Data.GetString(1);
                    A.GeboorteDatum = Data.GetDateTime(2);
                    A.Geslacht = Data.GetString(3);
                    A.Woonplaats = Data.GetString(4);
                    A.Opleiding = Data.GetString(5);
                    A.ProfielNaam = Data.GetString(6);
                    A.Geregistreerd = Data.GetDateTime(7);
                    

                }

            }
            return A;

        }



    }

  
}