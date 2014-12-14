using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInfomationLib
{
    public class Class1
    {

    }
    public class Product
    {
        public int index = -1;                 //商品编号
        public string name = "undefined";     //名称
        public float price = -1;              //价格
        public long labelnum = -1;            //条形码
        public int type = -1;                 //分类
        public void product()
        {
        }

        public static Product[] GetProducts()
        {
           Product[] products;
           products = new Product[30];

           for (int i = 0; i < 30; i++)
           {
               products[i] = new Product();
               products[i].index = i + 1;
           }

            //设置分类，0表示食物，1表示学习用品
            for (int i = 0; i < 30; i++)
                if (i < 16)
                    products[i].type = 0;
                else
                    products[i].type = 1;

            //一个一个地赋值，名称和价格
            products[0].name = "康师傅红烧牛肉面"; products[0].price = 3.50F; products[0].labelnum = 6920152414019;
            products[1].name = "可口可乐罐装"; products[1].price = 2.0F; products[1].labelnum = 6908198103807;
            products[2].name = "蒙牛特仑苏牛奶"; products[2].price = 4.70F; products[2].labelnum =6923644266066;
            products[3].name = "德芙黑巧克力43克"; products[3].price = 6.80F; products[3].labelnum =6914973600041;
            products[4].name = "农夫山泉矿泉水1.5L"; products[4].price = 2.50F; products[4].labelnum =6921168520015;
            products[5].name = "金锣肉颗粒多"; products[5].price = 14.0F; products[5].labelnum =6927462214186;
            products[6].name = "达利园蛋黄派"; products[6].price = 9.0F; products[6].labelnum =6911988006783;
            products[7].name = "娃哈哈八宝粥"; products[7].price = 3.50F; products[7].labelnum =6902083880781;
            products[8].name = "娃哈哈AD钙奶 220*4瓶"; products[8].price = 7.0F; products[8].labelnum =6902083881085;
            products[9].name = "好吃点香脆腰果饼干"; products[9].price = 2.90F; products[9].labelnum =6911988009777;
            products[10].name = "金丝猴奶糖 118克袋装"; products[10].price = 5.90F; products[10].labelnum = 6921681107564;
            products[11].name = "雀巢脆脆鲨威化饼干"; products[11].price = 23.0F; products[11].labelnum =69178780020570;
            products[12].name = "康师傅绿茶550ml装"; products[12].price = 2.60F; products[12].labelnum =6920459905166;
            products[13].name = "黄山毛尖"; products[13].price = 33.89F; products[13].labelnum =6931386400112;
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
            products[25].name = "《高等数学导论（下）》，中国科学技术大学出版社"; products[25].price = 37.50F; products[25].labelnum =9787312021428;
            products[26].name = "笔记本"; products[26].price = 6.60F; products[26].labelnum =6925550511032;
            products[27].name = "得力订书机"; products[27].price = 13F; products[27].labelnum =6921734903075;
            products[28].name = "led小台灯"; products[28].price = 33F; products[28].labelnum =6939020420663;
            products[29].name = "心相印纸面巾"; products[29].price = 5.20F; products[29].labelnum = 6903244984102;  
            return products;
         }
    }
}
