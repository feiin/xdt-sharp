using System;
using Microsoft.Web.XmlTransform;
using System.IO;

namespace Xdt
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length != 3) {
				Console.Error.WriteLine ("error args: mono xdt [path]/Web.config [path]/Web.[Tramsform].config [output]/Web.config");
				return;
			}
			var originalFileXml = new System.Xml.XmlDocument();
			originalFileXml.Load(args[0]);
 			var currentDirectory = System.Environment.CurrentDirectory;
			using (var xmlTransform = new XmlTransformation(args[1]))
			{
				if (xmlTransform.Apply (originalFileXml) == true) {
					var savePath = args [2];
					if (!System.IO.Path.IsPathRooted (savePath)) {
						savePath = Path.Combine (currentDirectory, savePath);
					}

					originalFileXml.Save (savePath);
					Console.WriteLine ("transform success:" + savePath);
				} else {
					Console.Error.WriteLine ("transform failed.");
				}
			}
		}
	}
}
