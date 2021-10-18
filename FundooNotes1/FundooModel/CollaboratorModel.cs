using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModel
{
    public class CollaboratorModel
    {
        [Key]
        public int CollaboratorId { get; set; }

        [ForeignKey("NotesModel")]
        public int NotesId { get; set; }
        public virtual NotesModel NotesModel { get; set; }

        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "")]
        public string SenderEmailid { get; set; }

        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "")]
        public string ReceiverEmailid { get; set; }
    }
}
