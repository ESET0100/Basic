using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    internal class RoomType
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; }
        public int Capacity { get; set; }
        public decimal BasePrice { get; set; }

        // Constructor 1: Most basic - name and price
        public RoomType(string typeName, decimal basePrice)
        {
            TypeName = typeName;
            BasePrice = basePrice;
        }

        // Constructor 2: With capacity
        public RoomType(string typeName, int capacity, decimal basePrice)
        {
            TypeName = typeName;
            Capacity = capacity;
            BasePrice = basePrice;
        }

        public bool CanAccommodate(int guests)
        {
            return guests <= Capacity;
        }

        public decimal CalculatePrice(int nights)
        {
            return BasePrice * nights;
        }

        public string GetTypeById(int id)
        {
            if (this.RoomTypeID == id)
            {
                return this.TypeName;
            }
            return "Room type not found";
        }
        // Calculate seasonal pricing
        public decimal CalculateSeasonalPrice(bool isPeakSeason, bool isWeekend)
        {
            decimal price = BasePrice;

            if (isPeakSeason)
                price *= 1.20m; // 20% increase in peak season

            if (isWeekend)
                price *= 1.15m; // 15% increase on weekends

            return Math.Round(price, 2);
        }

        // Suggest room based on guest count and budget
        public string SuggestRoom(int guestCount, decimal maxBudget)
        {
            if (Capacity >= guestCount && BasePrice <= maxBudget)
                return $"Perfect match: {TypeName} can accommodate {guestCount} guests at ₹{BasePrice}/night";
            else if (Capacity >= guestCount)
                return $"{TypeName} fits {guestCount} guests but exceeds budget by ₹{BasePrice - maxBudget}";
            else
                return $"{TypeName} cannot accommodate {guestCount} guests (max: {Capacity})";
        }

    }
}
