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
        private readonly IUserRepository _userRepository;
        private readonly ISocietyRepository _societyRepository;

        public DocumentService(IDocumentRepository documentRepository, IUserRepository userRepository, ISocietyRepository societyRepository)
        {
            _documentRepository = documentRepository;
            _userRepository = userRepository;
            _societyRepository = societyRepository;
        }

        public async void BuildDocument(UploadFileDto upload, string mail)
        {
            var uploadFile = upload.File.FileName.Replace(" ", "_");
            string filename = Path.GetFileNameWithoutExtension(uploadFile);

            Document? document = new Document();

            var date = DateTime.Now;
            document.DateSend = date;
            document.Code = $"{Convert.ToHexString(BitConverter.GetBytes(date.Ticks))}-{date.ToString("yyyyMM")}";

            var society = await _societyRepository.GetByUser(mail);
            if (society == null)
                return;


            document.Filename = $"{date.ToString("yyyyMMdd-")}{filename}.{Path.GetExtension(uploadFile)}";

            document.Url = society.Storage;
            CreateDirectory(document.Url);

            CreateFile(upload.File, document.Filename);
            CreateFile(upload.File, "(_original_)"+document.Filename);

            document = await _documentRepository.Create(document);

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
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
