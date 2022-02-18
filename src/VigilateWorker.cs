using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Vigilate
{
    public class VigilateWorker : IHostedService, IDisposable
    {
        private readonly ILogger<VigilateWorker> _logger;
        private KeepAwake keepAwake;
        private bool disposedValue;

        public VigilateWorker(ILogger<VigilateWorker> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            keepAwake = new KeepAwake(_logger);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            keepAwake.Stop(_logger);
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _logger.LogInformation("Stopping vigilate by dispose");
                    keepAwake.Stop(_logger);
                    keepAwake = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}