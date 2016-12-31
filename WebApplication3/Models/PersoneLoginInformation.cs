using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    /// <summary>
    /// Class PersoneLoginInformation is a Model of User credentials.
    /// User credentias are password and login
    /// </summary>
    class PersoneLoginInformation
    {
        /// <value>Property <c>login</c> represents the login of the User</value>
        private String login { get; set; }

        /// <value>Property <c>password</c> represents the password of the User</value>
        private String password { get; set; }

        public PersoneLoginInformation() {
            this.login = "";
            this.password = "";
        }


    }
}
