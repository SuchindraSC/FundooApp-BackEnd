using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModel
{
    public class NotesModel
    {
        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public virtual UserModel UserModel { get; set; }

        [Key]
        public int NotesId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }

        [DefaultValue(false)]
        public bool Is_Pin { get; set; }

        [DefaultValue(false)]
        public bool Is_Archieve { get; set; }

        [DefaultValue(false)]
        public bool Is_Trash { get; set; }
    }
}
