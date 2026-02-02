using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// Helper class to encapsulate processing time recording.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    [Serializable]
    public class ProcessingTimer
    {
        #region Ctor

        /// <summary>
        /// Ctor.
        /// </summary>
        internal ProcessingTimer()
        {
            ElapsedTimeAsMs = 0;
            IsRunning = false;
            TimerStartPoint = DateTime.Now;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="startTimer">Flag to indicate whether the timer should be started.</param>
        internal ProcessingTimer(bool startTimer)
            : this()
        {
            if (startTimer)
                Start();
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets a flag indicating whether the timer is currently running.
        /// </summary>
        public bool IsRunning
        { get; private set; }

        /// <summary>
        /// Gets\Sets the elapsed time in milliseconds.
        /// </summary>
        public double ElapsedTimeAsMs
        {
            get
            {
                UpdateElapsedTime();
                return this.elapsedTimeAsMsField;
            }
            set { this.elapsedTimeAsMsField = value; }
        }
        private double elapsedTimeAsMsField;

        /// <summary>
        /// Gets or sets the moment (in ticks) at which the processing started.
        /// </summary>
        public DateTime TimerStartPoint
        { get; private set; }

        /// <summary>
        /// Gets or sets the current time (in ticks).
        /// </summary>
        public DateTime CurrentTime
        { 
            get
            {
                DateTime result;
                result = TimerStartPoint;
                result = result.AddMilliseconds(ElapsedTimeAsMs);
                return result;
            } 
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            this.elapsedTimeAsMsField = 0;
            TimerStartPoint = DateTime.Now;
            IsRunning = true;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                UpdateElapsedTime();
                IsRunning = false;
            }
        }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        public void Resume()
        {
            if (!IsRunning)
            {
                TimerStartPoint = DateTime.Now;
                IsRunning = true;
            }
        }

        /// <summary>
        /// Updates the timer value.
        /// </summary>
        private void UpdateElapsedTime()
        {
            if (IsRunning)
            {
                DateTime now = DateTime.Now;
                TimeSpan elapsedTicks =
                    new TimeSpan(now.Ticks - TimerStartPoint.Ticks);
                this.elapsedTimeAsMsField += elapsedTicks.TotalMilliseconds;
                TimerStartPoint = now;
            }
        }

        #endregion Methods
    }
}