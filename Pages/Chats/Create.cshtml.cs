using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChatPDFCom.Data;
using ChatPDFCom.Models;

namespace ChatPDFCom.Pages.Chats
{
    public class CreateModel : PageModel
    {
        private readonly ChatPDFCom.Data.ChatPDFComContext _context;

        public CreateModel(ChatPDFCom.Data.ChatPDFComContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Chat Chat { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Chat.Add(Chat);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
