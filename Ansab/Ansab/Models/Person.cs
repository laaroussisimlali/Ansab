using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;

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

    [NotMapped]
    public class PersonVM : Person
    {
        public PersonVM(Person person)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Gender = person.Gender;
            RelationId = person.RelationId;
            Relation = person.Relation;
            Relations = person.Relations1.Concat(person.Relations2).ToList();
        }

        public ICollection<Relation> Relations { get; set; }
        public ICollection<Person> Fathers { get; set; }
        public void GetFathers(ApplicationDbContext db)
        {
            var result = db.Database.SqlQuery<Person>(@"

                WITH peopleWithParentId AS
	                (
		                SELECT People.*, Relations.HusbandId as ParentId
		                FROM People
		                LEFT JOIN  Relations
		                ON People.RelationId=Relations.Id
	                ),

                tblParent AS
                (
                SELECT *
                FROM  peopleWithParentId WHERE Id = @PersonId
                UNION ALL
                SELECT peopleWithParentId.*
                FROM peopleWithParentId  JOIN tblParent  ON peopleWithParentId.Id = tblParent.ParentId
                )
                SELECT * FROM  tblParent
                WHERE Id <> @PersonId
                OPTION(MAXRECURSION 32767)",
                             new SqlParameter("@PersonId", Id));

            Fathers = result.ToList();
            if (Fathers.Count > 0)
            {
                var root = Fathers.Last();
                db.People.Attach(root);
                Group = db.Groups.Include(x => x.ParentGroup.ParentGroup.ParentGroup).Single(x => x.Id == root.GroupId);
            }
        }
    }
    public enum GenderType
    {
        Male,
        Female
    }
}