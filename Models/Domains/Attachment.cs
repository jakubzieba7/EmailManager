﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Domains
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
    }
}