using System;
using System.IO;
using System.Text;
//using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RZToDoListApp
{
    class RZMain
    {
        static void Main(string[] args)
        {
            List<RZAccount> RZAccountsList;
            RZAccountsList = new List<RZAccount>();

//            DataContractSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<RZAccount>));

            if (RZAccountsList.Count == 0)
            {
                Console.WriteLine("Users is not exist!");
                RZAccount.RZSignUp(ref RZAccountsList);  //if user is not then create new user 
            }
            else RZAccount.RZLogIn(ref RZAccountsList);



            //            RZAccountsList.Add();
        }
    }
}
