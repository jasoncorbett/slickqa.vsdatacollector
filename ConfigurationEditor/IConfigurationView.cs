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
using System.Collections.Generic;
using SlickQA.DataCollector.Configuration;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public interface IConfigurationView
	{
		void PopulateProjects(IEnumerable<Project> projects);
		void GetProjects(object sender, EventArgs e);
		void SetUrl(SlickUrlType slickUrl);
		void SelectProjectAndRelease(ResultDestination destination);
	}
}