﻿namespace CasgemMicroservice.Services.Discount.Dtos
{
    public class CreateDiscountDto
    {
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
