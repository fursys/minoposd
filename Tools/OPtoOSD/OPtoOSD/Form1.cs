using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace OPtoOSD
{
    public partial class Form1 : Form
    {
        List<string[]> files = new List<string[]> ();

        //string [][] files = new String []
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader("FilesList.txt");
                String [] line = new String [5];
                int tab;
                string tabstr = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split (';');
                    files.Add(line);
                    tab = int.Parse(line[1]);
                    while (tab-->0)
                    {
                        tabstr += (char)9;
                    }
                    lstFiles.Items.Add(line[0] + tabstr + line[2]);
                    tabstr = "";
                    //Console.WriteLine(line);
                    // LocalPing(line);
                }
                sr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //WriteLogFile("The file could not be read:");
                //WriteLogFile(e.Message);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            foreach (string[] i in files)
            {


                try
                {
                    StreamReader sr = new StreamReader( txtFolder.Text + '\\' + i[0]);
                    String line;
                    int id_index;
                    bool exit = false;
                    while ((!sr.EndOfStream) && (!exit))
                    {
                        line = sr.ReadLine();
                        if (line.IndexOf ("define " + i[2]) > 0)
                        {
                            id_index = line.IndexOf("0x");
                            if (id_index>0)
                            {
                                i[3] = line.Substring(id_index, 10);
                                exit = true;
                            }
                            //richTextBox1.AppendText(line + (char) 10);
                        }

                        //Console.WriteLine(line);
                        // LocalPing(line);
                    }
                    sr.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //WriteLogFile("The file could not be read:");
                    //WriteLogFile(e.Message);
                }
            }





            try
            {
                StreamReader sr = new StreamReader(txtOSDfolder.Text);
                String line;
                int id_index;
                int curr_obj = 0;
                bool exit = false;
                while ((!sr.EndOfStream) && (!exit))
                {
                    line = sr.ReadLine();
                    if (line.IndexOf(files [curr_obj][2]) > 0)
                    {
                        id_index = line.IndexOf("0x");
                        if (id_index > 0)
                        {
                            files[curr_obj][4] = line.Substring(id_index, 10);
                        }
                        curr_obj++;
                        if (files.Count < curr_obj) exit = true;
                        //richTextBox1.AppendText(line + (char) 10);
                    }

                    //Console.WriteLine(line);
                    // LocalPing(line);
                }
                sr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //WriteLogFile("The file could not be read:");
                //WriteLogFile(e.Message);
            }

            foreach (string[] i in files)
            {
                richTextBox1.AppendText("#define " + i[2] + " " + i[3] + " //" + i[4]+  (char) 10);
            }



        }
    }
}
