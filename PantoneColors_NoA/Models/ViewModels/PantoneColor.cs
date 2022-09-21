namespace PantoneColors_NoA.Models.ViewModels
{
    public class PantoneColors
    {
        public List<PantoneColor> Colors { get; set; }
        public PantoneColors()
        {
            Colors = new List<PantoneColor>();
        }
    }

    public class PantoneColor
    {
        public int id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public string color { get; set; }
        public string pantone_value { get; set; }
    }
}
