using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Hospital.Infrastructure
{
    public class ImagePathGetter
    {
        public string GetImageStringPath(HttpPostedFileBase postedFile)
        {
            StringBuilder stringBuilder = new StringBuilder
                (Path.GetFileNameWithoutExtension(postedFile.FileName));
            stringBuilder.Append(DateTime.Now.ToString("yymmssff") +
                Path.GetExtension(postedFile.FileName));
            return stringBuilder.ToString();
        }
    }
}