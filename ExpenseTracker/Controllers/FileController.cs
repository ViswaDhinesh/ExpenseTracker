using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class FilesController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        public void File_Rename()
        {
            //string FirstExistingName = ".Table";
            //string FirstReplaceName = "";
            //string SecondExistingName = ".Table";
            //string SecondReplaceName = "";
            //string FilePath = @"C:\Users\dhine\OneDrive\Desktop\WFH\BBJ\Local_DB_Scripts\Tables";

            string FirstExistingName = ".StoredProcedure";
            string FirstReplaceName = "";
            string SecondExistingName = "dbo.";
            string SecondReplaceName = "";
            string FilePath = @"C:\Users\dhine\OneDrive\Desktop\WFH\BBJ\Local_DB_Scripts\Stored Procedures";


            DirectoryInfo d = new DirectoryInfo(FilePath);
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                // -- Original Function Static
                //System.IO.File.Move(f.FullName, f.FullName.Replace(".Table", "").Replace("dbo.", ""));

                // -- Original Function Dynamic
                System.IO.File.Move(f.FullName, f.FullName.Replace(FirstExistingName, FirstReplaceName).Replace(SecondExistingName, SecondReplaceName));

                //File.Move(f.FullName, f.FullName.Replace("abc_", ""));
                //f.Name.Replace(f.Name, f.Name.Replace(".Table", "").Replace("dbo.", ""));
                //string Fromfile = path + "FileLength.txt";
                //string Tofile = path + "FileLength1.txt";
                //File.Move(Fromfile, Tofile);
            }

            //return true;
        }
    }
}