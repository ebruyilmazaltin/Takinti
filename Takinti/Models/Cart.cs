using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Takinti.Models
{
    public class Cart
    {
        public Cart()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            CartItems = new HashSet<CartItem>(); //liste oluşturulur boş koleksiyon.hızlı olsun diye kullanıyoruz.


        }
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User{ get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int ProductCount { get { return CartItems.Sum(ci=>ci.Quentity); } }
        public decimal TotalPrice { get { return CartItems.Sum(CartItem => CartItem.TotalPrice); } } // set i olmayan alanlar veritabanına kaydolmamaktadır
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}