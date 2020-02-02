using System; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_Art.Controllers;
using System.Web.Mvc;

namespace Art_Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void About()
        {
            var controller = new HomeController();
            var result = controller.About() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Login()
        {
            var controller = new HomeController();
            var result = controller.Login() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Contact()
        {
            var controller = new HomeController();
            var result = controller.Contact() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Submit()
        {
            var controller = new HomeController();
            var result = controller.Submit() as ViewResult;
            Assert.IsNotNull(result);

        }

         [TestMethod]
        public void Approve()
        {
            var controller = new MenuController();
            var result = controller.Approve() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Admin()
        {
            var controller = new MenuController();
            var result = controller.Admin() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Artist()
        {
            var controller = new MenuController();
            var result = controller.Artist() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void collectorid()
        {
            var controller = new MenuController();
            var result = controller.collectorid() as ViewResult;
            Assert.IsNotNull(result);

        }
        










    }
}
