/* Copyright 2012 AccessData Group, LLC.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Runtime.Serialization;

namespace SlickQA.SlickSharp.ObjectReferences
{
	[DataContract]
	public sealed class ProjectReference : JsonObject<ProjectReference>, IJsonObject
	{
		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		private ProjectReference(Project project)
		{
			Id = project.Id;
			Name = project.Name;
		}

		public static implicit operator ProjectReference(Project project)
		{
			return new ProjectReference(project);
		}

		public static implicit operator Project(ProjectReference projectReference)
		{
			var p = new Project
			        {
			        	Id = projectReference.Id,
						Name = projectReference.Name
			        };
			p.Get();
			return p;
		}
	}
}