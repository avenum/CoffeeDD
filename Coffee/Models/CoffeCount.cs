namespace Coffee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoffeCount")]
    public partial class CoffeCount
    {
        public int Id { get; set; }

        public int CoffeLoverId { get; set; }

        public DateTime DateTime { get; set; }

        public virtual CoffeLover CoffeLover { get; set; }
    }
}
