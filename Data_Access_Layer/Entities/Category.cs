namespace Data_Access_Layer.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsStatus { get; set; }

        //navigation
        public virtual ICollection<Product> Product { get; set; }
    }
}
