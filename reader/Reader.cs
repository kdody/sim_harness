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
            public List<Houses> houses { get; set; }
        }
        public class Houses
        {
            public string name { get; set; }
            public List<devices> devices { get; set; }
            public List<Rooms> rooms { get; set; }
        }
        public class devices
        {
            public string name { get; set; }
            public string type { get; set; }
            public string sim { get; set; }
            public string startState { get; set; }
        }

        public class Rooms
        {   
            public List<RoomID> RoomIDs { get; set; }
            
        }

        public class RoomID
        {
            public string type { get; set; }
            public string name { get; set; }
            public dimensions dimensions { get; set; }
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


                foreach (Users item in myConfig.users)
                {
                    output.userNames.Add(item.UserID);
                }
               
                foreach(Houses item in myConfig.houses)
                {
                    output.houseIDs.Add(item.name);
                    foreach(devices item2 in item.devices)
                    {

                        output.deviceIDs.Add(item2.name);
                    }

                } 
                return (output);
            }
        }
    }
}

