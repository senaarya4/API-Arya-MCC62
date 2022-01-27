﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profilling> profillings { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual University University { get; set; }


    }
}
