using LiteDB;

namespace MetBench_Domain
{
    //Domain
    public class Domain : IEquatable<Domain>
    {
        [BsonId]
        public int IdDomain { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public bool Equals(Domain domain)
        {

            return IdDomain == domain.IdDomain;
        }


    }
}
