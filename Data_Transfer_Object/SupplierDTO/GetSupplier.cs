namespace Data_Transfer_Object.SupplierDTO
{
    public class GetSupplier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
