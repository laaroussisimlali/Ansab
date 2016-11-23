using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ansab.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentGroupId { get; set; }
        [ForeignKey("ParentGroupId")]
        public Group ParentGroup { get; set; }
        [InverseProperty("ParentGroup")]
        public ICollection<Group> Groups { get; set; }
        [InverseProperty("Group")]
        public ICollection<Person> Roots { get; set; }
    }

    [NotMapped]
    public class GroupVM : Group
    {
        public GroupVM(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            ParentGroupId = group.ParentGroupId;
            ParentGroup = group.ParentGroup;
            Groups = group.Groups;
            Roots = group.Roots;
        }
    }
}