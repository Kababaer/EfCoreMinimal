using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Minimal
{
    public class Document
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
