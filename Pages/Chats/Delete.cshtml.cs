using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChatPDFCom.Data;
using ChatPDFCom.Models;

namespace ChatPDFCom.Pages.Chats
{
    public class DeleteModel : PageModel
    {
        private readonly ChatPDFCom.Data.ChatPDFComContext _context;

        public DeleteModel(ChatPDFCom.Data.ChatPDFComContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Chat Chat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chat.FirstOrDefaultAsync(m => m.Id == id);

            if (chat == null)
            {
                return NotFound();
            }
            else
            {
                Chat = chat;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chat.FindAsync(id);
            if (chat != null)
            {
                Chat = chat;
                _context.Chat.Remove(Chat);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
