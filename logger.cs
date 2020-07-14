using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;


namespace BOM_Importer_V2
{
    public static class Log
    {
        private static ReaderWriterLockSlim lck = new ReaderWriterLockSlim();
        public static void Write(string str, bool append = true)
        {
            Console.WriteLine(Prompt() + str);

#if (DEBUG)
            string fileName = "app.log";
#endif

#if (!DEBUG)
            string fileName = Environment.ExpandEnvironmentVariables(@"%APPDATA%") + "\\albis-elcon\\BOM_Importer\\app.log";
#endif


            lck.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, append))
                {
                    writer.WriteLine(Prompt() + str);
                };
            }
            finally
            {
                lck.ExitWriteLock();
            }

        }

        private static string Prompt()
        {
            return TimeStamp() + " : ";

        }

        private static string TimeStamp()
        {
            return DateTime.Now.ToString("HH:mm:ss.ff");
        }
    }
}