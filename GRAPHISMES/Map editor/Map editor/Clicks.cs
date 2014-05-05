using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Map_editor
{
    [Serializable]
    public class Elements
    {
        bool spawn;
        Point indexSpawn;
        FileStream file;
        StreamWriter stw;
        StreamReader str;
        element[,] elements;
        public Elements()
        {
            spawn = false;
            elements = new element[40,40];
            for (int i = 0; i < 40; i++)
                for (int j = 0; j < 40; j++)
                    elements[j, i] = element.blanc;
        }
        public element[,] getElements()
        {
            return elements;
        }
        public void Add(int x, int y, element type)
        {
            elements[x, y] = type;
        }
        public void Save(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            stw = new StreamWriter(file);
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                    stw.Write((int)elements[j, i]);
                stw.WriteLine();
            }
            stw.Flush();
            file.Close();
        }
        public void load(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            str = new StreamReader(file);
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                    elements[j, i] = (element)((int)str.Read()-48);
                str.ReadLine();
            }
            file.Close();
        } 
    }
}
