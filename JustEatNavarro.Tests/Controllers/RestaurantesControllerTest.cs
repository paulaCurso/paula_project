using JustEatNavarro.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using JustEatNavarro.Models;
using System.Linq;

namespace JustEatNavarro.Tests.Controllers
{
    [TestClass]
    public class RestaurantesControllerTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void Details()
        {
            // Arrange
            RestaurantesController controller = new RestaurantesController();

            // Act
            var result = controller.Details("lele");

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            var statusCodeResult = (HttpStatusCodeResult)result;
            Assert.AreEqual(statusCodeResult.StatusCode, (int)HttpStatusCode.BadRequest);

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
                Assert.IsInstanceOfType(result, typeof(ViewResult));
                var viewResult = (ViewResult)result;
                Assert.IsNotNull(viewResult.Model);
                Assert.IsInstanceOfType(viewResult.Model, typeof(Restaurante));
                Assert.AreEqual(restaurante.Id, ((Restaurante)viewResult.Model).Id);
            }
        }
    }
}
