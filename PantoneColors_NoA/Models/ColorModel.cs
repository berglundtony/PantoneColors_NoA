namespace PantoneColors_NoA.Models
{
    public class ColorModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public string color { get; set; }
        public string pantone_value { get; set; }
    }

    public class Root
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<ColorModel> data { get; set; }
        public Support support { get; set; }

        public Root(int page, int per_page, int total, int total_pages, List<ColorModel> data, Support support)
        {
            this.page = page;
            this.per_page = per_page;
            this.total = total;
            this.total_pages = total_pages;
            this.data = data;
            this.support = support;
        }

        public Root()
        {

        }

    }

    public class Support
    {
        public string url { get; set; }
        public string text { get; set; }
    }
}
