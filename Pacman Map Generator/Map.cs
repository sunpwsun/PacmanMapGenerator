using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman_Map_Generator
{
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();


            //string[] mapStr = new string[Form1.map.GetLength(1)];
            string mapStr ="";


            for ( int i = 0; i < Form1.map.GetLength(1); i++)
            {
                mapStr += "[";

                for( int j = 0; j < Form1.map.GetLength(0); j++)
                {
                    mapStr += Form1.map[j, i];

                    if (j != Form1.map.GetLength(0) - 1)
                        mapStr += ",";
                }

                mapStr += "],\n";
            }

            txtMap.Text = mapStr;



            string pathStr = "";


            for (int i = 0; i < Form1.path.GetLength(1); i++)
            {
                pathStr += "[";

                for (int j = 0; j < Form1.path.GetLength(0); j++)
                {
                    pathStr += Form1.path[j, i];

                    if (j != Form1.path.GetLength(0) - 1)
                        pathStr += ",";
                }

                pathStr += "],\n";
            }

            txtPath.Text = pathStr;




        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save";
            saveDialog.Filter = "Map Files (*.map)|*.map" + "|" +
                                "All Files (*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveDialog.FileName;

                using (StreamWriter sw = new StreamWriter(file))
                {
                    for( int i = 0; i< Form1.map.GetLength(1); i++ )
                    {
                        for (int j = 0; j < Form1.map.GetLength(0); j++)
                            sw.WriteLine(Form1.map[j, i]);
                    }

                    for (int i = 0; i < Form1.path.GetLength(1); i++)
                    {
                        for (int j = 0; j < Form1.path.GetLength(0); j++)
                            sw.WriteLine(Form1.path[j, i]);
                    }

                }
            }


        }
    }
}
