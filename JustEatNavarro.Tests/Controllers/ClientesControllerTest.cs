using JustEatNavarro.Controllers;
using JustEatNavarro.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace JustEatNavarro.Tests.Controllers
{
    [TestClass]
    public class ClientesControllerTest
    {
        [TestMethod]
        public void Edit()
        {
            // Arrange
            ClientesController controller = new ClientesController();
            Cliente cliente = null;
            using (var db = new JustEatNavarroEntities())
            {
                cliente = db.Cliente.FirstOrDefault(c => !string.IsNullOrEmpty(c.NombreLogin));
                cliente.NombreLogin = "otro";
            }
            if (cliente != null)
            {
                // Act
                var result = controller.Edit(cliente);

                // Assert
                Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
                var statusCodeResult = (HttpStatusCodeResult)result;
                Assert.AreEqual(statusCodeResult.StatusCode, (int)HttpStatusCode.Unauthorized);
            }
        }
    }
}
