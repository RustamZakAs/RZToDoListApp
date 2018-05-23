using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RZToDoListApp
{
    [DataContract]
    class RZTasks
    {
        [DataMember]
        public int RZTitle { get; set; }
        [DataMember]
        public bool RZDone { get; set; }
        [DataMember]
        public DateTime RZDateCreate { get; set; }
        [DataMember]
        public DateTime RZDateClose { get; set; }
        [DataMember]
        public string RZUser { get; set; }
    }
}
