using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Repository.Mongo;

namespace SearchAThing.Patterns.MongoDBWpf.Ents
{

    public class Contact : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }

}
