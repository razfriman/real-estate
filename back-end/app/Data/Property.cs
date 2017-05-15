using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Data
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropertyID { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public decimal Value { get; set; }

        public decimal TotalRentPaid { get; set; }

        public int MonthsPaid { get; set; }

        public decimal RentPayment { get; set; }

        public decimal ReturnOnInvestment { get; set; }

        public int? TenantID { get; set; }

        public Tenant Tenant { get; set; }

        public string ImageURL { get; set; }

        public DateTime DateLastPaid { get; set; }
    }
}
