using System;
using System.Collections.Generic;
using System.Text;

namespace Map
{
    public class Class1
    {
        public static float[,] map = new float[45, 45];

            
        public static void CreateMap()   
        {
            int i, j;


            for (i = 0; i < 45; i++)
            {
                for (j = 0; j < 45; j++)
                {
                    map[i, j] = 1000;
                }
            }
            map[0, 1] = 9; map[1, 0] = 9; map[1, 2] = 7; map[1, 8] = 4; map[2, 1] = 7;
            map[2, 3] = 7; map[3, 2] = 7; map[3, 9] = 4; map[3, 4] = 6;
            map[4, 3] = 6; map[4, 10] = 4; map[4, 5] = 7; map[5, 4] = 7;
            map[5, 6] = 7; map[6, 5] = 7; map[6, 11] = 4; map[6, 7] = 6;
            map[7, 6] = 6; map[7, 12] = 4; map[8, 1] = 4; map[8, 13] = 6;
            map[9, 3] = 4; map[9, 10] = 6; map[9, 14] = 6; map[10, 4] = 4;
            map[10, 9] = 6; map[10, 15] = 6; map[11, 6] = 4; map[11, 12] = 6;
            map[11, 16] = 6; map[12, 11] = 6; map[12, 7] = 4; map[12, 17] = 6;
            map[13, 8] = 6; map[13, 18] = 6; map[14, 9] = 6; map[14, 19] = 6; map[14, 15] = 6;
            map[15, 14] = 6; map[15, 10] = 6; map[15, 20] = 6; map[16, 11] = 6; map[16, 21] = 6;
            map[16, 17] = 6; map[18, 13] = 6; map[18, 37] = 3; map[19, 20] = 6; map[19, 14] = 6;
            map[19, 25] = 3; map[20, 15] = 6; map[20, 19] = 6; map[20, 26] = 3; map[21, 16] = 6;
            map[21, 22] = 6; map[22, 21] = 6; map[22, 17] = 6; map[22, 28] = 3;
            map[37, 18] = 3; map[37, 23] = 3; map[37, 38] = 6; map[23, 37] = 3; map[23, 30] = 6;
            map[23, 24] = 4; map[24, 23] = 4; map[24, 31] = 6; map[24, 25] = 7; map[25, 24] = 7;
            map[25, 19] = 3; map[25, 32] = 6; map[25, 26] = 6; map[26, 20] = 3; map[26, 25] = 6;
            map[26, 33] = 6; map[26, 27] = 7; map[27, 26] = 7; map[27, 34] = 6; map[27, 28] = 7;
            map[28, 27] = 7; map[28, 21] = 3; map[28, 35] = 6; map[28, 29] = 6; map[29, 28] = 6;
            map[29, 22] = 3; map[29, 36] = 6; map[38, 37] = 6; map[38, 30] = 3; map[30, 23] = 6;
            map[30, 31] = 4; map[31, 30] = 4; map[31, 24] = 6; map[31, 32] = 7; map[32, 31] = 7;
            map[32, 25] = 6; map[32, 33] = 6; map[33, 32] = 6; map[33, 26] = 6; map[33, 34] = 7;
            map[34, 33] = 7; map[34, 27] = 6; map[34, 35] = 7; map[35, 34] = 7; map[35, 36] = 6;
            map[35, 28] = 6; map[36, 35] = 6; map[36, 29] = 6; map[38, 37] = 6; map[38, 30] = 3;
            map[39, 40] = 6; map[39, 38] = 4; map[40, 39] = 6; map[40, 37] = 4; map[21, 28] = 3;
            map[28, 21] = 3;



        }
    }
}

