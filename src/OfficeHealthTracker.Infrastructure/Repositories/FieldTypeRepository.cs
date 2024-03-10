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
    public class FieldTypeRepository : IFieldTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FieldTypeRepository> _logger;
        public FieldTypeRepository(ApplicationDbContext context, ILogger<FieldTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<FieldType> GetAll()
        {
            _logger.LogInformation("Getting all field types from the database.");
            return _context.FieldTypes.ToList();
        }

        public FieldType GetById(Guid id)
        {
            _logger.LogInformation($"Getting field type with ID {id} from the database.");
            return _context.FieldTypes.FirstOrDefault(s => s.FieldTypeId == id);
        }
        public FieldType GetByName(string name)
        {
            _logger.LogInformation($"Getting field type with name {name} from the database.");
            return _context.FieldTypes.FirstOrDefault(s => s.TypeName == name);
        }
        public void Add(FieldType data)
        {
            try
            {
                _context.FieldTypes.Add(data);
                _context.SaveChanges();
                _logger.LogInformation("Field type added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding field type: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public void Update(FieldType data)
        {
            try
            {
                var existingEntity = _context.FieldTypes.Find(data.FieldTypeId);
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation($"Field type with ID {data.FieldTypeId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating field type: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var data = _context.FieldTypes.FirstOrDefault(s => s.FieldTypeId == id);
                if (data != null)
                {
                    _context.FieldTypes.Remove(data);
                    _context.SaveChanges();
                    _logger.LogInformation($"Field type with ID {id} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting field type: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
    }
}
