using System;

namespace Api.Results
{
	public class ObjectIdResult
	{
		public ObjectIdResult(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; private set; }
	}
}
