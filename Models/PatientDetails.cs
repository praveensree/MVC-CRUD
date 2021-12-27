using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{

    public class PatientDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age{ get; set; }
        public string Contact { get; set; }
    }
}
