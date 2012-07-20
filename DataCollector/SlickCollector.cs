using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.Execution;

namespace SlickQA.DataCollector
{
	[DataCollectorTypeUri("datacollector://slickqa/SlickDataCollector/0.0.1")]
	[DataCollectorFriendlyName("Slick", false)]
	public class SlickCollector : Microsoft.VisualStudio.TestTools.Execution.DataCollector
	{
		private XmlElement _configurationSettings;
		private DataCollectionEnvironmentContext _dataCollectionEnvironmentContext;

		private DataCollectionEvents _dataEvents;

		private DataCollectionLogger _dataLogger;
		private DataCollectionSink _dataSink;

		public override void Initialize(XmlElement configurationElement, DataCollectionEvents events,
		                                DataCollectionSink dataSink, DataCollectionLogger logger,
		                                DataCollectionEnvironmentContext environmentContext)
		{
			_configurationSettings = configurationElement;
			_dataEvents = events;
			_dataSink = dataSink;
			_dataLogger = logger;
			_dataCollectionEnvironmentContext = environmentContext;

			_dataEvents.CustomNotification += OnCustomNotification;
			_dataEvents.DataRequest += OnDataRequest;

			_dataEvents.SessionStart += OnSessionStart;
			_dataEvents.SessionPause += OnSessionPause;
			_dataEvents.SessionResume += OnSessionResume;
			_dataEvents.SessionEnd += OnSessionEnd;

			_dataEvents.TestCaseStart += OnTestCaseStart;
			_dataEvents.TestCasePause += OnTestCasePause;
			_dataEvents.TestCaseResume += OnTestCaseResume;
			_dataEvents.TestCaseEnd += OnTestCaseEnd;
			_dataEvents.TestCaseFailed += OnTestCaseFailed;
			_dataEvents.TestCaseReset += OnTestCaseReset;

			_dataEvents.TestStepStart += OnTestStepStart;
			_dataEvents.TestStepEnd += OnTestStepEnd;
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			_dataEvents.CustomNotification -= OnCustomNotification;
			_dataEvents.DataRequest -= OnDataRequest;

			_dataEvents.SessionStart -= OnSessionStart;
			_dataEvents.SessionPause -= OnSessionPause;
			_dataEvents.SessionResume -= OnSessionResume;
			_dataEvents.SessionEnd -= OnSessionEnd;

			_dataEvents.TestCaseStart -= OnTestCaseStart;
			_dataEvents.TestCasePause -= OnTestCasePause;
			_dataEvents.TestCaseResume -= OnTestCaseResume;
			_dataEvents.TestCaseEnd -= OnTestCaseEnd;
			_dataEvents.TestCaseFailed -= OnTestCaseFailed;
			_dataEvents.TestCaseReset -= OnTestCaseReset;

			_dataEvents.TestStepStart -= OnTestStepStart;
			_dataEvents.TestStepEnd -= OnTestStepEnd;
		}

		private void OnCustomNotification(object sender, CustomNotificationEventArgs customNotificationEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnDataRequest(object sender, DataRequestEventArgs dataRequestEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnSessionStart(object sender, SessionStartEventArgs sessionStartEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnSessionPause(object sender, SessionPauseEventArgs sessionPauseEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnSessionResume(object sender, SessionResumeEventArgs sessionResumeEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnSessionEnd(object sender, SessionEndEventArgs sessionEndEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestCaseStart(object sender, TestCaseStartEventArgs testCaseStartEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestCasePause(object sender, TestCasePauseEventArgs testCasePauseEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestCaseResume(object sender, TestCaseResumeEventArgs testCaseResumeEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestCaseEnd(object sender, TestCaseEndEventArgs testCaseEndEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestCaseFailed(object sender, TestCaseFailedEventArgs testCaseFailedEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestCaseReset(object sender, TestCaseResetEventArgs testCaseResetEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestStepStart(object sender, TestStepStartEventArgs testStepStartEventArgs)
		{
			throw new NotImplementedException();
		}

		private void OnTestStepEnd(object sender, TestStepEndEventArgs testStepEndEventArgs)
		{
			throw new NotImplementedException();
		}
	}
}