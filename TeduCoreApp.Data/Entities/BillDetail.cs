using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("BillDetail")]
    public class BillDetail : DomainEntity<int>, ISwitchable
    {
        public BillDetail()
        {
        }

        public BillDetail(int id, int productId, int quantity, decimal price, int billId, int colorId, int sizeId)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            BillId = billId;
            ColorId = colorId;
            SizeId = sizeId;
        }

        public BillDetail(int productId, int quantity, decimal price, int billId, int colorId, int sizeId)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            BillId = billId;
            ColorId = colorId;
            SizeId = sizeId;
        }

        

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int BillId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        [ForeignKey("BillId")] public virtual Bill Bill { get; set; }

        [ForeignKey("SizeId")] public virtual Size Size { get; set; }

        [ForeignKey("ColorId")] public virtual Color Color { get; set; }

        [DefaultValue(Status.Active)] public Status Status { get; set; } = Status.Active;
    }
}