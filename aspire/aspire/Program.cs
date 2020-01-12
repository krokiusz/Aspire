using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

namespace aspire
{
    class Program
    {
        private static string[] servicesArr =
        {
            "Abroad boom!",
            "Grumpy third party driver",
            "They stole my iphone XX",
            "Windscreen washer assistance"
        };

        static void Main(string[] args)
        {
            TestMethods(CheckServicesStringCorrectness
                (""), new List<Result> {});
            TestMethods(CheckServicesStringCorrectness
                ("Abroad boom!"), new List<Result> {});
            TestMethods(CheckServicesStringCorrectness
                ("Grumpy third party driver"), new List<Result> {});
            TestMethods(CheckServicesStringCorrectness
                ("They stole my iphone XX"), new List<Result> {});
            TestMethods(CheckServicesStringCorrectness
                ("Windscreen washer assistance"), new List<Result> {});
            TestMethods(CheckServicesStringCorrectness
                ("Abroad boom! Grumpy third party driver They stole my iphone XX Windscreen washer assistance "), new List<Result> {});
            TestMethods(CheckServicesStringCorrectness
                ("Windscreen washer assistance Windscreen washer assistance"), new List<Result> {Result.Repeat });
            TestMethods(CheckServicesStringCorrectness
                ("Abrooad boom! Grumpy third party driver They stole my iphone XX Windscreen washer assistance "), new List<Result> { Result.Misspelling});
            TestMethods(CheckServicesStringCorrectness
                ("no czesc"), new List<Result> { Result.Misspelling,});
            TestMethods(CheckServicesStringCorrectness
                ("Abroad boom! Grumpy third par;ty driver Abroad boom!"), new List<Result> { Result.Misspelling, Result.Repeat });

            System.Console.ReadLine();
        }

        public static void TestMethods(List<Result> actualValue, List<Result> expectedValue)
        {
            if(CompareErrorsLists(actualValue, expectedValue))
                System.Console.WriteLine("Everything's correct\n");
            else
                System.Console.WriteLine("Something is no yes\n");
        }

        /// <summary>
        ///     Compares error list given by program to manually added list of expected errors
        /// </summary>
        /// <param name="actualValue"> Error list given by program. </param>
        /// <param name="expectedValue"> Manually added list of expected errors. </param>
        /// <returns> True if lists are the same. </returns>
        private static bool CompareErrorsLists(List<Result> actualValue, List<Result> expectedValue)
        {
            actualValue.Sort();
            expectedValue.Sort();

            if (actualValue.Count != expectedValue.Count)
                return false;

            for(int i = 0; i > actualValue.Count; i++)
            {
                if (actualValue[i] != expectedValue[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     Checks if givenServicesString is proper - names of services do not occur more than once and if names are correctly writen.
        /// </summary>
        /// <param name="givenServicesString"> String cointaining services displayed on screen. </param>
        /// <returns> List of errors. </returns>
        public static List<Result> CheckServicesStringCorrectness(string givenServicesString)
        {
         List<Result> errorsList = new List<Result>{};

        string replacedString = EraseCorrectServicesNames(givenServicesString);
            bool errorOcured = false;

            if (replacedString != "")
            {
                System.Console.WriteLine($"W jednej lub wiecej nazw jest błąd: \n\"{replacedString}\"\n");
                errorsList.Add(Result.Misspelling);
            }

            foreach (string s in servicesArr)
            {
                if (CheckStringOccurance(givenServicesString, s) > 1)
                {
                    System.Console.WriteLine($"Nazwa \"{s}\" wystąpiła więcej niż jeden raz.\n");
                    errorOcured = true;
                }
            }

            if (errorOcured)
                errorsList.Add(Result.Repeat);

            return errorsList;
        }

        /// <summary>
        ///     Erases services names that are written correctly. 
        /// </summary>
        /// <param name="givenString"> String to check. </param>
        /// <returns> String cointaining wrong names. </returns>
        public static string EraseCorrectServicesNames(string givenString)
        {
            StringBuilder serviceString = new StringBuilder(givenString);

            foreach (string service in servicesArr)
                if (serviceString.ToString().Trim().Contains(service))
                    serviceString = serviceString.Replace(service, "");

            return serviceString.ToString().Trim();
        }

        public static int CheckStringOccurance(string givenString, string service)
        {
            return Regex.Matches(givenString, service).Count;
        }
    }
    enum Result { Passed, Repeat, Misspelling };
}