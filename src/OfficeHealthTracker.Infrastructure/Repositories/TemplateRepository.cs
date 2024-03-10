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
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TemplateRepository> _logger;

        public TemplateRepository(ApplicationDbContext context, ILogger<TemplateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Template> GetAll()
        {
            _logger.LogInformation("Getting all templates from the database.");
            return _context.Templates.Include(template => template.TemplateFields).ThenInclude(t=>t.FieldType).ToList();
        }

        public Template GetById(Guid id)
        {
            _logger.LogInformation($"Getting template with ID {id} from the database.");
            return _context.Templates.Include(template => template.TemplateFields)
                .ThenInclude(templateField => templateField.FieldType)
                .FirstOrDefault(template => template.TemplateId == id);
        }

        public Template GetByName(string name)
        {
            _logger.LogInformation($"Getting template with name '{name}' from the database.");
            return _context.Templates.FirstOrDefault(s => s.TemplateName == name);
        }

        public void Add(Template data,List<TemplateField> templateField)
        {
            try
            {
                _context.Templates.Add(data);
                _context.SaveChanges(); // Save changes to the database to get the TemplateId
                
                _context.TemplateFields.AddRange(templateField);
                _context.SaveChanges();

                _logger.LogInformation("Template added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding template: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public void Update(Template data,List<TemplateField> templateField)
        {
            try
            {
                var existingEntity = _context.Templates.Find(data.TemplateId);
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation($"Template with ID {data.TemplateId} updated successfully.");

                DeleteTemplateFieldByTemplateId(data.TemplateId);
                _context.TemplateFields.AddRange(templateField);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating template: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var data = _context.Templates.FirstOrDefault(s => s.TemplateId == id);
                if (data != null)
                {
                    DeleteTemplateFieldByTemplateId(id);
                    _context.Templates.Remove(data);
                    _context.SaveChanges();
                    _logger.LogInformation($"Template with ID {id} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting template: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
        public void DeleteTemplateFieldByTemplateId(Guid id)
        {
            try
            {
                var data = _context.TemplateFields.Where(s => s.TemplateId == id).ToList();
                if (data.Any())
                {
                    _context.TemplateFields.RemoveRange(data);
                    _context.SaveChanges();
                    _logger.LogInformation($"Template with ID {id} delete successful.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting template: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
    }
}
