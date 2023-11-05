using System;

/******************************************************************
* Jhohan Arias
* 02/11/2023
* Technical test Ormuco
*******************************************************************/

namespace CheckOverlap
{
    /**************************************************************************************************************************
    * Q1. program that accepts two lines (startLine1,endLine1) and (startLine2,endLine2) on the x-axis and returns whether they overlap
    ***************************************************************************************************************************/
    public class Overlap
    {
        public string CheckOverlap(int[] line1, int[] line2) // recieve two arrays int and check if their overlaps
        {
            try
            {
                // Check all the possibilities according to the limits at every line
                if (line1 == null || line2 == null)
                    return "There is a null line";
                else
                {
                    int startLine1 = line1[0];
                    int endLine1 = line1[1];
                    int startLine2 = line2[0];
                    int endLine2 = line2[1];

                    if ((startLine1 <= startLine2 && startLine2 <= endLine1) || (startLine1 <= endLine2 && endLine2 <= endLine1) || (startLine2 <= startLine1 && startLine1 <= endLine2) || (startLine2 <= endLine1 && endLine1 <= endLine2))
                        return " the line (" + startLine1 + "," + endLine1 + ") overlaps with the line (" + startLine2 + "," + endLine2 + ")";
                    else
                        return " the line (" + startLine1 + "," + endLine1 + ") does NOT overlap with the line (" + startLine2 + "," + endLine2 + ")";

                }
            }
            catch (Exception ex)  // Capture any error produced in the procedure
            {
                Console.WriteLine(ex.Message, ex.GetType().ToString());
                return "Invalid Data";
            }
        }

        static void Main(string[] args)
        {

            Overlap overlap = new Overlap();
            //case 1
            int[] line1 = { 3, 5 };
            int[] line2 = { 2, 6 };
            //case 2
            int[] line3 = { 1, 5 };
            int[] line4 = { -3, 0 };

            //case 3
            int[] line5 = { 1, 5 };
            int[] line6 = { 2, 6 };
            //case 4
            int[] line7 = { 1, 5 };
            int[] line8 = { 6, 8 };

            Console.WriteLine(overlap.CheckOverlap(line1, line2));
            Console.WriteLine(overlap.CheckOverlap(line3, line4));
            Console.WriteLine(overlap.CheckOverlap(line5, line6));
            Console.WriteLine(overlap.CheckOverlap(line7, line8));
            Console.ReadLine();
        }
    
    }
}
