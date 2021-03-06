﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using ASP.NET_MVC_Application.Models;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using TweakersRemake.Models;

namespace TweakersRemake
{
    public static class Database
    {
        private static string Connection = "Data Source=Localhost;User id=system;Password=mmk55mmk100;";
        private static OracleConnection Conn;

        /// <summary>
        /// Stelt de connectie in
        /// </summary>
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
        /// <summary>
        /// Checked of de database open is, zo niet dan wordt die geopend
        /// </summary>
        /// <returns>succes</returns>
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
        /// <summary>
        /// Combineerd de connectie en de string tot een reader
        /// </summary>
        /// <param name="str">Sql string</param>
        /// <returns></returns>
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
#region Products
        /// <summary>
        /// Alle producten in het systeem
        /// </summary>
        /// <returns>Lijst van producten</returns>
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
        /// <summary>
        /// Geeft alle links tussen een bepaald product en zijn uitgevers
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns></returns>
        public static List<Product_Link> GetLinks(int id)
        {
            //Op basis van de id van de producten, Alle gelinkte waardes uit de database halen
            List<Product_Link> list = new List<Product_Link>();

            string str =
                "Select Product_Id, Prijs,Url,Levertijd,Naam From Product_Link P, Uitgever U where Product_Id = :Id And U.Id = P.Product_Id";

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
        /// <summary>
        /// Geeft alle Product Reviews van een bepaalde product
        /// </summary>
        /// <param name="c">Product Catergorie</param>
        /// <param name="id">Product id</param>
        /// <returns></returns>
        public static List<Preview> GetPreviews(string c, int id)
        {
            List<Preview> list = new List<Preview>();
            string str =
                "Select a.id, p.Pluspunten,P.Minpunten,P.Prijs,P.Text,a.naam, c.* from Acca a join Product_Review P on a.id = P.Acca_Id join " +
                "Review_" + c + "Crit" + " C on c.Review_id = p.id where P.Product_ID = :Id";
            //Hier zet ik de table naam in de string zodat deze variable is
            if (Openconnecion() && id != 0)
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
                    Pr.Review = new string[Data.FieldCount - 5, 2];

                    //Omdat ik niet zeker weet hoeveel data er in zit 
                    for (int i = 8; i < Data.FieldCount; i++)
                    {
                        Pr.Review[i - 8, 0] = Data.GetName(i);
                        int k = Data.GetInt32(i);
                        Pr.Review[i - 8, 1] = k.ToString();
                    }




                    list.Add(Pr);

                }

            }
            return list;
        }
        /// <summary>
        /// Krijg de producten uit een bepaalde wenslijst
        /// </summary>
        /// <param name="Wenslijst"></param>
        /// <param name="Profielnaam"></param>
        /// <returns></returns>
        public static List<Product> GetProductsWenslijst(string Wenslijst, string Profielnaam)
        {
            List<Product> list = new List<Product>();
            string str = "Select p.* from Acca a join wenslijst w on a.id = w.Acca_id join Wenslijst_product wp on w.ID = wp.Wenslijst_ID join Product p on wp.Product_id = p.id where w.naam = :naam and Acca_ID = :Acca";
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("naam", OracleDbType.Varchar2);
                command.Parameters["naam"].Value = Wenslijst;
                command.Parameters.Add("Acca", OracleDbType.Varchar2);
                command.Parameters["Acca"].Value = GetAccountId(Profielnaam);
                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                    Product Pr = new Product();
                    Pr.Id = Data.GetInt32(0);
                    Pr.Naam = Data.GetString(1);
                    Pr.Categorie = Data.GetString(2);
                    Pr.Foto_Url = Data.GetString(3);
                    list.Add(Pr);
                }

            }
            return list;
        }

        /// <summary>
        /// Geeft alle producten van een persoon die in zijn vergelijklijst staan
        /// </summary>
        /// <param name="Profielnaam"></param>
        /// <returns></returns>
        public static List<Product> GetCompare(string Profielnaam)
        {// Alle Producten die een persoon in zijn Vergelijkingslijst heeft staan
            List<Product> list = new List<Product>();
            string str = "Select p.* from Acca a join wenslijst w on a.id = w.Acca_id join Wenslijst_product wp on w.ID = wp.Wenslijst_ID join Product p on wp.Product_id = p.id where w.naam = :naam and Acca_ID = :Acca";
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
               
                command.Parameters.Add("Acca", OracleDbType.Varchar2);
                command.Parameters["Acca"].Value = GetAccountId(Profielnaam);
                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                    Product Pr = new Product();
                    Pr.Id = Data.GetInt32(0);
                    Pr.Naam = Data.GetString(1);
                    Pr.Categorie = Data.GetString(2);
                    Pr.Foto_Url = Data.GetString(3);
                    list.Add(Pr);
                }

            }
            return list;
        }
        /// <summary>
        /// Voegt Wenslijst toe aan de user
        /// </summary>
        /// <param name="Profielnaam"></param>
        /// <param name="Naam">Wenslijst Naam</param>
        /// <returns></returns>
        public static bool AddWishlist(string Profielnaam, string Naam)
        {
            string str = "Insert into Wenslijst values(:Id , :Account , :Naam )";
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = GetNextID("Wenslijst");
                command.Parameters.Add("Account", OracleDbType.Int16);
                command.Parameters["Account"].Value = GetAccountId(Profielnaam); //Ik heb de profielnaam vawege de authenticator dus gebruik ik deze methode
                command.Parameters.Add("Naam", OracleDbType.Varchar2);
                command.Parameters["Naam"].Value = Naam;

                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Voegt product toe aan vergelijkings lijst
        /// </summary>
        /// <param name="p">Te vergelijken Product</param>
        /// <param name="profielnaam"></param>
        /// <returns></returns>
        public static bool AddToCompare(Product p, string profielnaam)
        {
            string str = "Insert into Vergelijking values(:Id , :Account )";
            if (Openconnecion() && CheckCompare(p,profielnaam))
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = p.Id;
                command.Parameters.Add("Account", OracleDbType.Int16);
                command.Parameters["Account"].Value = GetAccountId(profielnaam); 
               

                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checked of het product al aanwezig is in de lijst
        /// </summary>
        /// <param name="p"></param>
        /// <param name="profielnaam"></param>
        /// <returns>False wanneer hij erin zit</returns>
        public static bool CheckCompare(Product p, string profielnaam)
        {
            //Ik check of de 
            string str = "select * from Vergelijking where Account_ID = :Account and Product_ID = :Id";
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = p.Id;
                command.Parameters.Add("Account", OracleDbType.Int16);
                command.Parameters["Account"].Value = GetAccountId(profielnaam);


                OracleDataReader Data = command.ExecuteReader();
                if (Data.HasRows)
                {
                    return false;
                }
            
            
            }
            return true;
        }


        /// <summary>
        /// Checked of het product al in de wenslijst bevind
        /// </summary>
        /// <param name="Wenslijst"></param>
        /// <param name="Product"></param>
        /// <returns>Zo ja returned false</returns>
        public static bool CheckWenslijst(int Wenslijst, int Product)
        {
            string str = "Select * From Wenslijst_Product where Product_ID = :p and Wenslijst_ID = :w ";
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("p", OracleDbType.Int16);
                command.Parameters["p"].Value = Product;
                command.Parameters.Add("w", OracleDbType.Int16);
                command.Parameters["w"].Value = Wenslijst;
             OracleDataReader Data =   command.ExecuteReader();
                if (Data.HasRows)
                {
                    return false;
                }
                

                
                
            }
            return true;

        }
        /// <summary>
        /// Voegt je product toe aan een bepaalde wenslijst
        /// </summary>
        /// <param name="Wenslijst"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static bool AddProductToWishlist(int Wenslijst, int Product)
        {
            if (CheckWenslijst(Wenslijst,Product))
            {
                string str = "Insert into Wenslijst_Product values(:Id , :IdP , 1 )";
                if (Openconnecion())
                {
                    OracleCommand command = new OracleCommand(str);
                    command.Connection = Conn;
                    command.Parameters.Add("Id", OracleDbType.Int16);
                    command.Parameters["Id"].Value = Wenslijst;
                    command.Parameters.Add("IdP", OracleDbType.Int16);
                    command.Parameters["IdP"].Value = Product;


                    command.ExecuteNonQuery();
                    return true;
                }
            }
            return false;
           
        }
        /// <summary>
        /// Verwijderd product uit de beteffende wenslijst
        /// </summary>
        /// <param name="Wenslijst">Wenslijst Naam</param>
        /// <param name="Product">Product id</param>
        /// <param name="profielnaam"></param>
        /// <returns></returns>
        public static bool RemoveProductFromWishList(string Wenslijst, int Product, string profielnaam)
        {
            Conn.Close();
            string str = "delete from Wenslijst_product wp where wp.Product_ID = :Idp and wp.Wenslijst_ID = (select w.id from wenslijst w where w.naam = :Lijstnaam and w.acca_id = :AccaId) ";
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("IdP", OracleDbType.Int16);
                command.Parameters["IdP"].Value = Product;
                command.Parameters.Add("Lijstnaam", OracleDbType.Varchar2);
                command.Parameters["Lijstnaam"].Value = Wenslijst;
                command.Parameters.Add("AccaId", OracleDbType.Int16);
                command.Parameters["AccaId"].Value = GetAccountId(profielnaam);


                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Lijst van alle wenslijsten van een user in een wenlijstviewmodel 
        /// </summary>
        /// <param name="profielnaam"></param>
        /// <returns></returns>
        public static List<WenslijstViewModel> GetWenslijsten(string profielnaam)
        {
            List<WenslijstViewModel> list = new List<WenslijstViewModel>();
            string str = "select * from wenslijst where Acca_ID = "+ GetAccountId(profielnaam);
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;

               OracleDataReader Data =  command.ExecuteReader();
                while (Data.Read())
                {
                   WenslijstViewModel w =  new WenslijstViewModel();
                    w.Id = Data.GetInt32(0);
                    w.Naam = Data.GetString(2);

                    list.Add(w);
                }
            }
            return list;


        }
        /// <summary>
        /// Verwijder een wenslijst
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool RemoveWishList(int id)
        {
            string str = "delete from Wenslijst_Product where Wenslijst_ID = "+ id;
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;

                command.ExecuteNonQuery();
            }
           str = "delete from Wenslijst where ID = " + id;
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;

                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }




        #endregion

