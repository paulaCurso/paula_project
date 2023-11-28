using JustEatNavarro.Controllers;
using JustEatNavarro.Models;
using NUnit.Framework;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace JustEatNavarro.Tests.Controllers
{
    [TestFixture]
    public class ClientesControllerTest
    {
        [Test]
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
                Assert.That(result, Is.InstanceOf(typeof(HttpStatusCodeResult)));
                var statusCodeResult = (HttpStatusCodeResult)result;
                Assert.That(statusCodeResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
            }
        }
    }
}
