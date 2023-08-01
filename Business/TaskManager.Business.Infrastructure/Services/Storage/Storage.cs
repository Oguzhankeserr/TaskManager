﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManager.Business.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected async Task<string> FileRenameAsync(string path, string fileName, Func<string, string, bool> hasFile)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            Regex regex = new Regex("[*'\",+-._&#^@|/<>~]");
            string newFileName = regex.Replace(oldName, string.Empty);
            DateTime datetimenow = DateTime.UtcNow;
            string datetimeutcnow = datetimenow.ToString("yyyyMMddHHmmss");
            string fullName = $"{datetimeutcnow}-{newFileName}{extension}";

            return fullName;
        }
    }
}
