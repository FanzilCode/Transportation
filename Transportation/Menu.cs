using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Transportation
{
    class Menu
    {
        static List<IWork> work = new List<IWork>();
        public static void SaveToFile(string path) // сохранение в файл
        {
            string strings = "";
            foreach (var p in work)
            {
                strings += p + "\n";
            }
            strings = strings.Trim();

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(strings);
            }
        }
        public static void ReadOnFile (string path)
        {
            string line;

        }
    }
}
