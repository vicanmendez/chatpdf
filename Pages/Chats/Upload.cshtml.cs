using ChatPDFCom.Data;
using ChatPDFCom.Models;
using ChatPDFCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChatPDFCom.Pages.Chats
{
    public class UploadModel : PageModel
    {
        private readonly ChatPDFService _chatPDFService;
        private readonly IConfiguration _configuration;
        private readonly ChatPDFComContext _dbContext;

        public UploadModel(ChatPDFService chatPDFService, ChatPDFComContext context)
        {
            _chatPDFService = chatPDFService;
            _dbContext = context;
        }

        [BindProperty]
        public IFormFile PdfFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (PdfFile != null && PdfFile.Length > 0)
            {
                using (var stream = PdfFile.OpenReadStream())
                {
                    var sourceId = await _chatPDFService.UploadPdfAsync(stream, PdfFile.FileName);

                    var chat = new Chat
                    {
                        PdfTitle = PdfFile.FileName,
                        SourceId = sourceId,
                        CreatedAt = DateTime.Now
                    };

                    _dbContext.Chat.Add(chat);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToPage("Chat", new { id = chat.Id });
                }
            }

            return Page();
        }
    }
}
