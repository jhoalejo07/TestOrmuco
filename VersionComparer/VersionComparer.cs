using System;

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
    public class CheckVersions
    {
        public string BiggerVersion(string version1, string version2)
        {
            try
            {
                //if any argument is null, return the error code
                if (string.IsNullOrEmpty(version1) || string.IsNullOrEmpty(version2))
                {
                  return "There is a null version";
                }

               else
              {

               //Creating arrays for every string to hold every character split by .
                string[] vers1 = version1.Split('.');
                string[] vers2 = version2.Split('.');

                // check if the string contains only character numbers in array 1
                if (!AreAllItemsNumbers(vers1) || !AreAllItemsNumbers(vers2))
                    return "Invalid Input";


                // Determining which version has the maximum length
                int maxLength = vers1.Length > vers2.Length ? vers1.Length : vers2.Length;

                //    //creating a cycle for comparing the two arrays, position by position converted to number to see which is bigger
                for (int i = 0; i < maxLength; i++)
                {
                    int part1 = (i < vers1.Length) ? int.Parse(vers1[i]) : 0; // convert every item to a number, validating if the iteration number is bigger than the array´s length return 0
                    int part2 = (i < vers2.Length) ? int.Parse(vers2[i]) : 0;
                    if (part1 > part2)
                    {
                        return "The version: " + version1 + " is greater than " + version2;
                    }
                    if (part1 < part2)
                    {
                        return "The version: " + version1 + " is lower than " + version2;
                    }

                }
               // if they are equals called the function to print out with value 0 
                  return "The version: " + version1 + " and version: " + version2 + " are equal";
                }

            }
            catch (Exception ex)  // Capture any error produced in the procedure
            {
                Console.WriteLine(ex.Message, ex.GetType().ToString());
                return "error";
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


        public static void Main(string[] args)
        {
            // Scenarios
            CheckVersions vs = new CheckVersions();
            Console.WriteLine(vs.BiggerVersion("2.5", "2.0.5"));
            Console.WriteLine(vs.BiggerVersion("1.1", "1.1.3"));
            Console.WriteLine(vs.BiggerVersion("1.0.5", "1.0.5"));
            Console.WriteLine(vs.BiggerVersion("1.0.5", "1.0."));
            Console.WriteLine(vs.BiggerVersion(null, "1.0."));
            Console.WriteLine(vs.BiggerVersion("1.0.", null));
            Console.WriteLine(vs.BiggerVersion("hola", null));
            Console.WriteLine(vs.BiggerVersion(null, "hola"));
            Console.WriteLine(vs.BiggerVersion("1.2", null));
            Console.WriteLine(vs.BiggerVersion(null, "2.1"));
            Console.WriteLine(vs.BiggerVersion(null, null));
            Console.WriteLine(vs.BiggerVersion("1.w.5", "1.0."));
            Console.ReadLine();
        }
    }

}

