
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_4
{
    public partial class Users
    {
   
        
        public string Id { get; set; }

  
        public string FullName { get; set; }
      
        public string UserName { get; set; }

        public string DateOfBirth { get; set; }

        
        public string Gender { get; set; }
    
        public string Email { get; set; }
     
        public string Password { get; set; }

        public string Picture { get; set; }
       
        public string Role { get; set; }

        public Users()
        {

        }
        public override string ToString()
        {
            return $"{Id}  {FullName} {UserName}";
        }
    }
}
