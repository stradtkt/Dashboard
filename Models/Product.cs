using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public int product_id {get;set;}
        public string name {get;set;}
        public string short_desc {get;set;}
        public string desc {get;set;}
        public string image {get;set;}
        public double price {get;set;}
        public double weight {get;set;}
        public int qty {get;set;}
        public List<ProductsCategories> ProductsCategories {get;set;}
        public Product()
        {
            ProductsCategories = new List<ProductsCategories>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}