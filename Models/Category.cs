using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int category_id {get;set;}
        public string name {get;set;}
        public Category()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}