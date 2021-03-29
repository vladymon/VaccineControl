using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VC.Common.Entities
{
    public class Calendar
    {
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Id { get; set; }

        [DisplayName("Año")]
        public int Year { get; set; }

        [DisplayName("Mes")]
        public int Month { get; set; }

        [DisplayName("Día")]
        public int Day { get; set; }

        [MaxLength(15)]
        [DisplayName("Nombre Mes")]
        public string MonthName { get; set; }

        [MaxLength(15)]
        [DisplayName("Nombre Semana")]
        public string WeekName { get; set; }

        public bool IsValid { get; set; }

        [NotMapped]
        public int Position { get; set; }

        [NotMapped]
        public bool IsAviable { get; set; }
    }
}
