
/// <summary>
/// reusable entities for repositories
/// </summary>
namespace CoreSB.Universal
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EntityGuidIdDAL : IEntityGuidIdDAL
    {
        public Guid Id { get; set; }
    }
    public class EntityIntIdDAL : IEntityIntIdDAL
    {
        [Key]
        public int Id { get; set; }
    }
    public class EntityStringIdDAL : IEntityStringIdDAL
    {
        public string Id { get; set; }
    }


    public class EntityDateDAL : IDateEntityDAL
    {
        public DateTime Date { get; set; }
    }
    public class EntityDateRangeDAL : IDateRangeEntityDAL
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class CoreSBEntityDAL : IMongoDAL
    {
        public string? Id { get; set; }
    }
}
