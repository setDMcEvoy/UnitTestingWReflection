using System;

namespace ReflectionTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Just run the unit tests

            Console.WriteLine("Hello World!");
        }
    }

    public class DoStuff
    {
        const string thoughts = "{0} thoughts here";

        private string privateProperty { get; set; } = "private property";
        public string publicProperty { get; set; } = "public property";

        private string getPrivateThoughts()
        {
            return string.Format(thoughts, "private");
        }

        public string getPublicThoughts()
        {
            return string.Format(thoughts, "public");
        }
    }

}
