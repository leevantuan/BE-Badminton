namespace Data_Transfer_Object.CourtDTO
{
    public class GetCourt
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string? Description { get; set; }

        public bool IsStatus { get; set; }
    }
}
