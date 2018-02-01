using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Bill")]
    public class Bill : DomainEntity<int>, IDateTracking, ISortable, ISwitchable
    {
        public Bill()
        {
        }

        public Bill(string customerName, string customerAddress, string customerMobile, string customerMessage,
            PaymentMethod paymentMethod,
            BillStatus billStatus, Status status, Guid customerId)
        {
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            CustomerMobile = customerMobile;
            CustomerMessage = customerMessage;
            PaymentMethod = paymentMethod;
            BillStatus = billStatus;
            Status = status;
            CustomerId = customerId;
        }

        public Bill(int id, string customerName, string customerAddress, string customerMobile,
            string customerMessage, PaymentMethod paymentMethod,
            BillStatus billStatus, Status status, Guid customerId)
        {
            Id = id;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            CustomerMobile = customerMobile;
            CustomerMessage = customerMessage;
            PaymentMethod = paymentMethod;
            BillStatus = billStatus;
            Status = status;
            CustomerId = customerId;
        }


        [Required] [StringLength(256)] public string CustomerName { get; set; }
       
        [Required] [StringLength(50)] public string CustomerMobile { get; set; }

        [StringLength(256)] public string CustomerAddress { get; set; }

        [StringLength(500)] public string CustomerMessage { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public BillStatus BillStatus { get; set; }

        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")] public virtual AppUser AppUser { get; set; }


        public ICollection<BillDetail> BillDetails { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int SortOrder { get; set; }

        [DefaultValue(Status.Active)] public Status Status { get; set; } = Status.Active;
    }
}