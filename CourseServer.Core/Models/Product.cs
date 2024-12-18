﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CourseServer.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public double Amount { get; set; }
        public int? PricingFeatureId { get; set; }
        [ForeignKey(nameof(PricingFeatureId))]
        [InverseProperty(nameof(ProductPricingFeature.Product))]
        public ProductPricingFeature? PricingFeature { get; set; }

        public int? TypeFeatureId { get; set; }
        [ForeignKey(nameof(TypeFeatureId))]
        [InverseProperty(nameof(ProductTypeFeature.Product))]
        public ProductTypeFeature? TypeFeature { get; set; }
    }
}
