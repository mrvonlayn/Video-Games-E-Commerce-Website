﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Business.FileManagement.Abstract
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string existingFilePath = null);
        void DeleteImage(string imagePath);
    }
}
