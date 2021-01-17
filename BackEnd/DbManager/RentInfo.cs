using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_4
{
    public partial class RentInfo
    {

        public int RentId { get; set; }

        public int CarId { get; set; }

        
        public string RentDate { get; set; }

        
        public string ReturnDate { get; set; }

        
        public string RealReturnDate { get; set; }

       
        public string UserId { get; set; }
    }
}
