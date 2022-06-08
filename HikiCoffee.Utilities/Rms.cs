using System.Runtime.InteropServices;
using System.Text;

namespace HikiCoffee.Utilities
{
    public class Rms
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        public static string Read(string section, string key, string def)
        {
            try
            {
                string re = GetPath(Directory.GetCurrentDirectory());
                re = re.Replace("/", "\\");


                StringBuilder sb = new StringBuilder(1024);
                GetPrivateProfileString(section, key, def, sb, 1024, re + "\\HikiCoffee.AppManager\\Data\\DataConfig.ini");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Rms.writeLog(ex.Message);
                return "";
            }
        }


        public static int Write(string section, string key, string value)
        {
            try
            {
                string re = GetPath(Directory.GetCurrentDirectory());
                re = re.Replace("/", "\\");

                return WritePrivateProfileString(section, key, value, re + "\\HikiCoffee.AppManager\\Data\\DataConfig.ini");
            }
            catch (Exception ex)
            {
                Rms.writeLog(ex.Message);
                return 0;
            }
        }

        public static void writeLog(string sLog)
        {
            try
            {
                string time = DateTime.Now.ToString("h:mm:ss tt");
                sLog = time + "---" + sLog;

                string re = GetPath(Directory.GetCurrentDirectory());
                re = re.Replace("/", "\\");
                string path = re + "\\HikiCoffee.Utilities\\log.txt";

                if (File.Exists(path))
                {
                    using (FileStream aFile = new FileStream(path, FileMode.Append, FileAccess.Write))
                    using (StreamWriter sw = new StreamWriter(aFile))
                    {
                        sw.WriteLine(sLog);
                    }
                }
                else
                {
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(sLog);
                        fs.Write(info, 0, info.Length);
                        byte[] data = new byte[] { 0x0 };
                        fs.Write(data, 0, data.Length);
                    }
                }
            }
            catch
            {

            }
        }

        public static string GetPath(string value)
        {
            int index = value.IndexOf("HikiCoffee.AppManager") + 21;

            return value.Substring(0, index);
        }
    }
}
