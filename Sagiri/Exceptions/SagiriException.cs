using System;
using System.Runtime.Serialization;

namespace Sagiri.Exceptions
{
	[Serializable()]
    internal class SagiriException : Exception
    {
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public SagiriException() : base("") { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg"></param>
		public SagiriException(string msg) : base(msg) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="inner"></param>
		public SagiriException(string msg, Exception inner) : base(msg, inner) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SagiriException(SerializationInfo info, StreamingContext context) { }

		#endregion Constructors
	}
}
