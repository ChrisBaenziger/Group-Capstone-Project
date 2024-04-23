﻿using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    /// 
    ///     Data access class for ServiceOrder.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Steven Sanchez
    /// <br />
    /// UPDATED: 2024-02-18
    /// <br />
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-03-19
    /// <br />
    ///     Initial creation
    ///     Added UpdateServiceOrder(ServiceOrder serviceOrder)
    ///     Added SelectServiceOrderByServiceOrderID method to complete a WO.
    /// </remarks>
    public class ServiceOrderAccessor : IServiceOrderAccessor
    {

        /// <summary>
        ///     Retrieves all ServiceOrder records from the database
        /// </summary>
        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see> List of ServiceOrder_VM objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public List<ServiceOrder_VM> GetAllServiceOrders()
        {
            List<ServiceOrder_VM> serviceOrders = new List<ServiceOrder_VM>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_active_service_orders";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ServiceOrder_VM serviceOrder = new ServiceOrder_VM()
                    {
                        VIN = reader.GetString(0),
                        Service_Order_ID = reader.GetInt32(1),
                        Critical_Issue = reader.GetBoolean(2),
                        Service_Type_ID = reader.GetString(3),
                        Service_Description = reader.GetString(4)
                    };
                    serviceOrders.Add(serviceOrder);
                }

                if (serviceOrders.Count == 0)
                {
                    throw new ArgumentException("No service order records found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return serviceOrders;
        }
        /// <summary>
        /// Updates a service order with the provided details.
        /// </summary>
        /// <param name="serviceOrder">The service order object containing the updated details.</param>
        /// <returns>
        ///     Returns an integer indicating the outcome of the update operation:
        /// </returns>
        /// <remarks>
        ///     If the provided <paramref name="serviceOrder"/> is null, an <see cref="ArgumentNullException"/> is thrown.
        ///     The method searches for the service order based on the provided Service_Order_ID.
        ///     If found, it updates the service order with the new values.
        ///     calls the sp_update_service_order stored procedure
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="serviceOrder"/> is null.</exception>
        /// <contributor>
        ///     Steven Sanchez
        /// </contributor>
        /// <created>2024-02-18</created>
        /// <updated>yyyy-MM-dd</updated>
        /// <update>
        /// <summary>
        /// Update comments go here.
        /// </summary>
        /// <remarks>
        /// Explain what you changed in this method.
        /// A new remark should be added for each update to this method.
        /// </remarks>
        /// </update>
        public int UpdateServiceOrder(ServiceOrder serviceOrder)
        {

            int rowsAffected = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_update_service_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.AddWithValue("@Service_Order_ID", serviceOrder.Service_Order_ID);
            cmd.Parameters.AddWithValue("@Critical_Issue", Convert.ToInt32(serviceOrder.Critical_Issue));
            cmd.Parameters.AddWithValue("@New_Service_Type_ID", serviceOrder.Service_Type_ID);
            cmd.Parameters.AddWithValue("@Service_Description", serviceOrder.Service_Description);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Creates a  service order with the provided details.
        /// </summary>
        /// <param name="serviceOrder">The service order object containing the details.</param>
        /// <returns>
        ///     Returns an integer indicating the outcome of the create operation:
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// <contributor>
        ///     Steven Sanchez
        /// </contributor>
        /// <created>2024-03-12</created>
        /// <updated>yyyy-MM-dd</updated>
        /// <update>
        /// <summary>
        /// Update comments go here.
        /// </summary>
        /// <remarks>
        /// Explain what you changed in this method.
        /// A new remark should be added for each update to this method.
        /// </remarks>
        public int CreateServiceOrder(ServiceOrder_VM serviceOrder)
        {
            int rows = 0;

            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_insert_service_order_and_type";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Service_Order_ID", serviceOrder.Service_Order_ID);
            cmd.Parameters.AddWithValue("@Service_Order_Version", serviceOrder.Service_Order_Version);
            cmd.Parameters.AddWithValue("@VIN", serviceOrder.VIN);
            cmd.Parameters.AddWithValue("@Service_Type_ID", serviceOrder.Service_Type_ID);
            cmd.Parameters.AddWithValue("@Created_By_Employee_ID", serviceOrder.Created_By_Employee_ID);
            cmd.Parameters.AddWithValue("@Date_Started", serviceOrder.Date_Started);
            cmd.Parameters.AddWithValue("@Date_Finished", serviceOrder.Date_Finished);
            cmd.Parameters.AddWithValue("@Service_Description", serviceOrder.Service_Description);
            try
            {

                conn.Open();


                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("Service Order could not be created");

                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating Service Order", ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        ///     Retrieves all ServiceOrder records from the database
        /// </summary>
        /// <returns>
        ///    <see cref="ServiceOrder_VM">ServiceOrder_VM</see> ServiceOrder_VM object otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-03-19
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public ServiceOrder_VM SelectServiceOrderByServiceOrderID(int serviceOrderID)
        {
            ServiceOrder_VM completeServiceOrder = new ServiceOrder_VM();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_service_order_by_service_order_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Service_Order_ID", serviceOrderID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(5))
                    {
                        ServiceOrder_VM serviceOrder = new ServiceOrder_VM()
                        {
                            Service_Order_ID = reader.GetInt32(0),
                            Service_Order_Version = reader.GetInt32(1),
                            VIN = reader.GetString(2),
                            Service_Type_ID = reader.GetString(3),
                            Created_By_Employee_ID = reader.GetInt32(4),
                            Serviced_By_Employee_ID = reader.GetInt32(5),
                            Date_Started = reader.GetDateTime(6),
                            Date_Finished = reader.GetDateTime(7),
                            Is_Active = reader.GetBoolean(8),
                            Critical_Issue = reader.GetBoolean(9)
                        };
                        completeServiceOrder = serviceOrder;
                    }
                    else
                    {
                        ServiceOrder_VM serviceOrder = new ServiceOrder_VM()
                        {
                            Service_Order_ID = reader.GetInt32(0),
                            Service_Order_Version = reader.GetInt32(1),
                            VIN = reader.GetString(2),
                            Service_Type_ID = reader.GetString(3),
                            Created_By_Employee_ID = reader.GetInt32(4),
                            Date_Started = reader.GetDateTime(6),
                            Date_Finished = reader.GetDateTime(7),
                            Is_Active = reader.GetBoolean(8),
                            Critical_Issue = reader.GetBoolean(9),
                            Service_Description = reader.GetString(10),
                        };
                        completeServiceOrder = serviceOrder;
                    }
                }

                if (completeServiceOrder == null)
                {
                    throw new ArgumentException("No service order record found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return completeServiceOrder;
        }

        /// <summary>
        /// changes the active field in the database for the given service order,
        ///  <br/>
        /// making active = false
        /// <br />
        /// <br />
        ///    Creator: Max Fare
        /// <br />
        ///    CREATED: 2024-04-05
        /// </summary>
        /// <param name="serviceOrder">The service order to deactivate</param>
        /// <returns>the number of rows affected in the database</returns>
        public int DeactivateServiceOrder(ServiceOrder_VM serviceOrder)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_deactivate_service_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Service_Order_ID", serviceOrder.Service_Order_ID);
            cmd.Parameters.AddWithValue("@Version", serviceOrder.Service_Order_Version);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
        /// <summary>
        /// changes the active field in the database for the given service order,
        /// <br/>
        /// making active = true
        /// <br />
        /// <br />
        ///    Creator: Max Fare
        /// <br />
        ///    CREATED: 2024-04-05
        /// </summary>
        /// <param name="serviceOrder">The service order to activate</param>
        /// <returns>the number of rows affected in the database</returns>
        public int ActivateServiceOrder(ServiceOrder_VM serviceOrder)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_activate_service_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Service_Order_ID", serviceOrder.Service_Order_ID);
            cmd.Parameters.AddWithValue("@Version", serviceOrder.Service_Order_Version);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
