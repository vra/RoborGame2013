using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductLibrary.dll;

namespace YouTiaoXingMaShuChuShangPinXinXiJiZongJia
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ProductLibrary.dll.Class1.products[9].labelnum = 6903244984102;
            double labelnum = ProductLibrary.dll.Class1.products[9].labelnum;
           for(int i=1;i<31;i++)
               if(labelnum==ProductLibrary.dll.Class1.products[i].labelnum)
                   this.tbxName.Text=ProductLibrary.dll.Class1.products[i].name;
        }
    }
}
