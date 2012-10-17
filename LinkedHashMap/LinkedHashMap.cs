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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SlickQA
{
	[DataContract]
	public sealed class LinkedHashMap<T> : IDictionary<string, T> where T : class
	{
		private readonly Dictionary<string, T> _dict;

		public LinkedHashMap()
		{
			_dict = new Dictionary<string, T>();
		}

		private LinkedHashMap(SerializationInfo info, StreamingContext context)
		{
			_dict = new Dictionary<string, T>();
			foreach (SerializationEntry entry in info)
			{
				var item = entry.Value as T;
				_dict.Add(entry.Name, item);
			}
		}

		#region IDictionary<string,T> Members

		public void Add(KeyValuePair<string, T> item)
		{
			_dict.Add(item.Key, item.Value);
		}

		public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
		{
			return _dict.GetEnumerator();
		}

		public void Clear()
		{
			_dict.Clear();
		}

		public bool Contains(KeyValuePair<string, T> item)
		{
			ICollection<KeyValuePair<string, T>> col = _dict;
			return col.Contains(item);
		}

		public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
		{
			ICollection<KeyValuePair<string, T>> col = _dict;
			col.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<string, T> item)
		{
			ICollection<KeyValuePair<string, T>> col = _dict;
			return col.Remove(item);
		}

		public int Count
		{
			get { return _dict.Count; }
		}

		public bool IsReadOnly
		{
			get
			{
				ICollection<KeyValuePair<string, T>> col = _dict;
				return col.IsReadOnly;
			}
		}

		public bool ContainsKey(string key)
		{
			return _dict.ContainsKey(key);
		}

		public void Add(string key, T value)
		{
			_dict.Add(key, value);
		}

		public bool Remove(string key)
		{
			return _dict.Remove(key);
		}

		public bool TryGetValue(string key, out T value)
		{
			return _dict.TryGetValue(key, out value);
		}

		public T this[string key]
		{
			get { return _dict[key]; }
			set { _dict[key] = value; }
		}

		public ICollection<string> Keys
		{
			get { return _dict.Keys; }
		}

		public ICollection<T> Values
		{
			get { return _dict.Values; }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		public void Add(DictionaryEntry entry)
		{
			_dict.Add(entry.Key.ToString(), entry.Value as T);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			foreach (KeyValuePair<string, T> keyValuePair in _dict)
			{
				info.AddValue(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}