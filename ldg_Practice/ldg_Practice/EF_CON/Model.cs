using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ldg_Practice.EF_CON
{
    internal class Model
    {
        public class User
        {
            [Key]
            public int User_ID { get; set; }
            public string? Name { get; set; }
        }
    }
}
