using ReflectionTest;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace XUnitTestProject
{
    public class TestFixture
    {
        [Fact]
        public void UnitTest()
        {
            DoStuff instance = new DoStuff();

            var allMethods = typeof(DoStuff).GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);
            var privateMethods = typeof(DoStuff).GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
            var publicMethods = typeof(DoStuff).GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

            foreach (var method in allMethods)
            {
                Console.WriteLine(string.Format("Method: {0}, with return type: {1}", method.Name, method.ReturnType.AssemblyQualifiedName));
            }

            Assert.True(allMethods.Length == 6);
            Assert.True(privateMethods.Length == 3);
            Assert.True(publicMethods.Length == 3);
        }

        [Fact]
        public void CallPrivateMethod()
        {
            DoStuff instance = new DoStuff();

            var privateMethod = typeof(DoStuff).GetMethod("getPrivateThoughts", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);

            var privateResult = privateMethod.Invoke(instance, null);

            var returnType = privateMethod.ReturnType;

            var resultString = (returnType.Name == "String") ? Convert.ToString(privateResult) : "";

            Assert.True(resultString == "private thoughts here");
        }

        [Fact]
        public void EditPrivateProperty()
        {
            var instance = new DoStuff();

            PropertyInfo privateProp = typeof(DoStuff).GetProperty("privateProperty", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

            var privateValue = (string)privateProp.GetGetMethod(true).Invoke(instance, null);

            //var privateValue = typeof(DoStuff)
            //    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public)
            //    .Where(x => x.Name.StartsWith("get_")).First()
            //    .Invoke(instance, null);

            Assert.True(privateValue == "private property");

            privateProp.SetValue(instance, "Break all the rules");

            privateValue = (string)privateProp.GetGetMethod(true).Invoke(instance, null);

            Assert.True(privateValue == "Break all the rules");
        }
    }
}
