using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeHealthTracker.Domain.Model
{
    public class User
    {
        [Key]
        public System.Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
