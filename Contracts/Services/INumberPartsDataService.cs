﻿using ProjectManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Contracts.Services
{
    public interface INumberPartsDataService
    {
        Task<IEnumerable<Numberpart>> GetNumberPartsAsync();
        Task<IEnumerable<Numberpart>> GetNumberPartsPerCustomerAsync(int customerid, string revision);
        Task<Numberpart> GetNumberPartAsync(int id);
        Task<Customer> GetCustomerAsync(int id);
        Task<NumberpartType> GetNumberpartTypeAsync(int id);
        Task<ICollection<EcnNumberpart>> GetNumberPartsEcnsAsync(int ecn);
        Task<IEnumerable<EcnNumberpart>> GetNumberPartHistoryAsync();
        Task<IEnumerable<EcnNumberpart>> GetHistoryAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        bool AddNumberPart(Numberpart numberpart);
        bool UpdateNumberPart(Numberpart numberpart);
        bool AddCustomer(Customer customer);
    }
}
