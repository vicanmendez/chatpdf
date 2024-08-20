using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatPDFCom.Models;

namespace ChatPDFCom.Data
{
    public class ChatPDFComContext : DbContext
    {
        public ChatPDFComContext (DbContextOptions<ChatPDFComContext> options)
            : base(options)
        {
        }

        public DbSet<ChatPDFCom.Models.Chat> Chat { get; set; } = default!;
    }
}
