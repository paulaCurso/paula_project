using JustEatNavarro.Controllers;
using System.Net;
using System.Web.Mvc;
using JustEatNavarro.Models;
using System.Linq;
using NUnit.Framework;

namespace JustEatNavarro.Tests.Controllers
{
    [TestFixture]
    public class RestaurantesControllerTest
    {
        [Test]
        public void Details()
        {
            // Arrange
            RestaurantesController controller = new RestaurantesController();

            // Act
            var result = controller.Details("lele");

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(HttpStatusCodeResult)));
            var statusCodeResult = (HttpStatusCodeResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));

            Restaurante restaurante = null;
            using (var db = new JustEatNavarroEntities())
            {
                restaurante = db.Restaurante.FirstOrDefault();
            }

            if (restaurante == null)
            {
                // Act
                result = controller.Details(restaurante.Id.ToString());

                // Assert
                Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
                var viewResult = (ViewResult)result;
                Assert.IsNotNull(viewResult.Model);
                Assert.That(viewResult.Model, Is.InstanceOf(typeof(Restaurante)));
                Assert.That(restaurante.Id, Is.EqualTo(((Restaurante)viewResult.Model).Id));
            }
        }
    }
}