#region Account
        /// <summary>
        /// Geeft informatie van een account
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns></returns>
        public static Account GetAccount(int id)
        {
            Account A = new Account();
            //Informatie over een account verkrijgen
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
        /// <summary>
        /// Geeft het account id op basis van de profielnaam
        /// </summary>
        /// <param name="Naam">Profielnaam</param>
        /// <returns></returns>
        public static int GetAccountId(string Naam)
        {
            int Nummer = 0;
            //Informatie over een account verkrijgen
            string str = "Select * from Acca where Profielnaam = :Id";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Varchar2);
                command.Parameters["Id"].Value = Naam;
                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                    Nummer = Data.GetInt32(0);
                 


                }

            }

            return Nummer;

        }

        /// <summary>
        /// Checked of de combinatie voorkomt in de database
        /// </summary>
        /// <param name="profiel">Profielnaam</param>
        /// <param name="wacht">Wachtwoord</param>
        /// <returns></returns>
        public static bool Isvalid(string profiel, string wacht)
        {
            string str = "Select * From Acca where Profielnaam = :Naam and Wachtwoord = :wacht";

            if (Openconnecion())
            {
                //Checken of de gebruikersnaam en wachtwoord voorkomen in de database
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Naam", OracleDbType.Varchar2);
                command.Parameters["Naam"].Value = profiel;
                command.Parameters.Add("Wacht", OracleDbType.Varchar2);
                command.Parameters["Wacht"].Value = wacht;
                OracleDataReader Data = command.ExecuteReader();
                //Als er een waarde is gevonden returned true
                if (Data.HasRows)
                {
                    return true;
                }
                return false;


            }
            return false;
        }
        /// <summary>
        /// Checked of de Profielnaam nog niet in gebruik is voordat je registreerd
        /// </summary>
        /// <param name="profiel"></param>
        /// <returns></returns>
        public static bool Isvalid(string profiel)
        {
            string str = "Select * From Acca where Profielnaam = :Naam";

            if (Openconnecion())
            {
                //Checken of de gebruikersnaam en wachtwoord voorkomen in de database
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Naam", OracleDbType.Varchar2);
                command.Parameters["Naam"].Value = profiel;
                
                OracleDataReader Data = command.ExecuteReader();
                //Als de naam voorkomt returned true
                if (Data.HasRows)
                {
                    return true;
                }
                return false;


            }
            return false;
        }
        /// <summary>
        /// Checked Het account en registreerd deze
        /// </summary>
        /// <param name="A">Te registreren Account model</param>
        /// <returns>Succes</returns>
        public static bool RegisterAccount(Account A)
        {

            A.Id = GetNextID("Acca");
            string str =
                "Insert into Acca Values(:id , :Naam , sysdate , :Geslacht , :Woonplaats , :Opleiding , :Profielnaam ,  Sysdate , Sysdate , :Wachtwoord )";
            try
            {
                if (Openconnecion() && !Database.Isvalid(A.ProfielNaam))
                {
                    
                    OracleCommand command = new OracleCommand(str);
                    command.Connection = Conn;
                    command.Parameters.Add("id", OracleDbType.Varchar2);
                    command.Parameters["id"].Value = A.Id;
                    command.Parameters.Add("Naam", OracleDbType.Varchar2);
                    command.Parameters["Naam"].Value = A.Naam;
                    command.Parameters.Add("Geslacht", OracleDbType.Varchar2);
                    command.Parameters["Geslacht"].Value = A.Geslacht;
                    command.Parameters.Add("Woonplaats", OracleDbType.Varchar2);
                    command.Parameters["Woonplaats"].Value = A.Woonplaats;
                    command.Parameters.Add("Opleiding", OracleDbType.Varchar2);
                    command.Parameters["Opleiding"].Value = A.Opleiding;
                    command.Parameters.Add("Profielnaam", OracleDbType.Varchar2);
                    command.Parameters["Profielnaam"].Value = A.ProfielNaam;
                    command.Parameters.Add("Wachtwoord", OracleDbType.Varchar2);
                    command.Parameters["Wachtwoord"].Value = A.Wachtwoord;
                    command.ExecuteNonQuery();

                    return true;


                }
            }
            catch (OracleException exception)
            {

                return false;
            }


            return false;
        }


