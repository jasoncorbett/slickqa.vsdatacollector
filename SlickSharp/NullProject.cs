using System;

namespace SlickQA.SlickSharp
{
	public class NullProject : Project, IEquatable<NullProject>
	{
		public override string ToString()
		{
			return "";
		}

		public override bool Equals(object obj)
		{
			var other = obj as NullProject;
			return other != null;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(NullProject other)
		{
			return true;
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator ==(NullProject left, NullProject right)
		{
			return true;
		}

		public static bool operator !=(NullProject left, NullProject right)
		{
			return false;
		}
	}
}