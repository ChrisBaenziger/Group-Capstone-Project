﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IServiceOrderManager
    {
        List<ServiceOrder_VM> GetALlServiceOrders();
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
        int UpdateServiceOrder(ServiceOrder serviceOrder);

    }
}