#endregion
        /// <summary>
        /// Geeft de volgende vrije id uit een bepaalde table
        /// </summary>
        /// <param name="Table">Tabel naam</param>
        /// <returns></returns>
        public static int GetNextID(string Table)
        {
            string str = "Select Max(ID) From " + Table;
            if (Openconnecion())
            {
                try
                {
                    OracleCommand command = new OracleCommand(str);
                    command.Connection = Conn;
                    OracleDataReader Data = command.ExecuteReader();
                    Data.Read();
                    return Data.GetInt32(0) + 1;

                }
                catch (OracleException)
                {

                    return 1;
                }



            }
            return 0;



        }

#region Posts 
        /// <summary>
        /// Krijg alle Hoofdposts uit een bepaalde map
        /// </summary>
        /// <param name="id">Map id</param>
        /// <returns></returns>
        public static List<Post> GetPosts(int id)
        {
            List<Post> post = new List<Post>();
            // Dit zijn alle Beginpost van een bepaalde map
            string str = "Select p.*, A.Naam, A.Profielnaam from Post p, Acca a where p.Acca_id = a.ID and Mappy_ID = :Id and Post_ID is null";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = id;
                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                 Post p = new Post();
                    p.Id = Data.GetInt32(0);
                    p.Message = Data.GetString(1);
                    p.Onderwerp = Data.GetString(3);
                    p.PrePost = new Post();
                   
                    p.Mappy = Data.GetInt32(5);
                    p.From = new Account();
                    p.From.Naam = Data.GetString(6);
                    p.From.ProfielNaam = Data.GetString(7);
                    post.Add(p);

                }

            }
            return post;

        }
        /// <summary>
        /// Verwijderd de ketting van een hoofdpost en zijn antwoorden
        /// </summary>
        /// <param name="mappy">Map id</param>
        /// <param name="Onderwerp">Unieke Post onderwerp</param>
        /// <returns></returns>
        public static bool DeleteChainPost(int mappy, string Onderwerp)
        {
            List<Post> post = new List<Post>();
            // Dit zijn alle Beginpost van een bepaalde map
            string str = "delete from post p where p.onderwerp = :onderwerp and p.mappy_id = :Id";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("onderwerp", OracleDbType.Varchar2);
                command.Parameters["onderwerp"].Value = Onderwerp;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = mappy;
                try
                {
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                

                return true;
            }
            return false;

        }
        /// <summary>
        /// Verwijderd een enkele post uit de database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeletePost(int id)
        {
            // Ik check eerst of de post een main post is, want anders moet alles verwijder worden in de post
            if (CheckPost(id))
            {
                return true;
            }

            
            // Dit zijn alle Beginpost van een bepaalde map
            string str = "delete from post p where p.id = :Id";
            string str2 = "update post p set p.post_id = :value where p.post_id = :id";
            //Het is een chain van posts dus de tweede string is het aanpassen van de chain
            if (Openconnecion())
            {
               

                OracleCommand command2 = new OracleCommand(str2);
                command2.Connection = Conn;

                command2.Parameters.Add("value", OracleDbType.Int16);
                command2.Parameters["value"].Value = GetPrePost(id);
                command2.Parameters.Add("id", OracleDbType.Int16);
                command2.Parameters["id"].Value = id;
                command2.ExecuteNonQuery();

                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;

                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = id;
                command.ExecuteNonQuery();






                return true;
            }
            return false;

        }
        /// <summary>
        /// Checked of de post een Hoofdpost is zo ja dan word deze verwijderd
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Returns true wanneer de post verwijderd is</returns>
        public static bool CheckPost(int id)
        {
            Post post = new Post();

          //Hier check ik of de Post een mainPost is of niet
            string str = "select * from post p where p.id = :Id and p.post_id is null";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;

                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = id;
                OracleDataReader Data = command.ExecuteReader();
                if (Data.HasRows)
                {
                    Data.Read();
                    return DeleteChainPost(Data.GetInt32(5), Data.GetString(3));


                }



            }
            return false;

        }
        /// <summary>
        /// Geeft de post voor de betreffende post om een link te leggen
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        public static int GetPrePost(int id)
        {
           

            //geeft de int terug van de prepost van de te verwijderen om die toe te kennen aan de nieuwe in de reeks
            string str = "select p.post_id from post p where p.id = :Id";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;

                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = id;
                OracleDataReader Data = command.ExecuteReader();
                if (Data.HasRows)
                {
                    Data.Read();
                    return Data.GetInt32(0);


                }



            }
            return 0;

        }
        /// <summary>
        /// Krijgt alle mappen uit een hoofdonderwerp
        /// </summary>
        /// <param name="Hoofdonderwerp"></param>
        /// <returns></returns>
        public static List<Mappy> GetMappy(string Hoofdonderwerp)
        {
            List<Mappy> post = new List<Mappy>();

            string str = "Select * From mappy where Hoofdonderwerp = :hoofd";

            if (Openconnecion())
            {
                //Alle mappies uit een bepaalde hoofdonderwerp
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("hoofd", OracleDbType.Varchar2);
                command.Parameters["hoofd"].Value = Hoofdonderwerp;
                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                  Mappy m = new Mappy();
                    m.Id = Data.GetInt32(0);
                    m.Naam = Data.GetString(1);
                    m.Hoofdonderwerp = Data.GetString(2);
                    post.Add(m);

                }

            }
            return post;

        }
        /// <summary>
        /// Krijgt alle posts uit een conversatie met dezelfde onderwerp
        /// </summary>
        /// <param name="Onderwerp">Post onderwerp</param>
        /// <returns></returns>
        public static List<Post> GetChainPost(string Onderwerp)
        {
            //Alle posts Die in een conversatie zitten
            List<Post> post = new List<Post>();

            string str = "select p.*, A.Naam, A.ID, A.Profielnaam  from post p left join post p2 on p.id = p2.post_id join Acca A on A.ID = P.Acca_ID where p.Onderwerp = :ond ";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("ond", OracleDbType.Varchar2);
                command.Parameters["ond"].Value = Onderwerp;
                OracleDataReader Data = command.ExecuteReader();
                while (Data.Read())
                {
                    Account a = new Account();
                    a.Naam = Data.GetString(6);
                    a.Id = Data.GetInt32(7);
                    a.ProfielNaam = Data.GetString(8);
                    Post p = new Post();
                    p.Id = Data.GetInt32(0);
                    p.Message = Data.GetString(1);
                    p.Onderwerp = Data.GetString(3);
                    p.PrePost = new Post();
                   
                    p.Mappy = Data.GetInt32(5);
                    p.From = a;
                    post.Add(p);

                }

            }
            return post;

        }
        /// <summary>
        /// Krijgt alle hoofdposts uit een bepaalde map
        /// </summary>
        /// <param name="id">Map id</param>
        /// <returns></returns>
        public static List<Post> GetMainPosts(int id)
        {
            //Alle beginnende posts, dus die niet een antwoord zijn van een andere post
            List<Post> List = new List<Post>();
            string str = "Select p.* from post p join Mappy M on p.Mappy_ID = M.ID where Post_ID is null and m.ID = :hoofd";

            OracleCommand command = new OracleCommand(str);
            command.Connection = Conn;
            command.Parameters.Add("hoofd", OracleDbType.Varchar2);
            command.Parameters["hoofd"].Value = id;
            OracleDataReader Data = command.ExecuteReader();
            while (Data.Read())
            {
                Post p = new Post();
                p.Id = Data.GetInt32(0);
                p.Onderwerp = Data.GetString(3);
            }

            return List;
        }
        /// <summary>
        /// Voegt een hoofdpost toe
        /// </summary>
        /// <param name="post"></param>
        /// <param name="profielnaam"></param>
        /// <returns></returns>
        public static bool AddPosts(Post post, string profielnaam)
        {
            
            //Adding van een bepaalde post
            string str = "Insert into Post Values(:id , :Bericht , :Acca , :Onderwerp , null , :Mappy)";

            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = GetNextID("Post");
                command.Parameters.Add("Bericht", OracleDbType.Varchar2);
                command.Parameters["Bericht"].Value = post.Message;
                command.Parameters.Add("Acca", OracleDbType.Int16);
                command.Parameters["Acca"].Value = GetAccountId(profielnaam);
                command.Parameters.Add("Onderwerp", OracleDbType.Varchar2);
                command.Parameters["Onderwerp"].Value = post.Onderwerp;
                command.Parameters.Add("Mappy", OracleDbType.Int16);
                command.Parameters["Mappy"].Value = post.Mappy;
                OracleDataReader Data = command.ExecuteReader();
                return true;

            }
            return false;

        }
        /// <summary>
        /// Reageerd op een hoofdpost of een post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="profielnaam"></param>
        /// <returns></returns>
        public static bool ReactPosts(Post post, string profielnaam)
        {


            string str = "Insert into Post Values(:id , :Bericht , :Acca , :Onderwerp , :React , :Mappy)";
            //Als reactie op een bepaalde post
            if (Openconnecion())
            {
                OracleCommand command = new OracleCommand(str);
                command.Connection = Conn;
                command.Parameters.Add("Id", OracleDbType.Int16);
                command.Parameters["Id"].Value = GetNextID("Post");
                command.Parameters.Add("Bericht", OracleDbType.Varchar2);
                command.Parameters["Bericht"].Value = post.Message;
                command.Parameters.Add("Acca", OracleDbType.Int16);
                command.Parameters["Acca"].Value = GetAccountId(profielnaam);
                command.Parameters.Add("Onderwerp", OracleDbType.Varchar2);
                command.Parameters["Onderwerp"].Value = post.Onderwerp;
                command.Parameters.Add("React", OracleDbType.Varchar2);
                command.Parameters["React"].Value = post.PrePost.Id;
                command.Parameters.Add("Mappy", OracleDbType.Int16);
                command.Parameters["Mappy"].Value = post.Mappy;
                OracleDataReader Data = command.ExecuteReader();
                return true;

            }
            return false;

        }

        #endregion


    }
}