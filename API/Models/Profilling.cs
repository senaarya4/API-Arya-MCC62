﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table ("TB_M_Profilling")]
    public class Profilling
    {
        [Key]
        public string NIK { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }

        [ForeignKey("Education")]
        public int EducationId { get; set; }

        [JsonIgnore]
        public virtual Education Education { get; set; }

    }
}
