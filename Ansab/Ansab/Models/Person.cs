using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ansab.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }



        public int? RelationId { get; set; }
        [ForeignKey("RelationId")]
        public Relation Relation { get; set; }

        [InverseProperty("Husband")]
        public ICollection<Relation> Relations1 { get; set; }
        [InverseProperty("Wife")]
        public ICollection<Relation> Relations2 { get; set; }


        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }

    public enum GenderType
    {
        Male,
        Female
    }
}