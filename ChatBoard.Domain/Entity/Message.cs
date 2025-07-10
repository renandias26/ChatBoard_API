using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBoard.Domain.Entity
{
    public class Message
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public required string UserName { get; set; }
        public required int GroupId { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public required DateTimeOffset DateTime { get; set; }
    }
}
