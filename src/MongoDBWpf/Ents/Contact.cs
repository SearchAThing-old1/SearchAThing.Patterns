using SearchAThing.MongoDB;

namespace SearchAThing.Patterns.MongoDBWpf.Ents
{

    public class Contact : MongoEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }

}
