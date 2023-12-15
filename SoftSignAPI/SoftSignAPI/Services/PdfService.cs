using System.Reflection.PortableExecutable;
using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using SoftSignAPI.Model;
using PdfSharp.Drawing;
using System.Drawing;

namespace SoftSignAPI.Services
{
	public class PdfService : IPdfService
	{
		public PdfService()
		{
		}
		
		private double PixelsToPoints(double pixels, int dpi)
		{
			const double pointsPerInch = 72.0;
			return (pixels / (double)dpi) * pointsPerInch;
			return 0.75 * pixels;
		}

		private async Task<MemoryStream?> CreatePDFCopy(string url)
		{
			try
			{
				FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);

				// Create a MemoryStream to read the file content into
				MemoryStream memoryStream = new MemoryStream();
					
				await fileStream.CopyToAsync(memoryStream);
				//fileStream.Close();
				fileStream.Close();
				return memoryStream;
			}
			catch(Exception ex)
			{
				return null;
			}
			
		}

		public async Task<MemoryStream?> GeneratePDF(Document document)
		{
			var file = await CreatePDFCopy(document.Url);

			if (file == null) return null;

			PdfDocument pdf = PdfReader.Open(file);

			List<Task> tasks = new List<Task>();

			var a = document.UserDocuments.Where(x => x.Step != 0).OrderBy(x => x.Step).ToList();
			foreach (var recipient in document.UserDocuments.Where(x=>x.Step!=0).OrderBy(x=>x.Step).ToList())
			{
				if (!recipient.IsFinished)
					tasks.Add(CreateBox(pdf, recipient));
				else
					tasks.Add(BuildField(pdf, recipient));
			}
			
			await Task.WhenAll(tasks);

			string filePath = "SoftSign.pdf";

			MemoryStream memoryStream = new MemoryStream();
			
			pdf.Save(memoryStream);
			
			pdf.Close();
			
			return memoryStream;
		}

		private async Task CreateBox(PdfDocument pdf, UserDocument recipient)
		{
			PdfPage page = new PdfPage();

			var color = ColorTranslator.FromHtml(recipient.Color!);

			List<Task> tasks = new List<Task>();
			
			foreach (var field in recipient.Fields)
			{
				tasks.Add(DrawField(pdf, field, color));
				//tasks.Add(DrawBox(pdf, field, color));
            }

			await Task.WhenAll(tasks);
		}

		private async Task BuildField(PdfDocument pdf, UserDocument recipient)
		{
			PdfPage page = new PdfPage();

			var color = ColorTranslator.FromHtml(recipient.Color!);

			List<Task> tasks = new List<Task>();

			foreach (var field in recipient.Fields)
			{
				tasks.Add(DrawField(pdf, field, color));
			}

			await Task.WhenAll(tasks);
		}
		private async Task DrawField(PdfDocument pdf, Field field, Color color)
		{
			PdfPage page = new PdfPage();

			for (int i = int.Parse(field.FirstPage) - 1; i <= int.Parse(field.LastPage) - 1; i++)
			{
				page = pdf.Pages[i];

				var scale = new
				{
					width = page.Width / field.PDF_Width,
					height = page.Height / field.PDF_Height
				};
				try
				{
					var x = PixelsToPoints(field.X!.Value * scale.width!.Value, 96);
					var y = PixelsToPoints(field.Y!.Value * scale.height!.Value, 96);
					var width = PixelsToPoints(field.Width!.Value * scale.width!.Value, 96);
					var height = PixelsToPoints(field.Height!.Value * scale.height!.Value, 96);
					FileStream fileStream = new FileStream("default/total.png", FileMode.Open);
					MemoryStream a = new MemoryStream();
					fileStream.CopyTo(a);
					fileStream.Close();
					XImage image = XImage.FromStream(a);
					fileStream.Close();
					XGraphics gfx = XGraphics.FromPdfPage(page);
					gfx.DrawImage(image, x, y, image.Width, image.Height);
					gfx.Dispose();
				}
				catch (Exception ex)
				{
					var message = ex.ToString();
				}
			}
			return;
		}

		private async Task DrawBox(PdfDocument pdf, Field field, Color color)
		{
			PdfPage page = new PdfPage();

			for (int i = int.Parse(field.FirstPage) - 1; i <= int.Parse(field.LastPage) - 1; i++)
			{
				page = pdf.Pages[i];

				var scale = new
				{
					width = page.Width / field.PDF_Width,
					height = page.Height / field.PDF_Height
				};
				try
				{
					var x = PixelsToPoints(field.X!.Value * scale.width!.Value, 96);
					var y = PixelsToPoints(field.Y!.Value * scale.height!.Value, 96);
					var width = PixelsToPoints(field.Width!.Value * scale.width!.Value, 96);
					var height = PixelsToPoints(field.Height!.Value * scale.height!.Value, 96);
					XGraphics gfx = XGraphics.FromPdfPage(page);
					XRect rectangle = new XRect(x,y, width, height);
					XSolidBrush brush = new XSolidBrush(XColor.FromArgb(75, color.R, color.G, color.B));
					gfx.DrawRectangle(brush, rectangle);
					gfx.Dispose();
				}catch(Exception ex)
				{
					var message = ex.ToString();
				}
			}
			return;
		}
		private object GetPage(string page)
		{
			var pages = page.Split('-');
			return new
			{
				FirstPage = int.Parse(pages.First().Trim()),
				LastPage = int.Parse(pages.Last().Trim())
			};
		}
	}
}