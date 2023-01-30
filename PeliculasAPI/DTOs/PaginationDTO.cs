namespace PeliculasAPI.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } =1;
        private int amountRegistersPerPage = 10;
        private readonly int maxAmountRegistersPerPage = 50;

        public int AmountRegistersPerPage
        { 
            get => amountRegistersPerPage;
            set 
            {
                amountRegistersPerPage = (value > amountRegistersPerPage) ? maxAmountRegistersPerPage:value ;
            }
        }
    }
}
