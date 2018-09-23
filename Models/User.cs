using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int user_id {get;set;}
        public string first_name {get;set;}
        public string last_name {get;set;}
        public string address {get;set;}
        public string city {get;set;}
        public string state {get;set;}
        public string zip {get;set;}
        public string phone {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public List<Message> Messages {get;set;}
        public List<Comment> Comments {get;set;}
        public User()
        {
            Messages = new List<Message>();
            Comments = new List<Comment>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }   
}