using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChatPDFCom.Data;
using ChatPDFCom.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatPDFCom.Pages.Chats
{
    public class IndexModel : PageModel
    {
        private readonly ChatPDFCom.Data.ChatPDFComContext _context;

        public IndexModel(ChatPDFCom.Data.ChatPDFComContext context)
        {
            _context = context;
        }

        public IList<Chat> Chat { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Searchtitle { get; set; }

        public async Task OnGetAsync()
        {
            // Iniciar una consulta sobre el contexto de Chat
            var chats = from c in _context.Chat
                        select c;

            // Aplicar filtro basado en el SearchString si no está vacío
            if (!string.IsNullOrEmpty(SearchString))
            {
                chats = chats.Where(s => s.PdfTitle.Contains(SearchString));
            }

            // Obtener la lista filtrada de chats
            Chat = await chats.ToListAsync();
        }
    }
}
