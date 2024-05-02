﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TestDomain
{
    [TestClass]
    public class TestOwner
    {

        Owner owner;

        [TestInitialize]
        public void Setup()
        {
            owner = new Owner();
        }

        [TestMethod]
        public void TestGetId()
        {
            owner.Id = 1;
            Assert.AreEqual(1, owner.Id);
        }

        [TestMethod]
        public void TestSetId()
        {
            owner.Id = 2;
            Assert.AreEqual(2, owner.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeId()
        {
            owner.Id = -1;
        }

        [TestMethod]
        public void TestName()
        {
            owner.Name = "Norberto";
            Assert.AreEqual("Norberto", owner.Name);
        }

        [TestMethod]
        public void TestModifyName()
        {
            owner.Name = "Norberto";
            owner.Name = "Magela";
            Assert.AreEqual("Magela", owner.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEmptyName()
        {
            owner.Name = String.Empty;
        }

        [TestMethod]
        public void TestLastName()
        {
            owner.LastName = "Rodriguez";
            Assert.AreEqual("Rodriguez", owner.LastName);
        }

        [TestMethod]
        public void TestModifyLastName()
        {
            owner.LastName = "Rodriguez";
            owner.LastName = "Gonzalez";
            Assert.AreEqual("Gonzalez", owner.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEmptyLastName()
        {
            owner.LastName = String.Empty;
        }

        [TestMethod]
        public void TestEmail()
        {
            owner.Email = "correo@mail.com";
            Assert.AreEqual("correo@mail.com", owner.Email);
        }

        [TestMethod]
        public void TestModifyEmail()
        {
            owner.Email = "correo@mail.com";
            owner.Email = "correo2@mail.com";
            Assert.AreEqual("correo2@mail.com", owner.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEmptyEmail()
        {
            owner.Email = String.Empty;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestInvalidEmail()
        {
            owner.Email = "probando";
        }

    }
}