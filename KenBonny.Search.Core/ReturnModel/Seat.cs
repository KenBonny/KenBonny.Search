namespace KenBonny.Search.Core.ReturnModel
{
    public class Seat
    {
        public Seat(string restaurant, int sectionId, int tableId)
        {
            Restaurant = restaurant;
            SectionId = sectionId;
            TableId = tableId;
        }

        public int TableId { get; }

        public int SectionId { get; }

        public string Restaurant { get; }
    }
}