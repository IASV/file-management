using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prog_III_2020_2_sesion_1
{
    public class GestionArchivo
    {
        public string Path { get; set; }

        public GestionArchivo(string path)
        {
            Path = path;
        }

        public GestionArchivo()
        {
     
        }

        public void Save(string Line)
        {
            StreamWriter writer = new StreamWriter(Path, true);

            writer.WriteLine(Line);

            writer.Close();
        }

        public void Delete(object data)
        {
            using (StreamWriter fileWrite = new StreamWriter("Files/temp.txt", true))
            {
                using (StreamReader fielRead = new StreamReader(Path))
                {
                    String line;

                    while ((line = fielRead.ReadLine()) != null)
                    {
                        string[] datos = line.Split(new char[] { ',' });
                        string[] dateValues = (data.ToString()).Split('\t');
                        if (datos[0].ToString() != dateValues[0].ToString())
                        {
                            fileWrite.WriteLine(line);
                        }

                    }
                }
            }

            //aqui se renombrea el archivo temporal
            File.Delete(Path);
            File.Move("Files/temp.txt", Path);
        }

        public void Edit(int indexLine, int i, object data)
        {
            string[] All = File.ReadAllLines(Path);
            string[] Lines = (All[indexLine]).Split(',');
            string[] date = (data.ToString()).Split('\t');

            /*int calificacion = Convert.ToInt32(date[12]);
            date[12] = calificacion.ToString();*/

            Lines[i] = date[i];
            string dataText = "";
            for (int j = 0; j < Lines.Length; j++)
            {
                if (Lines[j] != "\n")
                    dataText += Lines[j];
                if (j < Lines.Length - 1)
                    dataText += ",";
            }

            All[indexLine] = dataText;

            File.WriteAllLines(Path, All);
        }

        public void Edit(int indexLine, object data)
        {
            string[] All = File.ReadAllLines(Path);
            All[indexLine] = data.ToString();

            File.WriteAllLines(Path, All);
        }

        public void LoadList(List<object> Lista)
        {
            //Lista = new List<>();
            if (File.Exists(Path))
            {
                StreamReader reader = new StreamReader(Path);

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');

                    Vendedor v = new Vendedor();
                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    Lista.Add(v);

                }

                reader.Close();
            }
        }


    }
}
