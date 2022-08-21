using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonimus.Model
{
    public class Password
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Mail { get; set; }
        [MaxLength(255)]
        public string Sifre { get; set; }
        [MaxLength(255)]
        public string Date { get; set; }
        [MaxLength(255)]
        public string Color { get; set; }

        [MaxLength(255)]
        public string UserName { get; set; }
        [MaxLength(255)]
        public string DOB { get; set; }
        public int Length { get; set; }
        public bool isNameChecked { get; set; }
        public bool isDobChecked { get; set; }
        public bool isNumberChecked { get; set; }
        public bool isSpecialCharChecked { get; set; }
        public bool isCharChecked { get; set; }
    }
}
