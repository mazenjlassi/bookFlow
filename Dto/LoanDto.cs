namespace bookFlow.Dto
{
    public class LoanDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
    }
}
