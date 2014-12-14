using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;                  //加入串口调用
using System.Runtime.InteropServices;   //调用系统API
using System.Windows.Threading;
using System.Threading;
using SpeakLib;

namespace 输出商品信息
{
    public partial class Form1 : Form
    {
        #region variable
        public static Product[] products;
        SerialPort serial = new SerialPort();
        string recieved_data;
        #endregion



        /// <summary>
       /// 发声函数的调用
       /// 注意是用DllImportAttribute从外边导入，紧接着应该声明导入的函数，注意要加extern修饰符
       /// </summary>
       /// <param name="szSound"></param>
       /// <param name="hMod"></param>
       /// <param name="flags"></param>
       /// <returns></returns>
        [DllImportAttribute("winmm.dll")]                     //导入winmm.dll
        private static extern bool PlaySound(string szSound, System.IntPtr hMod, PlaySoundFlags flags);
          public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001, 
            SND_NODEFAULT = 0x0002, 
            SND_LOOP = 0x0008, 
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000, 
            SND_FILENAME = 0x00020000, 
            SND_RESOURCE = 0x00040004 
        }


        /// <summary>
        /// 串口连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          private void btnConnect_Click(object sender, EventArgs e)
          {
              if (this.btnConnect.Text == "连接")
              {
                  serial.PortName = "COM4";                                    //串口号
                  serial.BaudRate = 9600;                                     //波特率
                  serial.Handshake = System.IO.Ports.Handshake.None;          //交换位
                  serial.Parity = Parity.None;                                //奇偶校验
                  serial.DataBits = 8;                                        //数据位
                  serial.StopBits = StopBits.One;                             //停止位
                  serial.ReadTimeout = 200;
                  serial.WriteTimeout = 50;
                  serial.Open();
                  this.btnConnect.Text = "已连接";
                  serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Recieve);//订阅收到串口数据时的处理事件
              }
              else
              {
                  try // just in case serial port is not open could also be acheved using if(serial.IsOpen)
                  {
                      serial.Close();
                      this.btnConnect.Text = "连接";
                  }
                  catch
                  {
                      MessageBox.Show("A Exception Happened");
                  }
              }
          }
     

        /// <summary>
        /// 串口数据处理
        /// </summary>
          #region Recieving
          private void Recieve(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
          {
              this.tbxInput.Text = serial.ReadExisting();
          }
          #endregion


        public Form1()
        {
            InitializeComponent();

            #region inits products infomation
            ///
            ///初始化商品信息
            ///
            products = new Product[30];
            for (int i = 0; i < 30; i++)
                products[i] = new Product();

            //设置分类，0表示食物，1表示学习用品
            for (int i = 0; i < 30; i++)
                if (i < 16)
                    products[i].type = 0;
                else
                    products[i].type = 1;

            //一个一个地赋值，名称和价格
            products[0].name = "康师傅红烧牛肉面"; products[0].price = 3.50F; products[0].labelnum = 6920152414019;
            products[1].name = "可口可乐罐装"; products[1].price = 2.0F; products[1].labelnum = 6920476664183;
            products[2].name = "蒙牛特仑苏牛奶"; products[2].price = 4.70F; products[2].labelnum =6923644266066;
            products[3].name = "德芙黑巧克力43克"; products[3].price = 6.80F; products[3].labelnum =6914973600041;
            products[4].name = "农夫山泉矿泉水1.5L"; products[4].price = 2.50F; products[4].labelnum =6921168520015;
            products[5].name = "金锣肉颗粒多"; products[5].price = 14.0F; products[5].labelnum =6927462214186;
            products[6].name = "达利园蛋黄派"; products[6].price = 9.0F; products[6].labelnum =6911988006783;
            products[7].name = "娃哈哈八宝粥"; products[7].price = 3.50F; products[7].labelnum =6902083880781;
            products[8].name = "娃哈哈AD钙奶 220*4瓶"; products[8].price = 7.0F; products[8].labelnum =6902083881085;
            products[9].name = "好吃点香脆腰果饼干"; products[9].price = 2.90F; products[9].labelnum =6911988009777;
            products[10].name = "金丝猴奶糖 118克袋装"; products[10].price = 5.90F; products[10].labelnum =6921681167564;
            products[11].name = "雀巢脆脆鲨威化饼干"; products[11].price = 23.0F; products[11].labelnum =6917878131504;
            products[12].name = "康师傅绿茶550ml装"; products[12].price = 2.60F; products[12].labelnum =6920459905166;
            products[13].name = "黄山毛尖"; products[13].price = 33.89F; products[13].labelnum =6931386400129;
            products[14].name = "雀巢咖啡罐装"; products[14].price = 4.00F; products[14].labelnum =6917878027333;
            products[15].name = "清扬男士去屑洗发露"; products[15].price = 22.0F; products[15].labelnum =6902088113112;
            products[16].name = "天堂雨伞"; products[16].price = 25.0F; products[16].labelnum =6912003033111;
            products[17].name = "黑人牙膏90克"; products[17].price = 200F; products[17].labelnum =4891338005692;
            products[18].name = "富光新太空杯800ml"; products[18].price = 39.0F; products[18].labelnum =6921899998084;
            products[19].name = "小闹钟"; products[19].price = 25.0F; products[19].labelnum =6941326932009;
            products[20].name = "微波饭盒"; products[20].price = 3.20F; products[20].labelnum =6930622800020;
            products[21].name = "双飞燕鼠标"; products[21].price = 60.0F; products[21].labelnum =6949336011332;
            products[22].name = "科学计算器"; products[22].price = 6.40F; products[22].labelnum =6921734917102;
            products[23].name = "英雄蓝色墨水"; products[23].price = 3.50F; products[23].labelnum =6940328702320;
            products[24].name = "真彩中性笔芯20根"; products[24].price = 16.0F; products[24].labelnum =6945091706056;
            products[25].name = "《高等数学导论（下）》，中国科学技术大学出版社"; products[25].price = 37.50F; products[25].labelnum =9787312029851;
            products[26].name = "笔记本"; products[26].price = 6.60F; products[26].labelnum =6925550576024;
            products[27].name = "得力订书机"; products[27].price = 13F; products[27].labelnum =6921734903259;
            products[28].name = "led小台灯"; products[28].price = 33F; products[28].labelnum =6939020420603;
            products[29].name = "心相印纸面巾"; products[29].price = 5.20F; products[29].labelnum = 6903244984102;
            #endregion



            this.btnOK.Enabled = false;
            this.tbxInput.Validating += new System.ComponentModel.CancelEventHandler(this.tbxInputEmpty_Validating);
            this.tbxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxInput_KeyPress);
            this.tbxInput.TextChanged += new System.EventHandler(this.tbxInput_TextChanged);
            this.WindowState = FormWindowState.Maximized;
        }
        private void tbxInput_TextChanged(object sender,System.EventArgs e)
        {
            TextBox tb=(TextBox)sender;

            if (tb.Text.Length == 0)
            {
                tb.BackColor = Color.Blue;
                tb.Tag = false;
            }
            else 
            {
                int range = Convert.ToInt32(tb.Text);
                if (range > 0 && range < 31)
                {
                    tb.BackColor = SystemColors.Window;
                    tb.Tag = true;
                }
                else 
                {
                    tb.BackColor = Color.Red;
                    tb.Tag = false;
                }
            }
            ValidateAll();
        }

        /// <summary>
        /// 输入检验：输入商品编号必须是字符或#键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxInput_KeyPress(object sender,KeyPressEventArgs e)
        {
            TextBox tb=(TextBox)sender;

            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                e.Handled = true;
        }

        /// <summary>
        /// 输入有效性验证 必须是0-30内的数字          
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxInputEmpty_Validating(object sender,System.ComponentModel.CancelEventArgs e)
        {
            TextBox tb=(TextBox)sender;
            try 
            {
                if (tb.Text.Length == 0)
                {
                    tb.BackColor = Color.Blue;
                    tb.Tag = false;
                    tb.Tag = true;
                }
                else 
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    tb.Tag = true;
                }
            }
            catch(System.Exception)
            {
                MessageBox.Show("A Exception Happened In tbxInputEmpty_Validating ");
            }
            ValidateAll();
        }
        
        /// <summary>
        /// 使确定键能按下
        /// </summary>
        private void ValidateAll()
        {
            this.btnOK.Enabled = (bool)this.tbxInput.Tag;
        }
           
      
        /// <summary>
        /// 商品信息输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int index;
            index = Convert.ToInt32(this.tbxInput.Text)-1;
            try
            {
                //商品名称
                this.tbxName.Text = products[index].name.ToString();

                //商品分类
                if (products[index].type == 0)
                    this.tbxType.Text = "食物";
                else
                    this.tbxType.Text = "学习生活用品";

                //商品价格
                 this.tbxPrice.Text = Convert.ToString(products[index].price)+"元";

                //商品货架号
                this.tbxShelf.Text = "第"+(((index+1) %6==0) ?((index+1)/6):((index+1)/6+1)).ToString()+"货架";

                //商品层数
                 if (((index) / 3 + 1) % 2 == 0)
                     this.tbxStair.Text = "第2层";
                 else
                     this.tbxStair.Text = "第1层";
            }
            catch (System.Exception) 
            {
                MessageBox.Show("A Exception Happened In button1_Click:price=");
            }
        }



      /// <summary>
      /// 发声程序 待完善
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void btnVoice_Click(object sender, EventArgs e)
        {
           // TextBox tb=(TextBox)sender;

            SpeakLib.Class1.Read("good");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public class Product
    {
        public string name="undefined";     //名称
        public float price=-1;              //价格
        public long labelnum=-1;            //条形码
        public int type=-1;                 //分类
        public void product() 
        {
        }
    }
}
