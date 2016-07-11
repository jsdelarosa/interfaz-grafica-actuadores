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
using System.IO.Ports;
using System.Threading;

namespace interfazgraficatest
{
    public partial class Form1 : Form
    {
        int second = 0;
        public Form1()
        {
            InitializeComponent();
            
            serialPort1.Open();

            pictureBox5.Visible = false;
            timer1.Interval = 1000;
            timer1.Start();
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            toolStripStatusLabel1.Text = "Listo";
        }

        private void load() {
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
        }

        //VÁLVULA 1
        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        string a = "1\r";
        private void button3_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("¿Desea Alinear VLV 1?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    if ((vlv1open == true) && (vlv1close == false))
                    {
                        MessageBox.Show("La válvula ya está Alineada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    serialPort1.WriteLine(a);
                    pictureBox8.Visible = true;
                    flag = true;
                }
                else
                {
                }
                
                richTextBox3.Text = flag.ToString() + " vlv1open";
               // serialPort1.WriteLine("0\r");
            }
        }

        bool flag=false;
        string b = "2\r";
        private void button4_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("¿Desea Cerrar VLV 1?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    if ((vlv1open == false) && (vlv1close == true))
                    {
                        MessageBox.Show("La válvula ya está cerrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    serialPort1.WriteLine(b);
                    pictureBox8.Visible = true;
                    flag = true;
                }
                else
                {
                }
                richTextBox3.Text = flag.ToString()+" vlv1close";
               
            }
        }

        //VÁLVULA 2
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        string c = "3\r";
        private void button5_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("¿Desea Alinear VLV 2?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    if ((vlv2open == true) && (vlv2close == false))
                    {
                        MessageBox.Show("La válvula ya está Alineada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    serialPort1.WriteLine(c);
                    pictureBox9.Visible = true;

                }
                else
                {
                }
                richTextBox3.Text = flag.ToString() + " vlv2open";
            }

        }

        string d = "4\r";
        private void button6_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("¿Desea Cerrar VLV 2?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    if ((vlv2open == false) && (vlv2close == true))
                    {
                        MessageBox.Show("La válvula ya está Cerrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    serialPort1.WriteLine(d);
                    pictureBox9.Visible = true;
                }
                else
                {
                }
                richTextBox3.Text = flag.ToString() + " vlv2close";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ports = SerialPort.GetPortNames();
        }

        string g = "11\r";
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            serialPort1.Write(g);
            pictureBox5.Visible = true;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        bool vlv1LR;
        bool vlv1open;
        bool vlv1close;

        bool vlv2LR;
        bool vlv2open;
        bool vlv2close;
        string f;
        string datoRecibido;

        private void timer1_Tick(object sender, EventArgs e)
        {
            serialPort1.DtrEnable = true;   
            datoRecibido = null;
            f = "5\r";
            second++;
            label3.Text = second.ToString();
            datoRecibido = serialPort1.ReadExisting();
            serialPort1.WriteLine(f);
            datoRecibido = serialPort1.ReadLine();
            datoRecibido=datoRecibido.TrimStart('5');
          //  richTextBox1.Text = datoRecibido;

            if (datoRecibido=="0")
            {

                vlv1LR = false;
                pictureBox6.Visible = true;
            }
            else
            {

                vlv1LR = true;
                pictureBox6.Visible = false;
            }

            f = "6\r";
            serialPort1.WriteLine(f);
            datoRecibido = serialPort1.ReadLine();
            datoRecibido = datoRecibido.TrimStart('6');
           // richTextBox1.Text = datoRecibido;
            if (datoRecibido == "0")
            {
                vlv1open = false;
            }
            else
            {
                vlv1open = true;
            }

            f = "7\r";
            serialPort1.WriteLine(f);
            datoRecibido = serialPort1.ReadLine();
            datoRecibido = datoRecibido.TrimStart('7');
           // richTextBox1.Text = datoRecibido;

            if (datoRecibido == "0")
            {
                vlv1close = false;
            }
            else
            {
                vlv1close = true;
            }

            button3.Cursor = Cursors.Arrow;
            button4.Cursor = Cursors.Arrow;
            toolStripStatusLabel1.Text = "Estatus Válvula 1: Normal";
            toolStripStatusLabel1.BackColor = System.Drawing.Color.Empty;
            toolStripStatusLabel2.Text = "Estatus Válvula 2: Normal";
            toolStripStatusLabel2.BackColor = System.Drawing.Color.Empty;
            if ((vlv1open == true) && (vlv1close == true))
            {
               // MessageBox.Show("Revise estado de Válvula 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                toolStripStatusLabel1.Text = "Estatus Válvula 1: Error";
                toolStripStatusLabel1.BackColor = System.Drawing.Color.Red;
            }
            else if ((vlv1open == true) && (vlv1close == false))
            {
                pictureBox3.Visible = true;
                pictureBox8.Visible = false;
                textBox1.Text = "Válvula: Alineada";
                button3.Cursor = Cursors.No;
            }
            else if ((vlv1open == false) && (vlv1close == true))
            {
                pictureBox3.Visible = false;
                pictureBox8.Visible = false;
                textBox1.Text = "Válvula: Cerrada";
                button4.Cursor = Cursors.No;
            }
            else
           // else if ((vlv1open = false) && (vlv1close = false))
            {
                textBox1.Text = "Válvula: Tránsito";
                pictureBox8.Visible = true;
            }
            
            f = "8\r";
            serialPort1.WriteLine(f);
            datoRecibido = serialPort1.ReadLine();
            datoRecibido = datoRecibido.TrimStart('8');
            //richTextBox1.Text = datoRecibido;

            if (datoRecibido == "0")
            {
                pictureBox7.Visible = true;
                vlv2LR = false;
            }
            else
            {
                pictureBox7.Visible = false;
                vlv2LR = true;
            }

            f = "9\r";
            serialPort1.WriteLine(f);
            datoRecibido = serialPort1.ReadLine();
            datoRecibido = datoRecibido.TrimStart('9');
            //richTextBox1.Text = datoRecibido;

            if (datoRecibido == "0")
            {
                vlv2open = false;
            }
            else
            {
                vlv2open = true;
            }

            f = "10\r";
            serialPort1.WriteLine(f);
            datoRecibido = serialPort1.ReadLine();
            datoRecibido = datoRecibido.TrimStart('1');

            //richTextBox1.Text = datoRecibido;
            if (datoRecibido == "00")
            {
                vlv2close = false;
            }
            else
            {
                vlv2close = true;
            }

            richTextBox1.Text = vlv1open.ToString();
            richTextBox2.Text = vlv1close.ToString();

            button5.Cursor = Cursors.Arrow;
            button6.Cursor = Cursors.Arrow;
            if ((vlv2open == true) && (vlv2close == true))
            {
                toolStripStatusLabel2.Text = "Estatus Válvula 2: Error";
                toolStripStatusLabel2.BackColor = System.Drawing.Color.Red;
            }
            else if ((vlv2open == true) && (vlv2close == false))
            {
                pictureBox2.Visible = true;
                pictureBox9.Visible = false;
                textBox2.Text = "Válvula: Alineada";
                button5.Cursor = Cursors.No;
            }
            else if ((vlv2open == false) && (vlv2close == true))
            {
                pictureBox2.Visible = false;
                pictureBox9.Visible = false;
                textBox2.Text = "Válvula: Cerrada";
                button6.Cursor = Cursors.No;
            }
            else
            //else if ((vlv2open == false) && (vlv2close == false))
            {
                textBox2.Text = "Válvula: Tránsito";
                pictureBox9.Visible = true;
            }
        }
    }
}
