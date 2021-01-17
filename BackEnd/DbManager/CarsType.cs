using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_4
{
    public partial class CarsType
    {

        public int Id { get; set; }
        public string Model { get; set; }

      
        public string Manufacturer { get; set; }

      
        public string DayValue { get; set; }

       
        public string LateDayValue { get; set; }

        
        public string ManufacturerYear { get; set; }

     
        public string GearType { get; set; }

        public int CarId { get; set; }
    }
}
