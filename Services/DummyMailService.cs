using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
    public class DummyMailService : IMailService
    {
        private readonly ILogger<DummyMailService> _logger;

        public DummyMailService(ILogger<DummyMailService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(string from, string subject, string message)
        {
            _logger.LogInformation($" To { from } Subject { subject} Message { message}");
        }
    }
}
