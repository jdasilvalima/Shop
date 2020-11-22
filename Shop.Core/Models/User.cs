using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = ("Email obligatoire"))]
        [EmailAddress(ErrorMessage = "Entrez une addresse valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = ("Mot de Passe obligatoire"))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public SecurityLevel Level { get; set; }
        public enum SecurityLevel { ADMIN, BUYER };
    }
}
