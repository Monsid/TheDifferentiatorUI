using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDifferenciatorUI
{
    //this differentiator works best with porous stones. stones that dont reflect and photos taken in the same session / lighing conditions.
    public partial class Form1 : Form
    {
        Rectangle rect;
        Bitmap imageForScanning;
        Point LocationXY;
        Point LocationX1Y1;

        bool isMouseDown = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            isMouseDown = true;
            LocationXY = e.Location;
        }

        public class NaturalComparer : Comparer<string>, IDisposable
        {
            private Dictionary<string, string[]> table;

            public NaturalComparer()
            {
                table = new Dictionary<string, string[]>();
            }

            public void Dispose()
            {
                table.Clear();
                table = null;
            }

            public override int Compare(string x, string y)
            {
                if (x == y)
                {
                    return 0;
                }
                string[] x1, y1;
                if (!table.TryGetValue(x, out x1))
                {
                    x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
                    table.Add(x, x1);
                }
                if (!table.TryGetValue(y, out y1))
                {
                    y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)");
                    table.Add(y, y1);
                }

                for (int i = 0; i < x1.Length && i < y1.Length; i++)
                {
                    if (x1[i] != y1[i])
                    {
                        return PartCompare(x1[i], y1[i]);
                    }
                }
                if (y1.Length > x1.Length)
                {
                    return 1;
                }
                else if (x1.Length > y1.Length)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            private static int PartCompare(string left, string right)
            {
                int x, y;
                if (!int.TryParse(left, out x))
                {
                    return left.CompareTo(right);
                }

                if (!int.TryParse(right, out y))
                {
                    return left.CompareTo(right);
                }

                return x.CompareTo(y);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                LocationX1Y1 = e.Location;
                Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                LocationX1Y1 = e.Location;
                isMouseDown = false;
            }

            if (rect != null)
            {
                Bitmap bit = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                Bitmap cropImg = new Bitmap(rect.Width, rect.Height);
                Graphics g = Graphics.FromImage(cropImg);
                g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel);
                pictureBox2.Image = cropImg;
                imageForScanning = cropImg;
            }
        }
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (rect != null)
            {
                e.Graphics.DrawRectangle(Pens.Green, GetRect());
            }
        }
        public Rectangle GetXY()
        {
            rect = new Rectangle
            {
                X = Math.Min(LocationXY.X, LocationX1Y1.X) * 10,

                Y = Math.Min(LocationXY.Y, LocationX1Y1.Y) * 10,

                Width = Math.Abs(LocationXY.X - LocationX1Y1.X) * 10,

                Height = Math.Abs(LocationXY.Y - LocationX1Y1.Y) * 10
            };

            return rect;
        }
        public Rectangle GetRect()
        {
            rect = new Rectangle
            {
                X = Math.Min(LocationXY.X, LocationX1Y1.X),

                Y = Math.Min(LocationXY.Y, LocationX1Y1.Y),

                Width = Math.Abs(LocationXY.X - LocationX1Y1.X),

                Height = Math.Abs(LocationXY.Y - LocationX1Y1.Y)
            };

            return rect;
        }
        public int imageCount = 0;
        List<string> imagesInFolder = new List<string>();


        private void FolderBrowse_Click(object sender, EventArgs e)
        {
            imageCount = 0;

            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;


                var files = Directory.EnumerateFiles(folderDlg.SelectedPath, "*.jpg").OrderBy(filename => filename);

                imagesInFolder = files.ToList<string>();
                using (NaturalComparer comparer = new NaturalComparer())
                {
                    imagesInFolder.Sort(comparer);
                }
            }
            pictureBox1.Image = new Bitmap(imagesInFolder[0]);
        }
        private void ProduceCSVMethod()
        {
            float pixelBrightness = float.Parse("0.4");
            //each collumn should be a list -- this is to avoid using CSV helper and have to make objects.
            List<string> csvList = new List<string>();

            //write all resullt to csv by appending the lines formatted e.g. filename.jpg, Wet

            //loop through folder filenames and check all

            int count = 0;

            foreach (string img in imagesInFolder)
            {
                using (Bitmap realimg = new Bitmap(img))
                {
                    //float ratioX = rect.X / pictureBox1.ClientSize.Width;
                    //float ratioY = rect.Y / pictureBox1.ClientSize.Height;

                    float percentWidth = (float)rect.Width / (float)pictureBox1.ClientSize.Width;
                    float actualWidth = (float)realimg.Width / 100 * (percentWidth * 100);

                    float percentHeight = (float)rect.Height / (float)pictureBox1.ClientSize.Height;
                    float actualHeight = (float)realimg.Height / 100 * (percentHeight * 100);

                    float percentX = (float)rect.X / (float)pictureBox1.ClientSize.Width;
                    float actualX = (float)realimg.Width / 100 * (percentX * 100);

                    float percentY = (float)rect.Y / (float)pictureBox1.ClientSize.Height;
                    float actualY = (float)realimg.Height / 100 * (percentY * 100);

                    Rectangle recto = new Rectangle((int)actualX, (int)actualY, (int)actualWidth, (int)actualHeight);
                    using (Bitmap cropImg = new Bitmap((int)actualWidth, (int)actualHeight))
                    {
                        using (Graphics g = Graphics.FromImage(cropImg))
                        {
                            g.DrawImage(realimg, 0, 0, recto, GraphicsUnit.Pixel);
                            cropImg.Save(outputFolderPath + "\\" + img.Substring(img.LastIndexOf(@"\")) + "crop1.jpg");
                            count++;
                        }
                    }
                }
                GC.Collect();
            }
            
            csvList = GoogleWetDetect.MyGoogleVision(outputFolderPath);

            var saveFile = outputFolderPath + "\\DifferentiatorFile.csv";
            File.WriteAllLines(saveFile, csvList);
        }
        Thread thread;
        void StartCsv()
        {
            thread = new Thread(ProduceCSVMethod);
            thread.Start();
        }
        void StartTest()
        {
            thread = new Thread(TestCropRun);
            thread.Start();
        }
        private void ProduceCSV_Click(object sender, EventArgs e)
        {
            StartCsv();
        }
        string outputFolderPath = "";
        private void FolderBrowseOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                foldernameOutput.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
                outputFolderPath = folderDlg.SelectedPath;
            }
        }

        private void TestCrop_Click(object sender, EventArgs e)
        {
            StartTest();
        }
        public void TestCropRun()
        {
            int count = 0;


            foreach (string img in imagesInFolder)
            {
                using (Bitmap realimg = new Bitmap(img))
                {
                    //float ratioX = rect.X / pictureBox1.ClientSize.Width;
                    //float ratioY = rect.Y / pictureBox1.ClientSize.Height;

                    float percentWidth = (float)rect.Width / (float)pictureBox1.ClientSize.Width;
                    float actualWidth = (float)realimg.Width / 100 * (percentWidth * 100);

                    float percentHeight = (float)rect.Height / (float)pictureBox1.ClientSize.Height;
                    float actualHeight = (float)realimg.Height / 100 * (percentHeight * 100);

                    float percentX = (float)rect.X / (float)pictureBox1.ClientSize.Width;
                    float actualX = (float)realimg.Width / 100 * (percentX * 100);

                    float percentY = (float)rect.Y / (float)pictureBox1.ClientSize.Height;
                    float actualY = (float)realimg.Height / 100 * (percentY * 100);

                    Rectangle recto = new Rectangle((int)actualX, (int)actualY, (int)actualWidth, (int)actualHeight);

                    using (Bitmap cropImg = new Bitmap((int)actualWidth, (int)actualHeight))
                    {
                        using (Graphics g = Graphics.FromImage(cropImg))
                        {
                            g.DrawImage(realimg, 0, 0, recto, GraphicsUnit.Pixel);
                            cropImg.Save(outputFolderPath + "\\" + count + "crop1.jpg");
                            count++;
                        }
                    }
                }
            }
            GC.Collect();

        }

        private void StopProcesses_Click(object sender, EventArgs e)
        {
            thread.Abort();

        }

        private void folderPath_TextChanged(object sender, EventArgs e)
        {

            string folderpath = folderPath.Text;
            var actualpath = Path.GetDirectoryName(folderpath + @"\");
            var files = Directory.EnumerateFiles(actualpath, "*.jpg").OrderBy(filename => filename);

            imagesInFolder = files.ToList<string>();
            using (NaturalComparer comparer = new NaturalComparer())
            {
                imagesInFolder.Sort(comparer);
            }
            pictureBox1.Image = new Bitmap(imagesInFolder[0]);
        }

        private void foldernameOutput_TextChanged(object sender, EventArgs e)
        {
            outputFolderPath = foldernameOutput.Text;
        }

    }
}
