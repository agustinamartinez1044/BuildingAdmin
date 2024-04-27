﻿using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using IServices;
using Moq;
using DTO.In;
using DTO.Out;

namespace TestWebApi
{
    [TestClass]
    public class TestInvitationController
    {

        private Mock<IInvitationServices> _invitationServicesMock;

        [TestMethod]
        public void TestCreateInvitation()
        {
            _invitationServicesMock = new Mock<IInvitationServices>(MockBehavior.Strict);
            Invitation invitationCreated = new Invitation()
            {
                Id = 11,
                Email = "correoInvitado@correo.com",
                Status = Domain.DataTypes.InvitationStatus.Pending,
                ExpirationDate = DateTime.Now.AddDays(3)
            };
            _invitationServicesMock.Setup(r => r.CreateInvitation(It.IsAny<Invitation>())).Returns(invitationCreated);
            InvitationController controller = new InvitationController(_invitationServicesMock.Object);
            var input = new CreateInvitationInput()
            {
                Email = "correoInvitado@correo.com",
                Name = "Nombre Invitado",
                ExpirationDate = DateTime.Now.AddDays(3)
            };

            var result = controller.CreateInvitation(input);
            var okResult = result as OkObjectResult;
            var mewInvitationResponse = okResult.Value as CreateInvitationOutput;


            _invitationServicesMock.VerifyAll();
            Assert.AreEqual(11, mewInvitationResponse.InvitationId);
        }
    }
}
