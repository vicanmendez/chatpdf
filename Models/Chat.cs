using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChatPDFCom.Models
{
    public class Chat
    {
        
        public int Id { get; set; }
        public string PdfTitle { get; set; }
        public string SourceId { get; set; }
        public string? Conversation { get; set; }
        public DateTime CreatedAt { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Searchtitle { get; set; }

 

    }

}
