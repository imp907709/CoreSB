
/// <summary>
/// reusable entities for repositories
/// </summary>

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

    
    //mongo
    public class MongoDAL : IMongoDAL
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
