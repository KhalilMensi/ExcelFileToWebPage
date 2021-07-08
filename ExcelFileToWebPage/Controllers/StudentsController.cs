using ExcelDataReader;
using ExcelFileToWebPage.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelFileToWebPage.Controllers
{
	public class StudentsController : Controller
	{
		[HttpGet]
		public IActionResult Index(List<Student> students = null)
		{
			students = students == null ? new List<Student>() : students;
			ViewBag.FileName = "Choose file";
			return View(students);
		}

		[HttpPost]
		public IActionResult Index(IFormFile file,[FromServices] IWebHostEnvironment hostingEnvironment,string FileName)
		{
			if (file != null)
			{
				string filename = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
				using (FileStream fileStream = System.IO.File.Create(filename))
				{
					file.CopyTo(fileStream);
					fileStream.Flush();
				}

				var students = this.GetStudentList(file.FileName);

				ViewBag.FileName = file.FileName;
				ViewBag.File = file;

				return View(students);
			}
			else if(file == null && FileName != "Choose file")
			{
				string filename = $"{hostingEnvironment.WebRootPath}\\files\\{FileName}";

				var students = this.GetStudentList(FileName);

				ViewBag.FileName = FileName;

				return View(students);
			}
			else
			{
				ViewBag.FileName = "Choose file";
				return View(new List<Student>());
			}
		}

		private List<Student> GetStudentList(string fName)
		{
			List<Student> students = new List<Student>();
			var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;

			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			using (var stream = System.IO.File.Open(fileName,FileMode.Open,FileAccess.Read))
			{
				 using(var reader = ExcelReaderFactory.CreateReader(stream))
				{
					while (reader.Read())
					{
						students.Add(new Student()
						{
						   Name = reader.GetValue(0).ToString(),
						   Email = reader.GetValue(1).ToString(),
						   Tel = reader.GetValue(2).ToString()
						});
					}
				}
			}
			return students;
		}
	}
}
