using System.ComponentModel.DataAnnotations;

namespace MyScriptureJournal.Models
{
    public class JournalEntry
    {
        public int ID { get; set; }
        public string Book { get; set; } = string.Empty;
        public string Chapter { get; set; } = string.Empty;
        public string Verses { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        [DataType(DataType.Date), Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;


    }
}
