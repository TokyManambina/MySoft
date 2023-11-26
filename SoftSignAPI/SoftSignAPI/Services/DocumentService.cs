﻿using Microsoft.AspNetCore.Http;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using System.Xml.Linq;

namespace SoftSignAPI.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ISocietyRepository _societyRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DocumentService(IDocumentRepository documentRepository, ISocietyRepository societyRepository, IWebHostEnvironment hostingEnvironment)
        {
            _documentRepository = documentRepository;
            _societyRepository = societyRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<Document?> CreateDocument(IFormFile upload, User user)
        {
            var uploadFile = upload.FileName.Replace(" ", "_");
            string filename = Path.GetFileNameWithoutExtension(uploadFile);

            Document? document = new Document();

            var date = DateTime.Now;
            document.DateSend = date;
            document.Code = $"{Convert.ToHexString(BitConverter.GetBytes(date.Ticks))}-{date.ToString("yyyyMM")}";

            /*var society = await _societyRepository.GetByUser(mail);
			if (society == null)
				return null;
            */

            document.Filename = $"{date.ToString("yyyyMMddhhmmss-")}{filename}{Path.GetExtension(uploadFile)}";

            //document.Url = society.Storage;
            document.Url = @"C:/Users/manam/OneDrive/Documents/docs/" + document.Filename;
            //CreateDirectory(document.Url);

            CreateFile(upload, document.Url);
            CreateFile(upload, "(_original_)" + document.Filename);

            document = await _documentRepository.Create(document);
            return document;
        }
        public async Task<Document?> BuildDocument(UploadFileDto upload, string mail)
        {
            var uploadFile = upload.File.FileName.Replace(" ", "_");
            string filename = Path.GetFileNameWithoutExtension(uploadFile);

            Document? document = new Document();

            var date = DateTime.Now;
            document.DateSend = date;
            document.Code = $"{Convert.ToHexString(BitConverter.GetBytes(date.Ticks))}-{date.ToString("yyyyMM")}";

            var society = await _societyRepository.GetByUser(mail);
            if (society == null)
                return null;


            document.Filename = $"{date.ToString("yyyyMMdd-")}{filename}.{Path.GetExtension(uploadFile)}";

            document.Url = society.Storage;
            CreateDirectory(document.Url);

            CreateFile(upload.File, document.Filename);
            CreateFile(upload.File, "(_original_)" + document.Filename);

            document = await _documentRepository.Create(document);
            return document;
        }

        public void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }

        public void CreateFile(IFormFile upload, string file)
        {
            try
            {
                var streamCopy = new FileStream(file, FileMode.Create);
                upload.CopyTo(streamCopy);
                streamCopy.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
