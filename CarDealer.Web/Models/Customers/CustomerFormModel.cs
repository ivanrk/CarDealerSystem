namespace CarDealer.Web.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}
