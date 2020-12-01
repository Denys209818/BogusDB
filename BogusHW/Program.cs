using BogusHW.Classes;
using System;
using System.Data.SqlClient;
using System.Text;

namespace BogusHW
{
    class Program
    {
        //  Формує БД та таблички (Використовується у методі InsertToDataBase)
        //  conn.ExecuteConnection

        //  Метод що заповнює данними таблички
        //  conn.InsertToDataBase

        //  Метод що повертає всі Id з таблиць tblCity
        //  GetCountOfCities

        //  Детальніше про методи можна прочитати при наведенні на них мишкою
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            string strConn = "Data Source=serverpu816.database.windows.net;Initial Catalog=test;Persist Security Info=True;User ID=pu816;Password=Qwerty1*";

            SqlConn conn = new SqlConn(strConn);

            conn.InsertToDataBase();
        }
    }
}
