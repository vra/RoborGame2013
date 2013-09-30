/* bug
 * 1.串口输入框输入以零开头时报错
 * 
 * 
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeakLib;                    //加入语音模块
using System.IO.Ports;             //加入串口调用
using ProductInfomationLib;        //加入商品信息库
using Map;
using RouteLibrary;
using System.Runtime.InteropServices;
namespace GouTu
{
    public partial class Form1 : Form
    {

        #region variable
        ProductInfomationLib.Product[] products;
        SerialPort serial = new SerialPort();
        private int proid1 = -1;
        private int proid2 = -1;
        private int proid3 = -1;

        int beforeNode = 0;
        int lastNode = 0;
        int nextNode = 0;
        int nowX = 0;
        int nowY = 0;
        List<int> pointsOnWay=new List<int>();
        
        #endregion

        public Form1()
        {
            InitializeComponent();

            //初始化商品信息
            products = ProductInfomationLib.Product.GetProducts();

            //初始化地图坐标信息
            Map.Class1.CreateMap();
            RouteLibrary.Class1.SetPAndPosition();

            //初始化端口信息
         //   InitPort();
        }

        
        #region 输出商品信息
        private void OutputProductInfo(double index, bool inway)
        {
            int sink = -1;
            if (inway)                          //从串口输入
            {
                sink = (int)index;
                Paint(sink);

            }
            else                                //从条形码输入
            {
                for (int i = 0; i <= 29; i++)
                    if (products[i].labelnum == index)
                        sink = i;

                Paint(sink);
            }
        }

        private void Paint(int index)
        {
            //商品名称
            this.tbxName.Text = products[index].name.ToString();

            //商品分类
            if (products[index].type == 0)
                this.tbxType.Text = "食物";
            else
                this.tbxType.Text = "学习生活用品";

            //商品价格
            this.tbxPrice.Text = Convert.ToString(products[index].price) + "元";

            //商品货架号
            this.tbxShelf.Text = "第" + (((index + 1) % 6 == 0) ? ((index + 1) / 6) : ((index + 1) / 6 + 1)).ToString() + "货架";

            //商品层数
            if (((index) / 3 + 1) % 2 == 0)
                this.tbxStair.Text = "第2层";
            else
                this.tbxStair.Text = "第1层";

            EnsureOrder(index);

            RichPaint(index);
        }

        private void EnsureOrder(int index)
        {
            if (proid1 == -1)
                proid1 = index;
            else if (proid2 == -1)
                proid2 = index;
            else if (proid3 == -1)
                proid3 = index;

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            SpeakLib.Class1.Read(
                                "商品信息如下："
                                + "名称" + this.tbxName.Text.ToString()
                                + "分类" + this.tbxType.Text.ToString()
                                + "价格" + this.tbxPrice.Text.ToString()
                                + "货架编号" + this.tbxShelf.Text.ToString()
                                + "层数" + this.tbxStair.Text.ToString()
                                );
        }

        private void RichPaint(int indext)
        {
            if (proid1 == -1) //zero
            {

            }
            else if (proid2 == -1)//one
            {
                this.rtxMessage.Text =
                    ("当前已购买商品数：\n\t1件\n"
                    + "商品名称：\n\t" + products[proid1].name + "\n"
                    + "单价：\n\t" + products[proid1].price + "元\n"
                    + "总价：\n\t" + products[proid1].price + "元\n"
                    );

            }
            else if (proid3 == -1)//two
            {
                this.rtxMessage.Text =
                    ("当前已购买商品数：\t2件\n"
                    + "商品1：\n\t" + products[proid1].name + "\n"
                    + "单价：\n\t" + products[proid1].price + "元\n"
                    + "商品2：\n\t" + products[proid2].name + "\n"
                    + "单价：\n\t" + products[proid2].price + "元\n"
                    + "总价：\t" + (products[proid1].price + products[proid2].price) + "元\n"
                    );
            }
            else //three
            {
                this.rtxMessage.Text =
                   ("当前已购买商品数：\t3件\n"
                   + "商品1：\n\t" + products[proid1].name + "\n"
                   + "单价：\n\t" + products[proid1].price + "元\n"
                   + "商品2：\n\t" + products[proid2].name + "\n"
                   + "单价：\n\t" + products[proid2].price + "元\n"
                   + "商品3：\n\t" + products[proid3].name + "\n"
                   + "单价：\n\t" + products[proid3].price + "元\n"
                   + "总价：\t" + (products[proid1].price + products[proid2].price + products[proid3].price) + "元\n"
                   );
            }
        }
        #endregion


        #region 按键响应程序

        //从键盘输入商品编号
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double index = -1;
            TextBox tb = (TextBox)sender;
            if (tb.Text.IndexOf('#') == 1 | tb.Text.IndexOf('#') == 2)                            //从0开始
            {
                index = Convert.ToDouble(tb.Text.Substring(0, (tb.Text.IndexOf('#')))) - 1;             //这个index从0开始
                OutputProductInfo(index, true);
            }
        }

        //读条形码
        private void tbxLabelnum_TextChanged(object sender, EventArgs e)
        {
            serial.Write("T");
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length < 13)
                return;

            else
            {
                string num = tb.Text.Substring(0, 13);
                MessageBox.Show(num);
                OutputProductInfo(Convert.ToDouble(num), false);

                //发送商品3的信息
                if (products[proid3].type == 0)
                {
                    serial.Write("$1");
                    MessageBox.Show("发送商品3类别：食物");
                }

                else
                {
                    serial.Write("$2");
                    MessageBox.Show("发送商品3类别：学习");
                }


                bool loop = true;
                do
                {
                    string recv1 = serial.ReadLine();
                    if (recv1.IndexOf('D') != -1)
                    {
                        loop = false;
                        MessageBox.Show("收到D");
                    }

                } while (loop);

                int proPoint3 = Product2MapPoint(proid3);
                RouteLibrary.Class1.Dijkstra(proPoint3); //以商品3为起点，计算到收银台的路径
                TransmitPoints(38);

                

            }
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            SpeakLib.Class1.Read(this.rtxMessage.Text);
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void rtxMessage_TextChanged(object sender, EventArgs e)
        {

        }
        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btnFollow_Click(object sender, EventArgs e)
        {
            InitPort();
            DaoGou();
            serial.Close();             //注意传完信息后串口必须关闭，否则下一次传输时无法执行InitPort（）；
            tbxLabelnum.Focus();
        }

        private void btnSeparate_Click(object sender, EventArgs e)
        {
            InitPort();

            serial.Close();             
        }
        #endregion

        #region 串口相关

        private void InitPort()
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
        }

        
        private void TransmitPoints(int mapPointIndex)   //传送算出的路径上经过的坐标点,注意此函数只能在调用Dijkstra后使用
        {
            string allPoints = "";
            int j = 0;
            for (int i = 0; i < 44; i++)
                if (RouteLibrary.Class1.p[mapPointIndex, i] != -1)
                {
                    int temp = RouteLibrary.Class1.p[mapPointIndex, i];
                    pointsOnWay.Insert(j,temp);
                //    MessageBox.Show("pointOnWaylength:"+pointsOnWay.Count);
                    
                //    MessageBox.Show(pointsOnWay[j].ToString());

                    j++;

                    if (RouteLibrary.Class1.p[mapPointIndex, i].ToString().Length == 2)
                        allPoints = allPoints + RouteLibrary.Class1.p[mapPointIndex, i].ToString() + " ";
                    else
                        allPoints = allPoints + "0" + RouteLibrary.Class1.p[mapPointIndex, i].ToString() + " ";
                    
                }

           
            string send = "*" + allPoints + "#";
            MessageBox.Show(send);
            serial.Write(send);

        }

        private void DaoGou()
        {
            if (proid1 == -1)
            {
                SpeakLib.Class1.Read("还没输入商品哟亲");
                MessageBox.Show("还没输入商品哟亲");
            }
            else if (proid2 == -1)
            {
                SpeakLib.Class1.Read("还缺一件商品哟亲");
                MessageBox.Show("还缺一件商品哟亲");
            }
            else
            {
                //1.发送到商品1路过的节点
                int proPoint1 = Product2MapPoint(proid1);
                int proPoint2 = Product2MapPoint(proid2);
            //    RouteLibrary.Class1.CreateNode(8, 13);
             //   RouteLibrary.Class1.CreateNode(9, 14);
           //     RouteLibrary.Class1.CreateNode(10, 15);
                RouteLibrary.Class1.Dijkstra(0);
                TransmitPoints(proPoint1);                                              //

                //2.串口等待数据
               
                bool waitingSerial = true;
                string recv = "";

               
                while (waitingSerial)
                {
                    serial.ReadTimeout = 50000;
                    recv = serial.ReadLine();
                    
                   // serial.DiscardInBuffer();               //清除串口缓冲数据

                    //1.遇到障碍物，则封路,重新算路径并发送节点
                    if (recv != ""&& recv!="\n")
                    {
                        MessageBox.Show("recv=");
                        MessageBox.Show(recv);
                        if (recv.IndexOf('!') != -1)
                        {
                            MessageBox.Show("遇到障碍物了");
                            serial.DiscardInBuffer(); 
                            GetPoint(recv);
                            MessageBox.Show("算出的两个点是");
                            
                            GetTwoNode(nowX,nowY);
                            MessageBox.Show(lastNode.ToString());
                            MessageBox.Show(nextNode.ToString());
                            RouteLibrary.Class1.CreateNode(lastNode, nextNode);
                            RouteLibrary.Class1.Dijkstra(beforeNode);
                            TransmitPoints(proPoint1);
                        }
                            //解读出nowPoint,lastNode,nextNode

                        else if (recv.IndexOf('F') != -1)
                        {
                           // serial.DiscardInBuffer(); 
                            pointsOnWay.Clear();                //！！清除L注意必须清除list里面的所有数据
                           // System.Threading.Thread.Sleep(2000); //到达商品1,先暂停2s（具体数值还有待确定），作为取商品的时间，然后发送给单品商品类别

                            if (products[proid1].type == 0)
                            {
                                serial.Write("$1");
                                MessageBox.Show("发送商品1的类别：食物");
                            } //0表示食物

                            else
                            {
                                serial.Write("$2");
                                MessageBox.Show("发送商品2的类别：学习");
                            } //0表示食物

                            
                            bool loop = true;
                            do
                            {
                                string recv1 = serial.ReadLine();
                                if (recv1.IndexOf('D') != -1)
                                {
                                    loop = false;
                                    MessageBox.Show("收到D");
                                }
                                
                            } while (loop);

                            RouteLibrary.Class1.Dijkstra(proPoint1); //以商品1为起点，计算到商品2的路径
                            TransmitPoints(proPoint2);
                            
                        }
                        else if (recv.IndexOf('#') != -1)
                        {
                                MessageBox.Show("遇到障碍物了");
                                serial.DiscardInBuffer(); 
                            //解读出nowPoint,lastNode,nextNode
                                GetPoint(recv);
                                GetTwoNode(nowX,nowY);
                                RouteLibrary.Class1.CreateNode(lastNode, nextNode);
                                RouteLibrary.Class1.Dijkstra(beforeNode);
                                TransmitPoints(proPoint2);
                        }
                        else if (recv.IndexOf('S') != -1)
                        {
                          //  serial.DiscardInBuffer(); 

                            System.Threading.Thread.Sleep(2000);//到达商品2,先暂停2s（具体数值还有待确定），作为取商品的时间，然后发送给单品商品类别

                            if (products[proid2].type == 0) //0表示食物
                            {
                               serial.Write("$1");
                               MessageBox.Show("发送商品2的类别：食物");
                            }
                            else
                            {
                                serial.Write("$2");
                                MessageBox.Show("发送商品2的类别：学习");
                            }
                            
                                waitingSerial = false;//跳出循环，进入跟随阶段
                        }
                    }
                    //每次执行最后要清除recv
                    recv = "";
                }
            }
        }

        //当串口收到有障碍物的信息时，解读出当前点，障碍物左右两点
        private void GetPoint(string recv)
        {
            char[] delimiterChars = { ' ' };

            string[] points = recv.Split(delimiterChars);

           
            if (points.Length < 3)
            {
                MessageBox.Show("串口数据有误");
                return;
            }

            beforeNode = Convert.ToInt32(points[1]);
            nowX = Convert.ToInt32(points[2]);
            nowY = Convert.ToInt32(points[3]);
        }

        private void GetTwoNode(int x, int y)
        {
            //   
            MessageBox.Show("nowX=");
            MessageBox.Show(nowX.ToString());
            MessageBox.Show("nowY=");
            MessageBox.Show(nowY.ToString());
            for (int i = 0; (i <pointsOnWay.Count -1 )&& (i<=6)&&(pointsOnWay[i]>=lastNode); i++)
            {

                if (RouteLibrary.Class1.postion[pointsOnWay[i], 0] == RouteLibrary.Class1.postion[pointsOnWay[i + 1], 0])
                    //横坐标相同,判断nowY与两个y的关系
                {
                    MessageBox.Show("i=" + i + "时，进入x相等循环");
                    if ((RouteLibrary.Class1.postion[pointsOnWay[i], 1] <= nowY && nowY <= RouteLibrary.Class1.postion[pointsOnWay[i + 1], 1])
                            || (RouteLibrary.Class1.postion[pointsOnWay[i + 1], 1] <= nowY && nowY <= RouteLibrary.Class1.postion[pointsOnWay[i], 1]))
                    {
                        if ((RouteLibrary.Class1.postion[pointsOnWay[i], 1] < nowY && nowY < RouteLibrary.Class1.postion[pointsOnWay[i + 1], 1])
                            || (RouteLibrary.Class1.postion[pointsOnWay[i + 1], 1] < nowY && nowY < RouteLibrary.Class1.postion[pointsOnWay[i], 1]))
                        {
                            MessageBox.Show("x相同，在两点之间");
                           // MessageBox.Show(i.ToString());  
                            lastNode = pointsOnWay[i];
                            nextNode = pointsOnWay[i+1];
                            break;
                        }
                        if (RouteLibrary.Class1.postion[pointsOnWay[i], 1] == nowY && RouteLibrary.Class1.postion[pointsOnWay[i], 0] == nowX)
                        {
                            MessageBox.Show("x相同，在点1上");

                           // MessageBox.Show(i.ToString());
                            lastNode = pointsOnWay[i];
                            nextNode = pointsOnWay[i];
                            break;
                        }
                        if (RouteLibrary.Class1.postion[pointsOnWay[i + 1], 1] == nowY && RouteLibrary.Class1.postion[pointsOnWay[i + 1], 0] == nowX )
                        {
                            MessageBox.Show("x相同，在点2上");
                            //MessageBox.Show(i.ToString());
                            lastNode = pointsOnWay[i + 1];
                            nextNode = pointsOnWay[i + 1];
                            break;
                        }
                    }
                }
                else if (RouteLibrary.Class1.postion[pointsOnWay[i], 1] ==RouteLibrary.Class1.postion[pointsOnWay[i + 1], 1]) //纵坐标相同,判断nowX与两个x的关系
                {
                    MessageBox.Show("i=" + i + "时，进入y相等循环");
                    if ((RouteLibrary.Class1.postion[pointsOnWay[i], 0] <= nowX &&
                               nowX <= RouteLibrary.Class1.postion[pointsOnWay[i + 1], 0])
                              ||
                              (RouteLibrary.Class1.postion[pointsOnWay[i + 1], 0] <= nowX &&
                               nowX <= RouteLibrary.Class1.postion[pointsOnWay[i], 0]))
                            
                    {
                        if ((RouteLibrary.Class1.postion[pointsOnWay[i], 0] <nowX &&
                             nowX < RouteLibrary.Class1.postion[pointsOnWay[i + 1], 0])
                            ||
                            (RouteLibrary.Class1.postion[pointsOnWay[i + 1], 0] < nowX &&
                             nowX < RouteLibrary.Class1.postion[pointsOnWay[i], 0]))
                            {
                                MessageBox.Show("y相同，在两点之间");
                                //MessageBox.Show(i.ToString());
                                lastNode = pointsOnWay[i];
                                nextNode = pointsOnWay[i + 1];
                                break;
                            }
                        else if (RouteLibrary.Class1.postion[pointsOnWay[i], 0] == nowX && RouteLibrary.Class1.postion[pointsOnWay[i], 1] == nowY)
                        {
                            MessageBox.Show("y相同，在点1上");
                            //MessageBox.Show(i.ToString());
                            lastNode = pointsOnWay[i];
                            nextNode = pointsOnWay[i];
                            break;

                        }
                        else if (RouteLibrary.Class1.postion[pointsOnWay[i + 1], 0] == nowX && RouteLibrary.Class1.postion[pointsOnWay[i + 1], 1] == nowY)
                        {
                            MessageBox.Show("y相同，在点2上");
                            //MessageBox.Show(i.ToString());
                            lastNode = pointsOnWay[i + 1];
                            nextNode = pointsOnWay[i + 1];
                            break;
                        }

                    }
                }

            }
        }


        //将商品编号对应到坐标上的点
        private int Product2MapPoint(int proid)
        {
            int mapPoint = 0;
            switch (proid + 1)
            {
                case 1:
                case 4:
                    mapPoint = 8;
                    break;
                case 2:
                case 5:
                    mapPoint = 13;
                    break;
                case 3:
                case 6:
                    mapPoint = 18;
                    break;
                case 7:
                case 10:
                    mapPoint = 9;
                    break;
                case 8:
                case 11:
                    mapPoint = 14;
                    break;
                case 9:
                case 12:
                    mapPoint = 19;
                    break;
                case 13:
                case 16:
                    mapPoint = 10;
                    break;
                case 14:
                case 17:
                    mapPoint = 15;
                    break;
                case 15:
                case 18:
                    mapPoint = 20;
                    break;
                case 19:
                case 22:
                    mapPoint = 11;
                    break;
                case 20:
                case 23:
                    mapPoint = 16;
                    break;
                case 21:
                case 24:
                    mapPoint = 21;
                    break;
                case 25:
                case 28:
                    mapPoint = 12;
                    break;
                case 26:
                case 29:
                    mapPoint = 17;
                    break;
                case 27:
                case 30:
                    mapPoint = 22;
                    break;
                default:
                    break;
            }
            return mapPoint;
        }
        #endregion
    }
}


