using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ansab.Models
{
    public class Relation
    {
        public int Id { get; set; }


        public int HusbandId { get; set; }
        [ForeignKey("HusbandId")]
        public Person Husband { get; set; }

        public int WifeId { get; set; }
        [ForeignKey("WifeId")]
        public Person Wife { get; set; }

        [InverseProperty("Relation")]
        public ICollection<Person> Children { get; set; }
    }
}