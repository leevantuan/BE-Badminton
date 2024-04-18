namespace Data_Transfer_Object.SaleDTO
{
    public class SaleRequestDTO
    {
        public string Name { get; set; }

        public DateTime StartDay { get; set; }

        public DateTime EndDay { get; set; }

        public double Precent { get; set; }

        public bool IsStatus { get; set; }
    }
}
