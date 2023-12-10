using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
	public interface IPdfService
	{
		Task<MemoryStream?> GeneratePDF(Document document);
	}
}
