namespace RestaurantApi.Entities
{
    public class Dish
    {//testgit
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestauantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}