using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Presentation.ViewModels
{    
    public class ProductResultModel
    {
        [JsonPropertyName("productid")]
        public Guid ProductId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("stock")]
        public long Stock { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("createdby")]
        public long CreatedBy { get; set; }
        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("modifiedby")]
        public long ModifiedBy { get; set; }
        [JsonPropertyName("modifiedat")]
        public DateTime ModifiedAt { get; set; }
    }
}
