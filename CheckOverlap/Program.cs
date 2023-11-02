using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/******************************************************************
* Jhohan Arias
* 02/11/2023
* Technical test Ormuco
*******************************************************************/

namespace CheckOverlap
{
    /**************************************************************************************************************************
    * Q1. program that accepts two lines (x1,x2) and (x3,x4) on the x-axis and returns whether they overlap
    ***************************************************************************************************************************/
    class Overlap
    {
        static void CheckOverlap(int[] line1, int[] line2) // recieve two arrays int and check if their overlaps
        {
            int x1 = line1[0];
            int x2 = line1[1];
            int x3 = line2[0];
            int x4 = line2[1];

            // Check all the possibilities according to the limits at every line
            if ((x1 <= x3 && x3 <= x2) || (x1 <= x4 && x4 <= x2) || (x3 <= x1 && x1 <= x4) || (x3 <= x2 && x2 <= x4)) 
                Console.WriteLine(" the line (" + x1 + "," + x2 + ") overlaps with the line (" + x3 + "," + x4 +")");
            else
       
                Console.WriteLine(" the line (" + x1 + "," + x2 + ") does NOT overlap with the line (" + x3 + "," + x4 + ")");
        }

        static void Main(string[] args)
        {
            //test 1
            int[] line1 = { 3, 5 };
            int[] line2 = { 2, 6 };
            //test 2
            int[] line3 = { 1, 5 };
            int[] line4 = { -3, 0 };

            //test 3
            int[] line5 = { 1, 5 };
            int[] line6 = { 2, 6 };
            //test 4
            int[] line7 = { 1, 5 };
            int[] line8 = { 6, 8 };

            CheckOverlap(line1, line2); 
            CheckOverlap(line3, line4);
            CheckOverlap(line5, line6);
            CheckOverlap(line7, line8);
            Console.ReadLine();
        }
    
    }
}
