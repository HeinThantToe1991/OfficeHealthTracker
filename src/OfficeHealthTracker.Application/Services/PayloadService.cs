using Microsoft.Extensions.Logging;
using System;
using OfficeHealthTracker.Application.Mapper;
using OfficeHealthTracker.Interfaces;
using OfficeHealthTracker.Interfaces.Repository;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Services
{
    public class PayloadService : IPayloadService
    {
        private readonly IPayloadRepository _repository;
        private readonly ILogger<FieldTypeService> _logger;

        public PayloadService(IPayloadRepository repository, ILogger<FieldTypeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        

        public PayloadViewModel Add(PayloadViewModel viewModel)
        {
            try
            {
                var data = PayloadMapper.ToDbModel(viewModel);
                _repository.Add(data);
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding payload: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }

    }
}
