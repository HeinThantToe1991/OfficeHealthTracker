using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Infrastructure.Data;
using OfficeHealthTracker.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace OfficeHealthTracker.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AddressRepository> _logger;
        public AddressRepository(ApplicationDbContext context, ILogger<AddressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Address> GetAll()
        {
            _logger.LogInformation("Getting all addresses from the database.");
            return _context.Addresses.ToList();
        }

        public Address GetAddressById(Guid id)
        {
            _logger.LogInformation($"Getting address with ID {id} from the database.");
            return _context.Addresses.FirstOrDefault(s=>s.AddressId == id);
        }
        
        public void Add(Address data)
        {
            try
            {
                _context.Addresses.Add(data);
                _context.SaveChanges();
                _logger.LogInformation("Address added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding address: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
        public void Update(Address data)
        {
            try
            {
                var existingEntity = _context.Addresses.Find(data.AddressId);
                if (existingEntity != null)
                {
                    // Detach the existing entity from the context
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }

                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation($"Address with ID {data.AddressId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating address: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
        public void Delete(Guid id)
        {
            try
            {
                var data = _context.Addresses.FirstOrDefault(s => s.AddressId == id);
                if (data != null)
                {
                    _context.Addresses.Remove(data);
                    _context.SaveChanges();
                    _logger.LogInformation($"Address with ID {id} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting address: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
        
    }
}
