using System;
using System.Collections.Generic;
using System.Text;
using static Bogus.DataSets.Name;

namespace BogusHW.Classes
{
    class User
    {
        public string name;
        public string telNumer;
        public int CityId;

        public Gender gender = new Gender();
        public User(string name, string telNumer, int CityId)
        {
            this.name = name;
            this.telNumer = telNumer;
            this.CityId = CityId;
        }

        public User()
        {

        }
    }
}
