using ChatPDFCom.Data;
using ChatPDFCom.Models;
using ChatPDFCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ChatPDFCom.Pages.Chats
{
    public class ChatModel : PageModel
    {
        private readonly ChatPDFService _chatPDFService;
        private readonly ChatPDFComContext  _context;

        public ChatModel(ChatPDFService chatPDFService, ChatPDFComContext context)
        {
            _chatPDFService = chatPDFService;
            _context = context;
        }

        public Chat Chat { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Chat = await _context.Chat.FindAsync(id);

            if (Chat == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, string message)
        {
            var chat = await _context.Chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            var response = await _chatPDFService.SendMessageAsync(chat.SourceId, message);

            chat.Conversation += $"\nUsuario: {message}\nChatPDF: {response}";
            await _context.SaveChangesAsync();

            Chat = chat;
            return Page();
        }
    }
}

