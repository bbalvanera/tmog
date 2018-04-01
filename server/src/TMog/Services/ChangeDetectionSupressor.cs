using System;
using TMog.Data;

namespace TMog.Services
{
    /// <summary>
    /// Use to supress change detection from the given ITMogDatabase
    /// </summary>
    internal class ChangeDetectionSupressor :IDisposable
    {
        private bool disposed;
        private readonly ITMogDatabase context;

        public ChangeDetectionSupressor(ITMogDatabase context)
        {
            this.context = context;

            context.DisableChangeDetection();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // release managed resources here.
                try
                {
                    context.EnableChangeDetection();
                }
                catch
                {
                    // fail silently as dispose method should not throw errors.
                }
            }

            // release unmanaged resources here.
            disposed = true;
        }
    }
    
}
