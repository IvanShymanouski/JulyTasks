using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Four_tasks.Models
{
    [DataContract]
    public class Contact
    {
        [Display(Name="Age")]
        [DataMember]
        public long Age { get; set; }

        [Display(Name = "Name")]
        [DataMember]
        public string Name { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [DataMember]
        public string Phone { get; set; }
    }
}