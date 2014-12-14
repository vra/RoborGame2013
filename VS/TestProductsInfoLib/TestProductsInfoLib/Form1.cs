using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductsInfoLib;

namespace TestProductsInfoLib
{
    public partial class Form1 : Form
    {
        //static ProductsInfoLib.Class1.products[];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                for (int i = 1; i <= 30; i++)
                    if (ProductsInfoLib.Class1.products[i].labelnum == Convert.ToDouble(this.textBox1.Text))
                    {
                        MessageBox.Show("got");
                    }
                    else
                    {
                        MessageBox.Show("NOt Found");
                    }
            

        }
    }
}
