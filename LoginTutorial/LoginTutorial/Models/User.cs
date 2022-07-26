using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoginTutorial.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required(ErrorMessage = "Un User Name est requis."), StringLength(20, ErrorMessage = "Ce champ User Name ne doit pas dépasser 20 caractères.")]
        public string UserName { get; set; }

        [MinLength(6,ErrorMessage = "Password est très cours."), Required(ErrorMessage = "Un Password est requis."), StringLength(10, ErrorMessage = "Ce champ Password ne doit pas dépasser 10 caractères.")]

        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Une adresse email valide est requise.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Un numéro telephone valide est requise."), StringLength(8, ErrorMessage = "Ce champ Phone ne doit pas dépasser 8 chiffres.")]
        public string Phone { get; set; }
        public User()
        {

        }
        public User(string userName, string password, string email, string phone)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Phone = phone;
        }
    }
}
