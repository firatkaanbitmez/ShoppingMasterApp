using ShoppingMasterApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Review : BaseEntity, IAggregateRoot
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }  // 1-5 yıldız
        public DateTime ReviewDate { get; set; }
    }

}
