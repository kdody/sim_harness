using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hats
{
    namespace Reader
    {

        public class coordinates
        {
            public string x { get; set; }
            public string y { get; set; }
            public string z { get; set; }
        }
        public class Users
        {
            public string UserID { get; set; }
            public string Password { get; set; }
            public coordinates Coordinates { get; set; }
        }
        public class Configuration
        {
            public string storageLocation { get; set; }
            public List<Users> users { get; set; }
        }
        public class Houses
        {
            public List<Rooms> Roomss { get; set; }
        }

        public class Rooms
        {
            public List<RoomID> RoomIDs { get; set; }
        }

        public class RoomID
        {
            public string Type { get; set; }
            public dimensions dimensionss { get; set; }
            public string roomlevel { get; set; }
            public List<connectingrooms> connectingroomss { get; set; }
            public string roomname { get; set; }
            public List<devices> devicess { get; set; }
        }

        public class dimensions
        {
            public int width { get; set; }
            public int length { get; set; }
        }

        public class connectingrooms
        {

        }

        public class devices
        {
            public List<dev_type> dev_types { get; set; }
        }

        public class dev_type
        {
            public Lights Lightss { get; set; }
            public Garagedoor Garagedoors { get; set; }
            public Ceilingfan Ceilingfans { get; set; }
            public Thermostat Thermostats { get; set; }
            public Alarmsystem Alarmsystem { get; set; }
            public Refrigerator Refrigerators { get; set; }
            public Motionsensor Motionsensor { get; set; }
        }

        public class Lights
        {
            public String light_name { get; set; }
            public String light_ID { get; set; }
            public String light_startstate { get; set; }
            public String light_brightness { get; set; }
        }

        public class Garagedoor
        {
            public String gd_name { get; set; }
            public String gd_ID { get; set; }
            public String gd_startstate { get; set; }
        }

        public class Ceilingfan
        {
            public String cf_name { get; set; }
            public String cf_ID { get; set; }
            public String cf_startstate { get; set; }
            public String cf_speed { get; set; }
        }

        public class Thermostat
        {
            public String t_name { get; set; }
            public String t_ID { get; set; }
            public String t_startstate { get; set; }
            public String t_temp { get; set; }
        }

        public class Alarmsystem
        {
            public String a_name { get; set; }
            public String a_ID { get; set; }
            public String a_startstate { get; set; }
        }

        public class Motionsensor
        {
            public String m_name { get; set; }
            public String m_ID { get; set; }
            public String m_startstate { get; set; }
        }

        public class Refrigerator
        {
            public String r_name { get; set; }
            public String r_ID { get; set; }
            public String r_startstate { get; set; }
        }
        public class toVerify
        {
            public List<string> userNames  = new List<string>();
            public List<string> houseIDs = new List<string>();
            public List<string> deviceIDs = new List<string>();
        }
        public class Reader
        {

            public toVerify readFile(string fileName)
            {
                toVerify output = new toVerify();
                var myConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(System.IO.File.ReadAllText(fileName));
                Console.WriteLine("{0} is the storage location.", myConfig.storageLocation);

                foreach (Users item in myConfig.users)
                {
                    Console.WriteLine(item.UserID);
                    output.userNames.Add(item.UserID);
                    Console.WriteLine(item.Coordinates.x + "  " + item.Coordinates.y + "  " + item.Coordinates.z);
                }
                // wait for keypress to close console
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return (output);
            }
        }
    }
}

