using System;

namespace MediatrDemo.Domain
{
    public interface IAuditable
    {
        DateTimeOffset CreatedDate { get; }
        DateTimeOffset UpdatedDate { get; }
        Guid CreatedBy { get; }
        Guid UpdatedBy { get; }
    }
}
