using System.Reflection.PortableExecutable;
using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace SoftSignAPI.Services
{
	public class PdfService : IPdfService
	{
		public PdfService()
		{
		}

		public void AddFieldIntoPDF(string url)
		{
			using (FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read))
			{
				// Create a MemoryStream to read the file content into
				using (MemoryStream memoryStream = new MemoryStream())
				{
					// Copy the file content to the MemoryStream
					fileStream.CopyTo(memoryStream);

					// At this point, 'memoryStream' contains the content of the file as a stream
					// You can use 'memoryStream' as needed
				}
			}
			PdfDocument pdf = PdfReader.Open(url, PdfDocumentOpenMode.Modify);
		}
	}
}