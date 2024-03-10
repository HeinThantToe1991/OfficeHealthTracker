using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using OfficeHealthTracker.Application.Mapper;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces;
using OfficeHealthTracker.Interfaces.Repository;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ILogger<TemplateService> _logger;

        public TemplateService(ITemplateRepository templateRepository, ILogger<TemplateService> logger)
        {
            _templateRepository = templateRepository;
            _logger = logger;
        }

        public TemplateListViewModel GetAll()
        {
            _logger.LogInformation("Getting all templates.");
            var templates = _templateRepository.GetAll();
            return TemplateMapper.ToViewModelList(templates);
        }

        public TemplateResponse GetById(Guid id)
        {
            _logger.LogInformation($"Getting template with ID {id}.");
            var template = _templateRepository.GetById(id);
            var response = new TemplateResponse();
            response.Template = TemplateMapper.ToViewModel(template);
            response.FieldList = TemplateFieldMapper.ToViewModelList(template.TemplateFields);
            return response;
        }

        public TemplateViewModel GetByName(string name)
        {
            _logger.LogInformation($"Getting template with name {name}.");
            var template = _templateRepository.GetByName(name);
            return TemplateMapper.ToViewModel(template);
        }

        public TemplateViewModel Add(TemplateViewModel templateViewModel,TemplateFieldListViewModel templateFieldViewModel)
        {
            try
            {
                templateViewModel.TemplateId = Guid.NewGuid();
                templateViewModel.CreatedDate = DateTime.Now;
                var template = TemplateMapper.ToDbModel(templateViewModel);
                var templateFields = new List<TemplateField>();
                foreach (var item in templateFieldViewModel.TemplateField)
                {
                    item.TemplateFieldId = Guid.NewGuid();
                    item.CreatedDate = DateTime.Now;
                    item.TemplateId = templateViewModel.TemplateId;
                    templateFields.Add(TemplateFieldMapper.ToDbModel(item));
                }
                _templateRepository.Add(template, templateFields);
                return templateViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding template: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public TemplateViewModel Update(TemplateViewModel templateViewModel, TemplateFieldListViewModel templateFieldViewModel)
        {
            try
            {
                var template = TemplateMapper.ToDbModel(templateViewModel);
                template.UpdatedDate = DateTime.Now;
                var templateFields = new List<TemplateField>();
                foreach (var item in templateFieldViewModel.TemplateField)
                {
                    item.TemplateFieldId = Guid.NewGuid();
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedDate = DateTime.Now;
                    templateFields.Add(TemplateFieldMapper.ToDbModel(item));
                }
                _templateRepository.Update(template, templateFields);
                _logger.LogInformation($"Template with ID {templateViewModel.TemplateId} updated successfully.");
                return templateViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating template: {ex.Message}");
                return new TemplateViewModel();
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _templateRepository.Delete(id);
                _logger.LogInformation($"Template with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting template: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
    }
}
