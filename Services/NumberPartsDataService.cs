﻿using ProjectManager.Contracts.Services;
using ProjectManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public class NumberPartsDataService : INumberPartsDataService
    {
        private readonly EcnContext context = null;

        public NumberPartsDataService()
        {
            context = new EcnContext();
        }

        public async Task<IEnumerable<Numberpart>> GetNumberPartsAsync()
        {
            await Task.CompletedTask;
            return GetNumberParts();
        }

        public IEnumerable<Numberpart> GetNumberParts()
        {
            using EcnContext context = new EcnContext();
            return context.Numberparts.ToList();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            await Task.CompletedTask;
            return GetCustomer(id);
        }

        public Customer GetCustomer(int id)
        {
            return context.Customers.Find(id);
        }

        public async Task<NumberpartType> GetNumberpartTypeAsync(int id)
        {
            await Task.CompletedTask;
            return GetNumberPartType(id);
        }

        public NumberpartType GetNumberPartType(int id)
        {
            return context.NumberpartTypes.Find(id);
        }

        public async Task<Numberpart> GetNumberPartAsync(int id)
        {
            await Task.CompletedTask;
            return GetNumberPart(id);
        }

        public Numberpart GetNumberPart(int id)
        {
            return context.Numberparts.Find(id);
        }

        public ICollection<EcnNumberpart> GetNumberPartsEcn(int ecn)
        {
            return context.EcnNumberparts.Where(i => i.EcnId == ecn).ToList();
        }

        public async Task<ICollection<EcnNumberpart>> GetNumberPartsEcnsAsync(int ecn)
        {
            await Task.CompletedTask;
            return GetNumberPartsEcn(ecn);
        }

        public async Task<IEnumerable<Numberpart>> GetNumberPartsPerCustomerAsync(int customerid, string revision)
        {
            await Task.CompletedTask;
            return GetNumberPartsPerCustomer(customerid, revision);
        }

        public IEnumerable<Numberpart> GetNumberPartsPerCustomer(int customerid, string revision)
        {
            return context.Numberparts.Where(data => data.CustomerId == customerid && data.NumberPartRev == revision);
        }

        public async Task<IEnumerable<EcnNumberpart>> GetNumberPartHistoryAsync()
        {
            await Task.CompletedTask;
            return GetNumberPartHistory();
        }

        private IEnumerable<EcnNumberpart> GetNumberPartHistory()
        {
            return context.EcnNumberparts.Where(data => data.Ecn.OldDrawingLvl != data.Ecn.DrawingLvl && data.Ecn.StatusId == 3 && data.Ecn.ChangeTypeId < 3).ToList();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            await Task.CompletedTask;
            return GetCustomers();
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public bool AddNumberPart(Numberpart numberpart)
        {
            if (numberpart != null)
            {
                context.Numberparts.Add(numberpart);

                var result = context.SaveChanges();
                return result > 0;
            }
            return false;
        }

        public bool UpdateNumberPart(Numberpart numberpart)
        {
            _ = context.Update(numberpart);

            var result = context.SaveChanges();
            return result > 0;
        }

        public bool AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                context.Customers.Add(customer);

                var result = context.SaveChanges();
                return result > 0;
            }
            return false;
        }

        public async Task<IEnumerable<EcnNumberpart>> GetHistoryAsync()
        {
            await Task.CompletedTask;
            return GetHistory();
        }

        private IEnumerable<EcnNumberpart> GetHistory()
        {
            return context.EcnNumberparts.Where(data => data.Ecn.StatusId != 2).ToList();
        }
    }
}
