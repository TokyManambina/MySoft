using SoftSignAPI.Dto;
using SoftSignAPI.Helpers;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public class UserDocumentService : IUserDocumentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly IDocumentRepository _documentRepository;

        public UserDocumentService(IUserRepository userRepository, IUserDocumentRepository userDocumentRepository, IDocumentRepository documentRepository)
        {
            _userRepository = userRepository;
            _userDocumentRepository = userDocumentRepository;
            _documentRepository = documentRepository;
        }

        public async Task LinkUserWithDocument(string code, List<UserRoleDocumentDto> users)
        {
            var tasks = new List<Task>();
            foreach (var u in users.OrderBy(x=>x.Step))
            {
                tasks.Add(LinkUser(code, u));
            }
            await Task.WhenAll(tasks);
        }
        private async Task LinkUser(string code, UserRoleDocumentDto u)
        {
            string password;
            User? user = await _userRepository.GetByMail(u.Mail);
            try 
            { 
                if(user != null)
                {
                    await _userDocumentRepository.Create(new UserDocument
                    {
                        DocumentCode = code,
                        UserId = user.Id,
                        Role = u.Role,
                        Step = u.Step,
                    });
                }
                else
                {
                    password = Tools.RandomPassword(u.Mail);

                    user = await _userRepository.Create(new User
                    {
                        Email = u.Mail,
                        Password = BCrypt.Net.BCrypt.HashPassword(password),
                    });

                    await _userDocumentRepository.Create(new UserDocument
                    {
                        DocumentCode = code,
                        UserId = user.Id,
                        Role = u.Role,
                        Step = u.Step,
                    });
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
