﻿using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KenloadV2API.Services
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        //public IFormFileCollection Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content /*,IFormFileCollection attachments*/)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x,x)));
            Subject = subject;
            Body = content;
            //Attachments = attachments;
        }
    }
}