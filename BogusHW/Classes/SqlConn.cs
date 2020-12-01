using Bogus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using static Bogus.DataSets.Name;

namespace BogusHW.Classes
{
    class SqlConn
    {
        string sqlConn;
        SqlConnection sql;

        public SqlConn(string sqlConn)
        {
            this.sqlConn = sqlConn;
            sql = new SqlConnection(sqlConn);
        }
        /// <summary>
        ///     Свторення БД та заповнення табличками
        /// </summary>
        public void ExecuteConnection() 
        {
            try
            {
                this.sql.Open();
                string query = File.ReadAllText(@"SQL\TableCreator.sql");
                SqlCommand comm = new SqlCommand(query, this.sql);
                comm.ExecuteNonQuery();
                this.sql.Close();
            } 
            catch  
            {
                Console.WriteLine("Error link DB!");
            }
        }
        /// <summary>
        ///     Метод для повернення всі Id що існують
        /// </summary>
        /// <returns>Повертає масив ключів елементів</returns>
        public List<int> GetCountOfCities() 
        {
            List<int> numbers = new List<int>();
            string query = File.ReadAllText(@"SQL\GetCountElements.sql");
            SqlCommand comm = new SqlCommand(query, this.sql);
            using (SqlDataReader data = comm.ExecuteReader()) 
            {
                while (data.Read()) 
                {
                    numbers.Add(int.Parse(data["Id"].ToString()));
                }
            }
            if (numbers.Count > 0) 
            {
                return numbers;
            }
            return null;
        }

        /// <summary>
        ///     Заповнює БД данними
        /// </summary>
        public void InsertToDataBase() 
        {
            //   Створює таблички
            this.ExecuteConnection();
            this.sql.Open();
            Faker<City> cities = new Faker<City>("uk");
            cities.RuleFor(c => c.name,  f  => f.Address.City());

            List<City> cit = new List<City>();

            for (int i =0; i < 10; i++) 
            {
                cit.Add(cities.Generate());
            }

            foreach (var item in cit)
            {
                string query = "INSERT INTO [dbo].[tblCity] (Name) VALUES (N'" + item.name + "')";
                SqlCommand command = new SqlCommand(query, this.sql);
                command.ExecuteNonQuery();
            }


            List<int> countCities = this.GetCountOfCities();
            if (countCities != null)
            {
                Random r = new Random();
                Faker<User> users = new Faker<User>("uk");
                users.RuleFor(us => us.gender, f => f.PickRandom<Gender>());
                users.RuleFor(us => us.name, (f, us) => f.Name.FirstName(us.gender));
                users.RuleFor(us => us.telNumer, f => f.Phone.PhoneNumber());
                users.RuleFor(us => us.CityId, f => countCities[r.Next(0, countCities.Count - 1)]);

                List<User> us = new List<User>();
                for (int i = 0; i < 10; i++)
                {
                    us.Add(users.Generate());
                }

                foreach (var item in us)
                {
                    string query = $"INSERT INTO [dbo].[tblUser] ([Name],[telNummer],[CityId]) " +
                        $"VALUES (N'{item.name}', " +
                        $"N'{item.telNumer}', {item.CityId} )";
                    SqlCommand command = new SqlCommand(query, this.sql);
                    command.ExecuteNonQuery();
                }
            }

            this.sql.Close();
        }
    }
}
