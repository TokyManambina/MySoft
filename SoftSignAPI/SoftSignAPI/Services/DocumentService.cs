using Microsoft.AspNetCore.Http;
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
        public Document? CreateDocument(IFormFile upload, User user)
        {
            try
            {

                var uploadFile = upload.FileName.Replace(" ", "_");
                string filename = Path.GetFileNameWithoutExtension(uploadFile);

                Document? document = new Document();

                var date = DateTime.Now;
                document.DateSend = date;
                document.Code = $"{Convert.ToHexString(BitConverter.GetBytes(date.Ticks))}-{date.ToString("yyyyMM")}";

                var Location = user.Subscription!.Location!;
                if (Location == null)
                    return null;

                document.Filename = $"{date.ToString("yyyyMMddhhmmss-")}{filename}{Path.GetExtension(uploadFile)}";

			    CreateDirectory(Location);
			    document.Url = Path.Combine(Location, document.Filename);
            

                CreateFile(upload, document.Url);
                CreateFile(upload, Path.Combine(Location, "(_original_)" + document.Filename));

                //document = await _documentRepository.Create(document);
                return document;

			}catch(Exception ex)
            {
                throw new Exception();
            }

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

        public async Task GetDocumentFile(string code)
        {
            var document = await _documentRepository.Get(code);

            if (document == null) return;

            var url = document.Url;
			if (!File.Exists(document.Url))
			{
				return ;
			}

			byte[] fileBytes = System.IO.File.ReadAllBytes(url);


            string contentType = "application/octet-stream";
			if (Path.GetExtension(url).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
				contentType = "application/pdf";
            return;
            //return File(fileBytes, contentType, Path.GetFileName(url));
		}
    }
}
