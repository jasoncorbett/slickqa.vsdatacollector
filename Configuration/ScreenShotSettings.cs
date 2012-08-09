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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace SlickQA.DataCollector.Configuration
{
	public sealed class ScreenShotSettings : INotifyPropertyChanged
	{
		public ScreenShotSettings(XmlNode screenshotElem)
		{
			XmlAttributeCollection attrs = screenshotElem.Attributes;
			if (attrs == null)
			{
				return;
			}
			PreTest = Convert.ToBoolean(attrs["PreTest"].Value);
			PostTest = Convert.ToBoolean(attrs["PostTest"].Value);
			OnFailure = Convert.ToBoolean(attrs["OnFailure"].Value);
		}

		public ScreenShotSettings(bool preTest = false, bool postTest = false, bool onFailure = true)
		{
			PreTest = preTest;
			PostTest = postTest;
			OnFailure = onFailure;
		}

		private bool _preTest;
		public bool PreTest
		{
			get { return _preTest; }
			set
			{
				_preTest = value;
				NotifyPropertyChanged("PreTest");
			}
		}

		private bool _postTest;
		public bool PostTest
		{
			get { return _postTest; }
			set
			{
				_postTest = value;
				NotifyPropertyChanged("PostTest");
			}
		}

		private bool _onFailure;
		public bool OnFailure
		{
			get { return _onFailure; }
			set
			{
				_onFailure = value;
				NotifyPropertyChanged("OnFailure");
			}
		}

		public override string ToString()
		{
			return string.Format("PreTest? {0} PostTest? {1} OnFailure? {2}", PreTest, PostTest, OnFailure);
		}

		public XmlNode ToXml(XmlDocument doc)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, "Screenshot", String.Empty);
			XmlAttributeCollection attrCol = node.Attributes;

			XmlAttribute preTestAttr = doc.CreateAttribute("PreTest");
			preTestAttr.Value = PreTest.ToString(CultureInfo.InvariantCulture);

			XmlAttribute postTestAttr = doc.CreateAttribute("PostTest");
			postTestAttr.Value = PostTest.ToString(CultureInfo.InvariantCulture);

			XmlAttribute onFailureAttr = doc.CreateAttribute("OnFailure");
			onFailureAttr.Value = OnFailure.ToString(CultureInfo.InvariantCulture);

			Debug.Assert(attrCol != null, "attrCol != null");
			attrCol.Append(preTestAttr);
			attrCol.Append(postTestAttr);
			attrCol.Append(onFailureAttr);

			return node;
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}