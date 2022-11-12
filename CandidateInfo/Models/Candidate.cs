using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateInfo.Models
{
    public class Candidate
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public DateTime? CallingTime { get; set; }

        public string LinkedIn { get; set; }

        public string GitHub { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
