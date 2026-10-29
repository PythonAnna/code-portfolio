using System.Text.Json.Serialization;

namespace Act2Keeper.Models
{
    public enum ActionType
    {
        Introduce = 1,
        OfferHelp = 2,
        Threaten = 3
    }
    public class VisitorAtionsHistory
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public ActionType Action { get; set; }
        [JsonIgnore]
        public Visitor Visitor { get; set; }
    }
}
