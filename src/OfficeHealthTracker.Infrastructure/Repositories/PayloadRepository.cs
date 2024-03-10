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
    public class PayloadRepository : IPayloadRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PayloadRepository> _logger;
        public PayloadRepository(ApplicationDbContext context, ILogger<PayloadRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(PayloadData data)
        {
            try
            {
                _context.PayloadDatas.Add(data);
                _context.SaveChanges();
                _logger.LogInformation("Payload added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding Payload: {ex.Message}");
                throw; 
            }
        }

    }
}
