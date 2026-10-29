using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace Act2Keeper.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LevelOfTrust { get; set; } = 0;
        public List<VisitorAtionsHistory> History { get; set; } = new();
    }
}
