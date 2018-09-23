using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Order : BaseEntity
    {
        [Key]
        public int order_id {get;set;}
        public int user_id {get;set;}
        public User Users {get;set;}
        public double sub_total {get;set;}
        public double tax {get;set;}
        public double total {get;set;}
        public List<OrdersProducts> OrdersProducts {get;set;}
        public Order()
        {
            OrdersProducts = new List<OrdersProducts>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}