using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Geomethod.Web
{
	class PostedImageLengthException : Exception
	{
		public PostedImageLengthException(string msg) : base(msg) { }
	}

	class PostedImageFormatException : Exception
	{
		public PostedImageFormatException(string msg) : base(msg) { }
	}

	public class PostedImage
	{
		FileUpload fileUpload;
		System.Drawing.Image image;
		string fileName;
		string ext;

		public FileUpload FileUpload { get { return fileUpload; } }
		public System.Drawing.Image Image { get { return image; } }
		public string FilePath { get { return fileUpload.FileName; } }
		public string FileName { get { return fileName + ext; } }
		public string FileNameWithoutExtension { get { return fileName; } }
		public string Extension { get { return ext; } }

		public PostedImage(FileUpload fileUpload, int maxStreamLength)
		{
			this.fileUpload = fileUpload;
			ReadImage(maxStreamLength);
		}

		void ReadImage(int maxStreamLength)
		{
			string filePath = fileUpload.FileName;
			if (filePath.Trim().Length == 0) throw new PostedImageFormatException("Empty FileName.");

			int streamLength = (int)fileUpload.PostedFile.InputStream.Length;
			if (streamLength >= maxStreamLength)
				throw new PostedImageLengthException(string.Format("Picture size should be less than {0}Kb.", maxStreamLength / 1000));
			image = new Bitmap(fileUpload.PostedFile.InputStream);
			fileName = Path.GetFileNameWithoutExtension(filePath);
			ext = Path.GetExtension(filePath).ToLower();
		}

		public System.Drawing.Image FitImage(Size bounds)
		{
			Size size=image.Size;
			if (image.Width > bounds.Width || image.Height > bounds.Height)
			{
				double kx = ((double)bounds.Width) / image.Width;
				double ky = ((double)bounds.Height) / image.Height;
				double k=Math.Min(kx,ky);
				size.Width =(int) (k * size.Width);
				size.Height =(int) (k * size.Height);
			}
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(image, size);
			return bitmap;
		}
	}
}
