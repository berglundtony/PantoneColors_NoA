namespace PantoneColors_NoA.Models.ViewModels
{
    public class PantoneColors
    {
        public List<PantoneColor> Colors { get; set; }
        public List<PantoneColor> Group1 { get; set; }
        public List<PantoneColor> Group2 { get; set; }
        public List<PantoneColor> Group3 { get; set; }
        public PantoneColors()
        {
            Colors = new List<PantoneColor>();
            Group1 = new List<PantoneColor>();
            Group2 = new List<PantoneColor>();
            Group3 = new List<PantoneColor>();
        }
    }

    public class PantoneColor
    {
        public int id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public string color { get; set; }
        public string pantone_value { get; set; }
        public string group { get; set; }
    }
}
