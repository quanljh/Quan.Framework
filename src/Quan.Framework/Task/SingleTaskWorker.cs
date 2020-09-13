﻿using System;
using System.Threading;
using System.Threading.Tasks;
using static Quan.FrameworkDI;

namespace Quan
{
    /// <summary>
    /// <para>
    ///     Provides a thread-safe mechanism for starting and stopping the execution of an task
    ///     that can only have one instance of itself running regardless of the amount of times
    ///     start/stop is called and from what thread.
    /// </para>
    /// <para>
    ///     Supports cancellation requests via the <see cref="StopAsync"/> and the given task
    ///     will be provided with a cancellation token to monitor for when it should "stop"
    /// </para>
    /// </summary>
    public abstract class SingleTaskWorker
    {
        #region Protected Members

        /// <summary>
        /// A flag indicating if the worker task is still running
        /// </summary>
        protected ManualResetEvent mWorkerFinishedEvent = new ManualResetEvent(true);

        /// <summary>
        /// The token used to cancel any ongoing work in order to shutdown
        /// </summary>
        protected CancellationTokenSource mCancellationToken = new CancellationTokenSource();

        #endregion

        #region Public Properties

        /// <summary>
        /// A unique ID for locking the starting and stopping calls of this class
        /// </summary>
        public string LockingKey { get; set; } = nameof(SingleTaskWorker) + Guid.NewGuid().ToString("N");

        /// <summary>
        /// The name that identifies this worker (used in unhandled exception logs to report source of an issue)
        /// </summary>
        public abstract string WorkerName { get; }

        /// <summary>
        /// Indicates if the service is shutting down and should finish what it's doing and save any important information/progress
        /// </summary>
        public bool Stopping => mCancellationToken.IsCancellationRequested;

        /// <summary>
        /// Indicates if the main worker task is running
        /// </summary>
        public bool IsRunning
        {
            // We are running if we haven't finished
            get => !mWorkerFinishedEvent.WaitOne(0);
            set
            {
                // If we are running...
                if (value)
                    // Then we have not finished
                    mWorkerFinishedEvent.Reset();
                // If we want to stop running...
                else
                    // We should call ShutdownAsync instead
                    throw new InvalidOperationException($"Use {nameof(StopAsync)} instead");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        protected SingleTaskWorker()
        {

        }

        #endregion

        #region Startup / Shutdown

        /// <summary>
        /// Starts the given task running if it is not already running
        /// </summary>
        /// <returns></returns>
        public Task<bool> StartAsync()
        {
            // Log it
            Logger.LogTraceSource($"Start requested...");

            // Make sure only one start or stop call runs at a time...
            return AsyncLock.LockResultAsync(LockingKey, () =>
            {
                // If we are already running...
                if (IsRunning)
                {
                    // Log it
                    Logger.LogTraceSource($"Already started. Ignoring.");

                    // We are not starting a new task
                    return false;
                }

                // New cancellation token
                mCancellationToken = new CancellationTokenSource();

                // Flag that we are running
                IsRunning = true;

                // Log it
                Logger.LogTraceSource($"Starting worker process...");

                // Start the main task
                RunWorkerTaskNoAwait();

                // We have started a new task
                return true;
            });
        }

        /// <summary>
        /// Requests that the given task should stop running, and awaits for it to finish
        /// </summary>
        /// <returns>Returns once the worker task has returned</returns>
        public Task StopAsync()
        {
            // Make sure only one startup or shutdown call runs at a time...
            return AsyncLock.LockAsync(LockingKey, async () =>
            {
                // If we are not running...
                if (!IsRunning)
                {
                    // Log it
                    Logger.LogTraceSource($"Already stopped. Ignoring.");

                    // Ignore
                    return;
                }

                // Log it
                Logger.LogTraceSource($"Stop requested...");

                // Flag main worker to shut down
                mCancellationToken.Cancel();

                // Wait for it to stop running
                await mWorkerFinishedEvent.WaitOneAsync();
            });
        }

        #endregion

        #region Worker Task

        /// <summary>
        /// Runs the worker task and sets the IsRunning to false once complete
        /// </summary>
        /// <returns>Returns once the worker task has completed</returns>
        protected void RunWorkerTaskNoAwait()
        {
            // IMPORTANT: Not awaiting a Task leads to swallowed exceptions
            //            so we try/catch the entire task and report any unhandled
            //            exceptions to the log
            Task.Run(async () =>
            {
                try
                {
                    // Log something
                    Logger.LogTraceSource($"Worker task started...");

                    // Run given task
                    await WorkerTaskAsync(mCancellationToken.Token);
                }
                // Swallow expected and normal task canceled exceptions
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    // Unhandled exception
                    // Log it
                    Logger.LogCriticalSource($"Unhandled exception in single task worker '{WorkerName}'. {ex}");
                }
                finally
                {
                    // Log it
                    Logger.LogTraceSource($"Worker task finished");

                    // Set finished event informing waiters we are finished working
                    mWorkerFinishedEvent.Set();
                }
            });
        }

        /// <summary>
        /// The task that will be run by this worker
        /// </summary>
        protected virtual Task WorkerTaskAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        #endregion
    }
}
