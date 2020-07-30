using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pizza_Exercise.Models;

namespace UnitTest
{
    [TestClass]
    public class WebTest
    {
        [TestMethod]
        public void EqualsTestMethod()
        {
            Combination combination = new Combination { Toppings = new System.Collections.Generic.List<string> { "Pepperoni" } };
            Combination combination1 = new Combination { Toppings = new System.Collections.Generic.List<string> { "Pepperoni" } };
            Combination combination2 = new Combination { Toppings = new System.Collections.Generic.List<string> { "Pepperoni", "Italian Sausage" } };
            Combination combination3 = new Combination { Toppings = new System.Collections.Generic.List<string> { "Green Peppers" } };
            Combination combination4 = new Combination { Toppings = new System.Collections.Generic.List<string> { "Pepperoni", "Italian Sausage" } };
            Combination combination5 = new Combination { Toppings = new System.Collections.Generic.List<string> { "Green Peppers", "Italian Sausage" } };

            Assert.IsTrue(combination.Equals(combination1));
            Assert.IsFalse(combination.Equals(combination2));
            Assert.IsTrue(combination2.Equals(combination4));
            Assert.IsFalse(combination2.Equals(combination5));
            Assert.IsFalse(combination3.Equals(combination5));
        }
    }
}
