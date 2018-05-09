using System;

namespace BigShop.Common.ViewModels
{
    public class RevenueStatisticViewModel
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
        public decimal Benefit { get; set; }
    }
}