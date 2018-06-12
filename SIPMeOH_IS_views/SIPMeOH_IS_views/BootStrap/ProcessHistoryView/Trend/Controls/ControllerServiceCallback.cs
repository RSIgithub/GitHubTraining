//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file=TimeServiceCallback.cs" company="RSI">
//    Copyright (c) RSI - All Rights Reserved
//  </copyright>
//  <summary>
//    
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls
{
    using System;
    using RSI.Services.Controller.Interfaces;

    public class ControllerServiceCallback : ControllerServiceCallbackBase
    {
        #region Fields

        private readonly ProcessHistoryChartControl _trendControlVP;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerServiceCallback"/> class.
        /// </summary>
        /// <param name="trendControlVP">The trend control vp.</param>
        public ControllerServiceCallback(ProcessHistoryChartControl trendControlVP)
        {
            this._trendControlVP = trendControlVP;
        }

        #endregion

        /// <summary>
        /// Processes the simulation step completed.
        /// </summary>
        /// <param name="simulatedStep">The simulated step.</param>
        /// <param name="simulatedTime">The simulated time.</param>
        public override void ProcessSimulationStepCompleted(int simulatedStep, TimeSpan simulatedTime)
        {
            this._trendControlVP.ProcessSimulationProgress(simulatedTime);
        }
    }
}