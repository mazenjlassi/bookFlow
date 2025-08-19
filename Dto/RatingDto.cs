namespace bookFlow.Dto
{
    public class RatingDto
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
