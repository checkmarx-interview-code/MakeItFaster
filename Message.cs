using System;
using System.Collections.Generic;
using System.Text;

namespace MakeItFaster
{
    public class Message
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
