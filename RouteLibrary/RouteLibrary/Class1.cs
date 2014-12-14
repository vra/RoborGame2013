using System;
using System.Collections.Generic;
using System.Text;
using Map;

namespace RouteLibrary
{
    public class Class1
    {
        public static int[,] p = new int[45, 45];               //
        public static int postCount;           //已发现障碍物个数
        public static int[,] postion = new int[45, 2];

        public static void SetPAndPosition()
        {

            for (int i = 0; i < 45; i++)
                for (int j = 0; j < 45; j++)
                    p[i, j] = -1;

            //postion[i,0]表示x坐标，postion[i,1]表示y坐标

            postion[0, 0] = 0; postion[0, 1] = 0;
            postion[1, 0] = 9; postion[1, 1] = 0;
            postion[2, 0] = 16; postion[2, 1] = 0;
            postion[3, 0] = 23; postion[3, 1] = 0;
            postion[4, 0] = 29; postion[4, 1] = 0;
            postion[5, 0] = 36; postion[5, 1] = 0;
            postion[6, 0] = 43; postion[6, 1] = 0;
            postion[7, 0] = 49; postion[7, 1] = 0;


            postion[8, 0] = 9; postion[8, 1] = 7;
            postion[9, 0] = 23; postion[9, 1] = 7;
            postion[10, 0] = 29; postion[10, 1] = 7;
            postion[11, 0] = 43; postion[11, 1] = 7;
            postion[12, 0] = 49; postion[12, 1] = 7;


            postion[13, 0] = 9; postion[13, 1] = 10;
            postion[14, 0] = 23; postion[14, 1] = 10;
            postion[15, 0] = 29; postion[15, 1] = 10;
            postion[16, 0] = 43; postion[16, 1] = 10;
            postion[17, 0] = 49; postion[17, 1] = 10;



            postion[18, 0] = 9; postion[18, 1] = 13;
            postion[19, 0] = 23; postion[19, 1] = 13;
            postion[20, 0] = 29; postion[20, 1] = 13;
            postion[21, 0] = 43; postion[21, 1] = 13;
            postion[22, 0] = 49; postion[22, 1] = 13;


            postion[23, 0] = 12; postion[23, 1] = 19;
            postion[24, 0] = 16; postion[24, 1] = 19;
            postion[25, 0] = 23; postion[25, 1] = 19;
            postion[26, 0] = 29; postion[26, 1] = 19;
            postion[27, 0] = 36; postion[27, 1] = 19;
            postion[28, 0] = 43; postion[28, 1] = 19;
            postion[29, 0] = 49; postion[29, 1] = 19;


            postion[30, 0] = 12; postion[30, 1] = 25;
            postion[31, 0] = 16; postion[31, 1] = 25;
            postion[32, 0] = 23; postion[32, 1] = 25;
            postion[33, 0] = 29; postion[33, 1] = 25;
            postion[34, 0] = 36; postion[34, 1] = 25;
            postion[35, 0] = 43; postion[35, 1] = 25;
            postion[36, 0] = 49; postion[36, 1] = 25;
            postion[37, 0] = 9; postion[37, 1] = 19;
            postion[38, 0] = 9; postion[38, 1] = 25;
            postion[39, 0] = 4; postion[39, 1] = 25;




            /*         postion[40]=new int[]{};      postion[41]=new int[]{};
                       postion[42]=new int[]{};      postion[43]=new int[]{};
                       postion[43]=new int[]{};    
        
           */
        }

        public static void CreateNode(int lastnote, int nextnote)//在两点之间设置障碍物标志,若lastnote=nextnote，则说明障碍物在坐标点上
        {

            postCount++;

            if (lastnote != nextnote)
            {
                for (int i = 0; i <= (40 + postCount); i++)
                {

                    Map.Class1.map[i, (40 + postCount)] = 1000;
                    Map.Class1.map[40 + postCount, i] = 1000;
                }
                Map.Class1.map[lastnote, nextnote] = 1000;
                Map.Class1.map[nextnote, lastnote] = 1000;

            }
            else
            {
                int node = lastnote;
                for (int i = 0; i <= 41; i++)
                {
                    Map.Class1.map[i, node] = 1000;
                    Map.Class1.map[node, i] = 1000;
                }
            }
        }


        public static void Dijkstra(int num0)//寻找较优路径，并将中途节点号存入p[目标节点编号,0:i-1], i为所需经过的节点个数,num0为起始点编号
        {
            int max;
            max = 45;
            int[] s = new int[45];
            float[] d = new float[45];

            int i, j, w;

            int k = new int();

            float min;
            for (i = 0; i < 45; i++)
            {
                d[i] = 0;
                s[i] = 0;

            }
            for (i = 0; i < 45; i++)
            {
                for (j = 0; j < 45; j++)
                {
                    p[i, j] = -1;
                }
            }
            for (i = 1; i < max; i++)
            {
                

                d[i] = Map.Class1.map[num0, i];
                if (d[i] != 1000)
                {
                    p[i, 0] = num0;
                    p[i, 1] = i;
                    p[i, 2] = -1;
                }
            }

            s[num0] = 1;
            d[num0] = 0;

            for (i = 1; i < max; i++)
            {
                min = 1000;
                for (j = 1; j < max; j++)
                {
                    if ((s[j] == 0) && (d[j] < min))
                    {
                        min = d[j];
                        k = j;
                    }
                }
                s[k] = 1;

                for (j = 0; j < 45; j++)
                {
                    if ((s[j] == 0) && ((d[k] + Map.Class1.map[k, j]) < d[j]))
                    {
                        d[j] = d[k] + Map.Class1.map[k, j];
                        for (w = 0; p[k, w] != (-1); w++)
                        {
                            p[j, w] = p[k, w];
                        }
                        p[j, w] = j;
                        p[j, w + 1] = (-1);
                    }
                }

            }

        }
    }
}



