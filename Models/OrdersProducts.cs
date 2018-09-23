using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class OrdersProducts
    {
        [Key]
        public int orders_products_id {get;set;}
        public int order_id {get;set;}
        public Order Orders {get;set;}
        public int product_id {get;set;}
        public Product Products {get;set;}
    }
}