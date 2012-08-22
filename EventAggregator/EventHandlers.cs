// Copyright (c) 2009 Derick Bailey. All Rights Reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace SlickQA.DataCollector.EventAggregator
{
	internal sealed class EventHandlers
	{
		private Action<Exception> ErrorHandler { get; set; }
		private IDictionary<Type, IList<EventHandlerOptions>> Handlers { get; set; }

		internal EventHandlers()
		{
			Handlers = new Dictionary<Type, IList<EventHandlerOptions>>();
		}

		internal void Add<T>(EventHandlerOptions handler)
		{
			var handleType = typeof(T);
			Add(handleType, handler);
		}

		internal void Add(Type eventType, EventHandlerOptions handler)
		{
			if (!Handlers.ContainsKey(eventType))
			{
				Handlers[eventType] = new List<EventHandlerOptions>();
			}
			Handlers[eventType].Add(handler);
		}

		internal void Remove<T>(IEventHandler<T> handler)
		{
			var handleType = typeof(T);
			Remove(handleType, handler);
		}

		internal void Remove(Type eventType, object handler)
		{
			if (!Handlers.ContainsKey(eventType))
			{
				return;
			}

			IList<EventHandlerOptions> handlerOptions = Handlers[eventType];
			IList<EventHandlerOptions> filteredOptions = handlerOptions.Where(h => h.EventHandler == handler).ToList();
			foreach (var option in filteredOptions)
			{
				handlerOptions.Remove(option);
			}
		}

		internal void Handle<T>(T eventData)
		{
			IEnumerable<EventHandlerOptions> handlers = GetEventHandlers<T>();
			foreach (var handlerOptions in handlers)
			{
				try
				{
					var eventHandler = handlerOptions.EventHandler as IEventHandler<T>;
					if (eventHandler != null)
					{
						eventHandler.Handle(eventData);
					}
				}
				catch (Exception e)
				{
					HandleError(handlerOptions, e);
				}
			}
		}

		internal void OnHandlerError(Action<Exception> errorHandler)
		{
			ErrorHandler = errorHandler;
		}

		private void HandleError(EventHandlerOptions eventHandlerOptions, Exception e)
		{
			if (ErrorHandler != null)
			{
				ErrorHandler(e);
			}

			if (eventHandlerOptions.ErrorHandler != null)
			{
				eventHandlerOptions.ErrorHandler(e);
			}
		}

		private IEnumerable<EventHandlerOptions> GetEventHandlers<T>()
		{
			var handleType = typeof(T);
			IList<EventHandlerOptions> handlers = new List<EventHandlerOptions>();

			if (Handlers.ContainsKey(handleType))
			{
				foreach (var handler in Handlers[handleType].Where(handler => handler != null))
				{
					handlers.Add(handler);
				}
			}

			return handlers;
		}
	}
}