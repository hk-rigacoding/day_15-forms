using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day_15_forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            //komand rindas argumenti
            string[] args = Environment.GetCommandLineArgs();

            InitializeComponent();
        }

        private void bilde_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog d = new OpenFileDialog();


            //šis kods izpildīsies, kad klikšķināsim uz pogas !


            //failu izvēles filtrs
            //solo fails
            //d.Filter = "Image files(*.png; *.jpeg; *bmp)|*.png;*jpeg;*.bmp|Custom files(a*.*)|a*.*|All files(*.*)|*.*";



            //multi fails

            //string[] masīvs, kas saturēs vairākus failu nosaukumus
            //d.FileNames

            //d.Multiselect = true;
            d.Filter = "Image files(*.png; *.jpeg; *bmp)|*.png;*jpeg;*.bmp|Custom files(a*.*)|a*.*|All files(*.*)|*.*";

            d.ShowDialog();

            //pilns ceļš līdz izvēlētajam failam
            string fname = d.FileName;
            string[] fnames = d.FileNames;

            //ja dialogā nospiežam cancel, tad fname būs tukšs !

            //ja fname ir tukšs, tad neko nedaram
            if (fnames.Length == 0)
            {
                MessageBox.Show("Nav izvēlēta bilde @");
                return;
            }

            //bilde.ImageLocation = fname;
            //bilde.SizeMode

            PictureBox pb = new PictureBox();

            int yloc = 10;


            //1. instancēt tik PictureBox objektus, cik ir izvēlēti faili
            //2. ielādējiet bildes ar bilde.load("filename")
            //3. salieciet (nokoriģējiet) adekvātas Y lokacijas !!! - jānosaka bildes augstums ar bilde.Image.Height
            //
            
            //PictureBox pb = new PictureBox();

            
            pb.Size = new Size(100,100);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            pb.Load(fname);
            pb.Click += Pb_Click;
            pb.MouseMove += Pb_MouseMove;
            //log.KeyPress += log_KeyPress;

            pb.Location = new Point(pb.Location.X, yloc);

            yloc += 120;


            this.Controls.Add(pb);

            this.Refresh();



            //korekcijas cikls
            //pb.Location = new Point(10, Y+???);

            
            progress.Maximum = 50;
            int x = pb.Location.X;

            for (int i = 0; i < 50; i++)
            {
                //foreach (PictureBox bilde in pbmas) //loops katrai bildei šito !
                //{
                //mainam bildes atrašanās vietu : sākotnējai X + i
                pb.Location = new Point(x  + i, pb.Location.Y);
                //bilde.Load(fname);

                //atļaut apstrādāt citus notikumus, savādāk forma būs 'iefrīzojusi'
                Application.DoEvents();

                //gaidīt 100 milisekundes
                Thread.Sleep(100);
                //}

                progress.Value = i;
            }
            

            //bilde.Load(fname);



            //variants 1
            //ciklā failu apstrāde
            //log.AppendText(fname);
            //new line  - stringam pievieno speciālo simbolu
            //log.AppendText(Environment.NewLine);


            //variants 2
            log.Lines = fnames;


            //masīvs ar elementiem
            //string pirmaa = log.Lines[0];


            //kā viens strings
            //log.Text



            //pievienot vai instancēt SaveFileDialog
            //[izveidot filtru]
            //izsaukt ShowDialog()
            //ievadīt faila nosaukumu, kuŗā saglabāt "log" datus
            //saglabāt failā "log" datus.


            //kā noteikt, ko lietotājs ir izvēlējies Dialogā ?

            //if (dialogs.ShowDialog() == DialogResult.OK)
            //nospiests OK
            //else
            //nospiests cancel



            SaveFileDialog s = new SaveFileDialog();


            s.Filter = "Image files(*.png; *.jpeg; *bmp)|*.png;*jpeg;*.bmp|Custom files(a*.*)|a*.*|All files(*.*)|*.*";


           
            if (s.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Nav izvēlēta bilde @");
                return;
            }
           

            //s.ShowDialog();

            //pilns ceļš līdz izvēlētajam failam
               string sfname = s.FileName;


            //ja fails eksistē
            //if (File.Exists(sfname))

            //ja fails neeksistē
            if (!File.Exists(sfname))
            {
                File.AppendAllText(sfname, "ADDED " + DateTime.Now.ToString() + " \n");
                File.AppendAllLines(sfname, log.Lines);
            } else
            {

                DialogResult rezultaats = MessageBox.Show("Vai gribi pārrakstīt esošu failu ?", "Apstiprini ...", MessageBoxButtons.YesNo);
                    
                 if (rezultaats == DialogResult.Yes)
                {
                    File.AppendAllText(sfname, "ADDED " + DateTime.Now.ToString() + " \n");
                    File.AppendAllLines(sfname, log.Lines);
                }

            }

        }



        private void Pb_MouseMove(object sender, MouseEventArgs e)
        {
            //atjaunot leibla saturu ar peles X:Y info
            //e.X
            //e.Y
            

            status.Text = "X:" + e.X + "; Y:" + e.Y;

            //throw new NotImplementedException();
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Booo");

                                        

            ToolTip tt = new ToolTip();
            tt.SetToolTip((PictureBox)sender, "Boo!");


            this.Controls.Remove((PictureBox)sender);


            //throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {


            DialogResult rezultaats = MessageBox.Show("Vai gribi iziet no programmas ?", "Apstiprini ...", MessageBoxButtons.YesNo);

            if (rezultaats == DialogResult.Yes)
            {
                Application.Exit();
            }

            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void log_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            MessageBox.Show("Key Presed : " + e.KeyChar);
        }
    }
}
