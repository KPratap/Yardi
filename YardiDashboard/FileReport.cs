using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YardiDashboard
{
    public class FileReport
    {
        public FileReport()
        {

        }

        public IEnumerable<FileInfo> GetFiles(string rootDir, string pattern)
        {
            var fileInfos = new List<FileInfo>();
            var diTop = new DirectoryInfo(rootDir);
            foreach (var di in diTop.EnumerateDirectories("*"))
            {
                var difiles = di.EnumerateFiles(pattern, SearchOption.AllDirectories);
                foreach (var fi in di.EnumerateFiles(pattern, SearchOption.AllDirectories))
                {
                   // Console.WriteLine("{0}\t\t{1}", fi.FullName, fi.Length.ToString("N0"));
                    fileInfos.Add(fi);
                }
            }
            return fileInfos;
        }
    }
}
