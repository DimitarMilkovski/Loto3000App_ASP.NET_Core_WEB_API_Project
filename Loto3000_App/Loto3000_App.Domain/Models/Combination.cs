namespace Loto3000_App.Domain
{
    public class Combination: BaseEntity
    {
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; } 
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
    }
}