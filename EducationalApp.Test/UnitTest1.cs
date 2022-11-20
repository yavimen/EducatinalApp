using EducatinalApp.ColorsHelper;
using System.Drawing;
namespace EducationalApp.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Color col = Color.FromArgb(156, 255, 56);
            Color res = ColorHelper.ChangeSaturation((float)0.25, col);

            Assert.AreEqual(col, res);
        }
    }
}