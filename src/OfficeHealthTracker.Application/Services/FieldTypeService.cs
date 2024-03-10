using Microsoft.Extensions.Logging;
using System;
using OfficeHealthTracker.Application.Mapper;
using OfficeHealthTracker.Interfaces;
using OfficeHealthTracker.Interfaces.Repository;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Services
{
    public class FieldTypeService : IFieldTypeService
    {
        private readonly IFieldTypeRepository _fieldTypeRepository;
        private readonly ILogger<FieldTypeService> _logger;

        public FieldTypeService(IFieldTypeRepository fieldTypeRepository, ILogger<FieldTypeService> logger)
        {
            _fieldTypeRepository = fieldTypeRepository;
            _logger = logger;
        }

        public FieldTypeListViewModel GetAll()
        {
            _logger.LogInformation("Getting all field types.");
            var fieldTypes = _fieldTypeRepository.GetAll();
            return FieldTypeMapper.ToViewModelList(fieldTypes);
        }

        public FieldTypeViewModel GetById(Guid id)
        {
            _logger.LogInformation($"Getting field type with ID {id}.");
            var fieldType = _fieldTypeRepository.GetById(id);
            return FieldTypeMapper.ToViewModel(fieldType);
        }

        public FieldTypeViewModel GetByName(string name)
        {
            _logger.LogInformation($"Getting field type with name {name}.");
            var fieldType = _fieldTypeRepository.GetByName(name);
            return FieldTypeMapper.ToViewModel(fieldType);
        }

        public FieldTypeViewModel Add(FieldTypeViewModel fieldTypeViewModel)
        {
            try
            {
                fieldTypeViewModel.FieldTypeId = Guid.NewGuid();
                var fieldType = FieldTypeMapper.ToDbModel(fieldTypeViewModel);
                _fieldTypeRepository.Add(fieldType);
                return fieldTypeViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding field type: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public FieldTypeViewModel Update(FieldTypeViewModel fieldTypeViewModel)
        {
            try
            {

                var fieldType = FieldTypeMapper.ToDbModel(fieldTypeViewModel);
                _fieldTypeRepository.Update(fieldType);
                return fieldTypeViewModel;
                _logger.LogInformation($"Field type with ID {fieldTypeViewModel.FieldTypeId} updated successfully.");
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
                _fieldTypeRepository.Delete(id);
                _logger.LogInformation($"Field type with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting field type: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
    }
}
