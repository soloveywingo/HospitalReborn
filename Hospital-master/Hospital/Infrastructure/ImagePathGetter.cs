using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Hospital.Infrastructure
{
    public class ImageWorker
    {
        public string GetImageStringPath(HttpPostedFileBase postedFile)
        {
            if (postedFile == null)
            {
                return "default.png";
            }
            StringBuilder stringBuilder = new StringBuilder
                (Path.GetFileNameWithoutExtension(postedFile.FileName));
            stringBuilder.Append(DateTime.Now.ToString("yymmssff") +
                Path.GetExtension(postedFile.FileName));
            return stringBuilder.ToString();
        }

    }
}