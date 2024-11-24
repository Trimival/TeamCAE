using System;

namespace TeamCAE.Tables {
    public class FavoriteFood {
        public int ID { get; set; }
        public string FoodName { get; set; }
        public string MealOfDay { get; set; }
        public int Calories { get; set; }
        public bool IsVegetarian { get; set; }
    }
}