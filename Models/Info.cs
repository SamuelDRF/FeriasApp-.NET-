namespace FeriasApp.Models
{
    public class Info
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Requested { get; set; }
        public string Confirmed { get; set; }
        public int Days { get; set; }
        public string Zone { get; set; }

        public Info()
        {

        }
    }
}
