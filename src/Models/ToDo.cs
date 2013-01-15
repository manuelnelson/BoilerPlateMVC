using Application.Models.Contract;
using ServiceStack.DataAnnotations;

namespace Application.Models
{
    [Alias("Todos")]
    public class ToDo : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Task { get; set; }
        public bool Completed { get; set; }
    }
}
