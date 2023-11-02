using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

/******************************************************************
* Jhohan Arias
* 02/11/2023
* Technical test Ormuco
*******************************************************************/

namespace VersionComparer
{
    /**************************************************************************************************************************
    * Q2. library that accepts 2 version string as input and returns whether one is greater than, equal, or less than the other
    ***************************************************************************************************************************/
    class CheckVersions
    {
        public static void BiggerVersion(string version1, string version2)
        {
            try
            {
                //if any argument is null, return the error code
                if (string.IsNullOrEmpty(version1) || string.IsNullOrEmpty(version2))
                    Results(version1, version2, 666);
                else
                {

                    //Creating arrays for every string to hold every character split by .
                    string[] vers1 = version1.Split('.');
                    string[] vers2 = version2.Split('.');

                    // check if the string contains only character numbers in array 1
                    if (!AreAllItemsNumbers(vers1) || !AreAllItemsNumbers(vers2))
                        Results(version1, version2, 111);
                    //return 111;

                    // Determining which version has the maximum length
                    int maxLength = vers1.Length > vers2.Length ? vers1.Length : vers2.Length;
                    Boolean flag = false;

                    //creating a cycle for comparing the two arrays, position by position converted to number to see which is bigger
                    for (int i = 0; i < maxLength; i++)
                    {
                        int part1 = (i < vers1.Length) ? int.Parse(vers1[i]) : 0; // convert every item to a number, validating if the iteration number is bigger than the array´s length return 0
                        int part2 = (i < vers2.Length) ? int.Parse(vers2[i]) : 0;
                        if (part1 > part2)
                        {
                            Results(version1, version2, 1); // called the function to print out and change the flag, they aren´t equals
                            flag = true;
                            break;
                        }
                        if (part1 < part2)
                        {
                            Results(version1, version2, -1);
                            flag = true;
                            break;
                        }//return -1;

                    }
                    if (!flag) // if they are equals called the function to print out with value 0 
                        Results(version1, version2, 0); //return 0;
                }

            }
            catch (Exception ex)  // Capture any error produced in the procedure
            {
                Console.WriteLine(ex.Message, ex.GetType().ToString());
                //return;
            }

        }

        static bool AreAllItemsNumbers(string[] array)
        {
            foreach (string item in array)
            {
                if (!int.TryParse(item, out _))
                {
                    return false; // If parsing as int fails, the item is not a number.
                }
            }
            return true; // All items are valid numbers.
        }

        static void Results(string version1, string version2, int result)
        {
            if (result == 1)
                Console.WriteLine("The version: " + version1 + " is greater than " + version2);
            else if (result == -1)
                Console.WriteLine("The version: " + version1 + " is lower than " + version2);
            else if (result == 0)
                Console.WriteLine("The version: " + version1 + " and version: " + version2 + " are equal ");
            else if (result == 666)
                Console.WriteLine("Invalid input (null value) ");
            else if (result == 111)
                Console.WriteLine("Some item in the version string is not a number ");

        }

        public static void Main(string[] args)
        {
            // Test cases
            BiggerVersion("2.5", "2.0.5");
            BiggerVersion("1.1", "1.1.3");
            BiggerVersion("1.0.5", "1.0.5");
            BiggerVersion("1.0.5", "1.0.");
            BiggerVersion(null, "1.0.");
            BiggerVersion("1.0.", null);
            BiggerVersion("hola", null);
            BiggerVersion(null, "hola");
            BiggerVersion("1.2", null);
            BiggerVersion(null, "2.1");
            BiggerVersion(null, null);
            BiggerVersion("1.w.5", "1.0");
            Console.ReadLine();
        }
    }

}

