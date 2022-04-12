using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Task_Managment.Models
{
    internal class Members
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Members(string email, string username, string password)
        {
            Email = email;
            UserName = username;
            Password = password;
        }

        public Members(Members refObj)
        {
            Email = refObj.Email;
            UserName = refObj.UserName;
            Password = refObj.Password;
        }
    }
}
