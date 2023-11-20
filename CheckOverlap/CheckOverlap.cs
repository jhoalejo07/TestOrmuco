using System;

/******************************************************************
* Jhohan Arias
* 06/11/2023
* Technical test Ormuco
*******************************************************************/

namespace CheckOverlap
{
    /**************************************************************************************************************************
    * Q1. program that accepts two lines (startLine1,endLine1) and (startLine2,endLine2) on the x-axis and returns whether they overlap
    * 
    * 
    * SORRY I didn´t caught what means "if lines are given in disorder."
    ***************************************************************************************************************************/
    public class Overlap
    {
        public string CheckOverlap(int[] line1, int[] line2) // recieve two arrays int and check if their overlaps
        {
            try
            {
                // Check if the lines are not null or more than begin and end points. (NEW validation)
                if (line1 == null || line2 == null || line1.Length != 2 || line2.Length != 2)
                    throw new ArgumentException("One line is null or has no accurate length"); // raise a specific error
                else
                {
                    int startLine1 = line1[0];
                    int endLine1 = line1[1];
                    int startLine2 = line2[0];
                    int endLine2 = line2[1];

                    //check the numbers are in the int range. (NEW validation)
                    if (startLine1 < int.MinValue || startLine1 > int.MaxValue ||
                        endLine1 < int.MinValue || endLine1 > int.MaxValue ||
                        startLine2 < int.MinValue || startLine2 > int.MaxValue ||
                         endLine2 < int.MinValue || endLine2 > int.MaxValue)
                        throw new ArgumentOutOfRangeException("One number is out of range for a int value ");// raise a specific error

                    // I am checking that Line2's start or end are on Line 1: stL2 is between line1 or endL2 is between line1
                    // or  Line1's start or end are on Line 2: stL2 is between line1 or endL2 is between line1
                    if ((startLine1 <= startLine2 && startLine2 <= endLine1) || (startLine1 <= endLine2 && endLine2 <= endLine1) || (startLine2 <= startLine1 && startLine1 <= endLine2) || (startLine2 <= endLine1 && endLine1 <= endLine2))
 
                    // it Should  be more practical if I chech only every border. Ex, startL1 with endL2 and endL1 with startL2
                    //if (startLine1 <= endLine2 && endLine1 >= startLine2)
                        return " the line (" + startLine1 + "," + endLine1 + ") overlaps with the line (" + startLine2 + "," + endLine2 + ")";
                    else
                        return " the line (" + startLine1 + "," + endLine1 + ") does NOT overlap with the line (" + startLine2 + "," + endLine2 + ")";



                }
            }
            catch (ArgumentOutOfRangeException ex) // Capture error produced a number out of range
            {
               // Console.WriteLine(ex.Message);
                return ex.Message;
            }
            catch (ArgumentException ex) // Capture error produced a null
            {
               // Console.WriteLine(ex.Message);
                return ex.Message;
            }

            catch (Exception ex)  // Capture any error produced in the procedure
            {
                Console.WriteLine(ex.Message, ex.GetType().ToString());
                return ex.Message;
            }

        }

        static void Main(string[] args)
        {

            Overlap overlap = new Overlap();
            //case 1
            int[] line2 = { 3, 5, 8 };
            int[] line1 = { 2, int.MaxValue };

            //int[] line1 = { int.MaxValue, 5 };
            //int[] line2 = { 3, 6 };

            //case 2
            int[] line3 = { -3, 5 };
            int[] line4 = { 1, 0 };

            //case 3
            int[] line5 = null;
            int[] line6 = { 2, 6 };
            //case 4
            int[] line7 = { 1, 5 };
            int[] line8 = { 6, -2 };

     
            Console.WriteLine(overlap.CheckOverlap(line1, line2));
            Console.WriteLine(overlap.CheckOverlap(line3, line4));
            Console.WriteLine(overlap.CheckOverlap(line5, line6));
            Console.WriteLine(overlap.CheckOverlap(line7, line8));
            Console.ReadLine();
        }
    
    }
}
