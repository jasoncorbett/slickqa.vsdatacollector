// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SlickQA.DataCollector.EventAggregator;
using StructureMap;
using StructureMap.Interceptors;
using StructureMap.TypeRules;

namespace SlickQA.DataCollector.ConfigurationEditor.IoC_Setup
{
	public sealed class EventInterceptor : TypeInterceptor
	{
		#region TypeInterceptor Members

		public object Process(object target, IContext context)
		{
			var publisher = context.GetInstance<IEventPublisher>();
			publisher.RegisterHandlers(target);
			return target;
		}

		public bool MatchesType(Type type)
		{
			return type.ImplementsInterfaceTemplate(typeof(IEventHandler<>));
		}

		#endregion
	}
}
