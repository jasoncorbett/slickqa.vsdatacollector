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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.Web;

namespace SlickQA.DataCollector.ConfigurationEditor.Test
{
	[TestClass]
	public sealed class SetServerConfig
	{
		[TestMethod]
		public void With_empty_site_path()
		{
			ConfigurationController.SetServerConfig("http", "example.com", 8080, String.Empty);
			Assert.AreEqual(new Uri("http://example.com:8080/api"), ServerConfig.BaseUri);
		}

		[TestMethod]
		public void With_port_80_leaves_port_out_of_url()
		{
			ConfigurationController.SetServerConfig("http", "example.com", 80, String.Empty);
			Assert.AreEqual(new Uri("http://example.com/api"), ServerConfig.BaseUri);
		}

		[TestMethod]
		public void With_site_path()
		{
			ConfigurationController.SetServerConfig("http", "example.com", 8080, "baz");
			Assert.AreEqual(new Uri("http://example.com:8080/baz/api"), ServerConfig.BaseUri);
		}
	}
}