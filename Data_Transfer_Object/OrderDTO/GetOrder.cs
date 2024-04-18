namespace Data_Transfer_Object.OrderDTO
{
    public class GetOrder
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreateAt { get; set; }

        public double Total { get; set; }

        public bool IsStatus { get; set; }
    }
}
