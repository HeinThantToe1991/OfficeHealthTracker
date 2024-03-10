using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Infrastructure.Data;
using OfficeHealthTracker.Interfaces.Repository;

namespace OfficeHealthTracker.Infrastructure.Repositories
{
    public class FilledFormRepository : IFilledFormRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FilledFormRepository> _logger;

        public FilledFormRepository(ApplicationDbContext context, ILogger<FilledFormRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<FilledForm> GetAll()
        {
            _logger.LogInformation("Getting all filled forms from the database.");
            return _context.FilledForms.ToList();
        }

        public FilledForm GetById(Guid id)
        {
            _logger.LogInformation($"Getting filled form with ID {id} from the database.");
            return _context.FilledForms.FirstOrDefault(s => s.FilledFormId == id);
        }

        public void Add(FilledForm data)
        {
            try
            {
                _context.FilledForms.Add(data);
                _context.SaveChanges();
                _logger.LogInformation("Filled form added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding filled form: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public void Update(FilledForm data)
        {
            try
            {
                var existingEntity = _context.FilledForms.Find(data.FilledFormId);
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation($"Filled form with ID {data.FilledFormId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating filled form: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var data = _context.FilledForms.FirstOrDefault(s => s.FilledFormId == id);
                if (data != null)
                {
                    _context.FilledForms.Remove(data);
                    _context.SaveChanges();
                    _logger.LogInformation($"Filled form with ID {id} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting filled form: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
    }
}
