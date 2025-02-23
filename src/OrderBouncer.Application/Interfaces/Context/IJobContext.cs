using System;
using OrderBouncer.Domain.Models;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Interfaces.Context;

public interface IJobContext
{
    public void TryStoreInt(Guid jobId, int value);
    public void TryStoreString(Guid jobId, string value);
    public void TryStoreObject<T>(Guid jobId, T value);
    public void TryStoreGuid(Guid jobId, Guid value);
    public (int, bool) TryGetInt(Guid jobId, Func<int, bool> predicate);
    public (string, bool) TryGetString(Guid jobId, Func<string, bool> predicate);
    public (T, bool) TryGetObject<T>(Guid jobId, Func<JobContextObject, bool> predicate);
    public (Guid, bool) TryGetGuid(Guid jobId, Func<Guid, bool> predicate);
}
