using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_4
{
    public partial class CarsInfo
    {

        public int Id { get; set; }

        public int CarId { get; set; }

        public string CarType { get; set; }

  
        public int CurrectMileage { get; set; }
        
        public string Picture { get; set; }
      
        public string ProperForRent { get; set; }

   
        public string AvailableForRent { get; set; }
    }
}
