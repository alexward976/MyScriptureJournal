using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.JournalEntries
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<JournalEntry> JournalEntry { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string ? SearchString { get; set; }
        public SelectList ? Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ? ScriptureBook { get; set; }

        public string BookSort { get; set; }
        public string DateSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {

            IQueryable<string> bookQuery = from e in _context.JournalEntry orderby e.Book select e.Book;

            var entries = from e in _context.JournalEntry select e;

            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            BookSort = sortOrder == "Book" ? "book_desc" : "Book";

            switch(sortOrder)
            {
                case "date_desc":
                    entries = entries.OrderByDescending(s => s.DateAdded); break;
                case "Book":
                    entries = entries.OrderBy(s => s.Book); break;
                case "book_desc":
                    entries = entries.OrderByDescending(s => s.Book); break;
                default:
                    entries = entries.OrderBy(s => s.DateAdded); break;
            }
            

            if (!string.IsNullOrEmpty(SearchString))
            {
                entries = entries.Where(s => s.Notes.Contains(SearchString));
            }

            if(!string.IsNullOrEmpty(ScriptureBook))
            {
                entries = entries.Where(x => x.Book ==  ScriptureBook);
            }

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            JournalEntry = await entries.ToListAsync();
        }
    }
}
