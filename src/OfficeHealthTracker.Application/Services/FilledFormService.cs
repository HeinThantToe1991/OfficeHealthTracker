using Microsoft.Extensions.Logging;
using System;
using OfficeHealthTracker.Application.Mapper;
using OfficeHealthTracker.Interfaces;
using OfficeHealthTracker.Interfaces.Repository;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Services
{
    public class FilledFormService : IFilledFormService
    {
        private readonly IFilledFormRepository _filledFormRepository;
        private readonly ILogger<FilledFormService> _logger;

        public FilledFormService(IFilledFormRepository filledFormRepository, ILogger<FilledFormService> logger)
        {
            _filledFormRepository = filledFormRepository;
            _logger = logger;
        }

        public FilledFormListViewModel GetAll()
        {
            _logger.LogInformation("Getting all filled forms.");
            var filledForms = _filledFormRepository.GetAll();
            return FilledFormMapper.ToViewModelList(filledForms);
        }

        public FilledFormViewModel GetById(Guid id)
        {
            _logger.LogInformation($"Getting filled form with ID {id}.");
            var filledForm = _filledFormRepository.GetById(id);
            return FilledFormMapper.ToViewModel(filledForm);
        }

        public FilledFormViewModel Add(FilledFormViewModel filledFormViewModel)
        {
            try
            {
                filledFormViewModel.FilledFormId = Guid.NewGuid();
                var filledForm = FilledFormMapper.ToDbModel(filledFormViewModel);
                _filledFormRepository.Add(filledForm);
                return filledFormViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding filled form: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

        public FilledFormViewModel Update(FilledFormViewModel filledFormViewModel)
        {
            try
            {
                var filledForm = FilledFormMapper.ToDbModel(filledFormViewModel);
                _filledFormRepository.Update(filledForm);
                _logger.LogInformation($"Filled form with ID {filledFormViewModel.FilledFormId} updated successfully.");
                return filledFormViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating filled form: {ex.Message}");
                return new FilledFormViewModel();
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _filledFormRepository.Delete(id);
                _logger.LogInformation($"Filled form with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting filled form: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
    }
}
