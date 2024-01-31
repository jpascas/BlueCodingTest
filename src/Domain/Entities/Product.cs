using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public Guid ProductId { get; set; }                        
        public string Name { get; set; }
        public int Status { get; set; }
        public long Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }    
    }
}
