﻿using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class ServiceOrderManagerTests
    {
        ServiceOrderManager _serviceOrderManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _serviceOrderManager = new ServiceOrderManager(new ServiceOrderAccessorFakes());
        }

        [TestMethod]
        public void TestGetAllServiceOrderCountPasses()
        {
            int expected = 3;
            int actual = _serviceOrderManager.GetALlServiceOrders().Count;

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestGetAllServiceOrderCountFails()
        {
            int expected = 1;
            int actual = _serviceOrderManager.GetALlServiceOrders().Count;

            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///  Tests that the UpdateServiceOrder method passes when a valid service order is provided.      
        /// </summary>
        /// <returns>
        ///   The number of rows affected by the update operation
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-18
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        [TestMethod]
        public void TestUpdateServiceOrderPasses()
        {
            // Arrange

            ServiceOrder serviceOrderToUpdate = new ServiceOrder()
            {
                Service_Order_ID = 100000,
                Critical_Issue = true,
                Service_Type_ID = "Engine Tune-up",
                Service_Description = "Perform engine tune-up"
            };

            // Act
            int rowsAffected = _serviceOrderManager.UpdateServiceOrder(serviceOrderToUpdate);

            // Assert
            Assert.AreEqual(1, rowsAffected); // Assuming the update was successful

        }
        /// <summary>
        /// Tests that the UpdateServiceOrder method throws an exception when a null service order is provided.
        /// </summary>
        /// <returns>
        ///   The number of rows affected by the update operation
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentNullException">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-18
        /// <br />
        /// <br />
        ///    UPDATER: Steven Sanchez
        /// <br />
        ///    UPDATED: 2024-02-28
        /// <br />
        ///     test for invalid service order ID
        /// </remarks>
        [TestMethod]
        public void TestUpdateNonExistingServiceOrderFails()
        {
            // Arrange
            ServiceOrder serviceOrderToUpdate = new ServiceOrder()
            {
                Service_Order_ID = 100010,  // Assuming this ID doesn't exist in the database
                Critical_Issue = true,
                Service_Type_ID = "Engine Tune-up",
                Service_Description = "Perform engine tune-up"
            };

            // Act
            int rowsAffected = _serviceOrderManager.UpdateServiceOrder(serviceOrderToUpdate);

            // Assert
            Assert.AreEqual(0, rowsAffected); // Assuming the update failed since service order doesn't exist
        }
        /// <summary>
        /// Tests that the CreateServiceOrder method passes.
        /// </summary>
        /// <returns>
        ///   The number of rows affected by the create operation
        /// </returns>
        /// <remarks>
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-03-12
        /// <br />
        /// <br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     
        /// </remarks>
        [TestMethod]
        public void TestCreateServiceOrderPasses()
        {
            // Arrange
            ServiceOrder_VM serviceOrderToCreate = new ServiceOrder_VM()
            {
                VIN = "1HGCM82633A987654",
                Service_Order_Version = 1,
                Service_Type_ID = "Engine Tune-up",
                Created_By_Employee_ID = 100001,
                Serviced_By_Employee_ID = 100002,
                Date_Started = DateTime.Now,
                Date_Finished = DateTime.Now,
                Is_Active = true,
                Critical_Issue = true
            };

            // Act
            bool rowsAffected = _serviceOrderManager.CreateServiceOrder(serviceOrderToCreate);

            // Assert
            Assert.AreEqual(true, rowsAffected);
        }

        /// <summary>
        /// Tests that the CreateServiceOrder method fails.
        /// </summary>
        /// <returns>
        ///   The number of rows affected by the create operation
        /// </returns>
        /// <remarks>
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-03-12
        /// <br />
        /// <br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     
        /// </remarks>
        [TestMethod]
        public void TestCreateServiceOrderFails()
        {

            ServiceOrder_VM serviceOrderToCreate = new ServiceOrder_VM()
            {
                Service_Order_ID = 100000, // Existing ID in the fakes
                VIN = "1HGCM82633A987654",
                Service_Order_Version = 1,
                Service_Type_ID = "Engine Tune-up",
                Created_By_Employee_ID = 100001,
                Serviced_By_Employee_ID = 100002,
                Date_Started = DateTime.Now,
                Date_Finished = DateTime.Now,
                Is_Active = true,
                Critical_Issue = true
            };

            // Act
            bool rowsAffected = _serviceOrderManager.CreateServiceOrder(serviceOrderToCreate);

            // Assert
            Assert.AreEqual(false, rowsAffected);
        }

        [TestMethod]
        public void TestSelectServiceOrderByServiceOrderIDReturnsAServiceOrder()
        {
            var expected = new ServiceOrder_VM()
            {
                VIN = "JTLZE4FEXB1123437"
            };
            var actual = _serviceOrderManager.SelectServiceOrderByServiceOrderID(100000);

            Assert.AreEqual(expected.VIN, actual.VIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSelectServiceOrderByServiceOrderIDReturnsAVehicle()
        {
            var actual = _serviceOrderManager.SelectServiceOrderByServiceOrderID(100010);
        }

        /// <summary>
        /// Tests that the GetAllCompleteServiceOrders method works.
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-03-05
        /// <br />
        /// </remarks>
        [TestMethod]
        public void TestGetAllCompleteServiceOrdersReturnsCorrectCount()
        {
            int expected = 2;
            int actual = _serviceOrderManager.GetAllCompleteServiceOrders().Count;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests that the GetAllInCompleteServiceOrders method works.
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-03-05
        /// <br />
        /// </remarks>
        [TestMethod]
        public void TestGetAllIncompleteServiceOrdersReturnsCorrectCount()
        {
            int expected = 1;
            int actual = _serviceOrderManager.GetAllIncompleteServiceOrders().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAllServiceTypesCountPasses()
        {
            // Arrange
            int expected = 2;

            // Act
            int actual = _serviceOrderManager.GetAllServiceTypes().Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAllServiceTypesCountFails()
        {
            // Arrange
            int expected = 3;

            // Act
            int actual = _serviceOrderManager.GetAllServiceTypes().Count;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
    }
}