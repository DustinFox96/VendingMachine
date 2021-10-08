using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class ProductItemTests
    {
        [TestMethod]
        [DataRow("Chip", "Crunch Crunch, Yummy!")]
        [DataRow("Candy", "Munch Munch, Yummy!")]
        [DataRow("Drink", "Glug Glug, Yummy!")]
        [DataRow("Gum", "Chew Chew, Yummy!")]
        public void MakeSound_Returns_Correct_String(string productType, string expected)
        {
            //arrange
            ProductItem productItem = new ProductItem("testItem", 0, productType);

            //act
            string actual = productItem.MakeSound();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("asdgasdgasdgasgd", "Yummy!")]
        [DataRow("", "Yummy!")]
        public void MakeSound_Handles_Unknown_Snack_Types(string productType, string expected)
        {
            //arrange
            ProductItem productItem = new ProductItem("testItem", 0, productType);

            //act
            string actual = productItem.MakeSound();

            //assert
            Assert.AreEqual(expected, actual);
        }

    }
}
