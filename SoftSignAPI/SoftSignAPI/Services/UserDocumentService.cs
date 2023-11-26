using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserDocumentService(IUserRepository userRepository, IUserDocumentRepository userDocumentRepository, IDocumentRepository documentRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userDocumentRepository = userDocumentRepository;
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDocument>> CreateUserDocument(Document document, User user, List<DocumentRecipientsDto> ListRecipient)
        {
            try
            {
				int step = 1;

				List<UserDocument> recipients = new List<UserDocument>();

				UserDocument recipient;
				User userDoc;
				foreach (var item in ListRecipient)
				{
					recipient = new UserDocument();
                    recipient.Color = item.Color;
                    recipient.Message = item.Message;
					recipient.Document = document;
					recipient.MyTurn = step == 1;
					recipient.Step = step++;
                    recipient.Fields = _mapper.Map<List<Field>>(item.Fields);

					userDoc = new User();

					if (!await _userRepository.IsExist(item.Mail))
						userDoc = (await _userRepository.Insert(item.Mail, "123"))!;
					else
						userDoc = (await _userRepository.GetByMail(item.Mail))!;

					if (userDoc == null)
						return null;

                    recipient.User = userDoc;

                    recipients.Add(recipient);
				}

                recipient = new UserDocument()
                {
                    Document = document,
                    Step = 0,
                    User = user,
                    MyTurn = false,
                    IsFinished = true
                };
                recipients.Add(recipient);

				return recipients;
			}
			catch (Exception ex)
            {
                return null;
            }
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
