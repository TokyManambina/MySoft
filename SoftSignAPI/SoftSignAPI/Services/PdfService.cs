using System.Reflection.PortableExecutable;
using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using SoftSignAPI.Model;
using PdfSharp.Drawing;
using System.Drawing;
using System.Collections;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;
using PdfSharp.Quality;

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

			List<UserDocument> RecipientList = new List<UserDocument>();

			if (document.Type == DocumentType.WithoutFlow)
				RecipientList = document.UserDocuments.Where(x => x.Step == 0).ToList();
			else
				RecipientList = document.UserDocuments.Where(x => x.Step != 0).OrderBy(x => x.Step).ToList();

			foreach (var recipient in RecipientList)
			{
				if (!recipient.IsFinished)
					tasks.Add(CreateBox(pdf, recipient));
				else
					tasks.Add(BuildField(pdf, recipient));
				//tasks.Add(BuildField(pdf, recipient));
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
				tasks.Add(DrawBox(pdf, field, color));
            }

			await Task.WhenAll(tasks);
		}

		private async Task BuildField(PdfDocument pdf, UserDocument recipient)
		{
			PdfPage page = new PdfPage();

			var color = ColorTranslator.FromHtml(recipient.Color!);

			List<Task> tasks = new List<Task>();

			XImage xImageSign = null;
			XImage xImageParaphe = null;

			if (recipient.Signature != null)
			{
				xImageSign = await ByteToXImage(recipient.Signature);
			}
			if (recipient.Paraphe != null)
			{
				xImageParaphe = await ByteToXImage(recipient.Paraphe);
			}

			foreach (var field in recipient.Fields)
			{
				if (field.FieldType == FieldType.Signature)
					tasks.Add(DrawField(pdf, field, color, xImageSign));
				else if(field.FieldType == FieldType.Paraphe)
					tasks.Add(DrawField(pdf, field, color, xImageParaphe));

			}

			await Task.WhenAll(tasks);
		}

		private async Task<XImage> ByteToXImage(byte[] imageByte)
		{
			using (MemoryStream stream = new MemoryStream(imageByte, 0, imageByte.Length, true, true))
			{
				try
				{
					return XImage.FromStream(stream);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error converting byte array to XImage: " + ex.Message);
					return null;
				}
			}
		}
		private async Task DrawField(PdfDocument pdf, Field field, Color color, XImage image)
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
					gfx.DrawImage(image, x, y, width, height );
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
					XPen borderPen = new XPen(XColors.Black, 1)
					{
						DashStyle = XDashStyle.Dash,
						Color = XColor.FromArgb(255, color.R, color.G, color.B),
						Width = 2
					};
					XSolidBrush brush = new XSolidBrush(XColor.FromArgb(50, 100, 100, 100));
					

					gfx.DrawRectangle(brush, rectangle);
					gfx.DrawRectangle(borderPen, rectangle);

					gfx.Dispose();
				}catch(Exception ex)
				{
					var message = ex.ToString();
				}
			}
			return;
		}
	}
}