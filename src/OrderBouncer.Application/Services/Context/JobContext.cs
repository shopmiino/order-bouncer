using System;
using System.Collections.Concurrent;
using OrderBouncer.Application.Interfaces.Context;
using OrderBouncer.Domain.Models;

namespace OrderBouncer.Application.Services.Context;

public class JobContext : IJobContext
{
    private ConcurrentDictionary<Guid, List<Guid>> _guidStore;
    private ConcurrentDictionary<Guid, List<int>> _intStore;
    private ConcurrentDictionary<Guid, List<JobContextObject>> _objectStore;
    private ConcurrentDictionary<Guid, List<string>> _stringStore;

    public JobContext(){

    }

    public (Guid, bool) TryGetGuid(Guid jobId, Func<Guid, bool> predicate)
    {
        if(_guidStore.TryGetValue(jobId, out List<Guid>? guidStore)){
            return (guidStore.FirstOrDefault(predicate), true);
        }

        return (default, false);
    }

    public (int, bool) TryGetInt(Guid jobId, Func<int, bool> predicate)
    {
        if(_intStore.TryGetValue(jobId, out List<int>? intStore)){
            return (intStore.FirstOrDefault(predicate), true);
        }

        return (default, false);
    }

    public (T, bool) TryGetObject<T>(Guid jobId, Func<JobContextObject, bool> predicate)
    {
        if(_objectStore.TryGetValue(jobId, out List<JobContextObject>? objectStore)){
            JobContextObject contextObject = objectStore.FirstOrDefault(predicate);

            contextObject.obj = Convert.ChangeType(contextObject.obj, contextObject.type);

            return ((T)contextObject.obj, true);
        }

        return (default, false);
    }

    public (string, bool) TryGetString(Guid jobId, Func<string, bool> predicate)
    {
        if(_stringStore.TryGetValue(jobId, out List<string>? stringStore)){
            return (stringStore.FirstOrDefault(predicate) ?? string.Empty, true);
        }

        return (string.Empty, false);
    }

    public void TryStoreGuid(Guid jobId, Guid value)
    {
        _ =_guidStore.AddOrUpdate(
            jobId,
            _ => [value],
            (_, store) => {
                lock (store){
                    store.Add(value);
                };
                return store;
            });
    }

    public void TryStoreInt(Guid jobId, int value)
    {
        throw new NotImplementedException();
    }

    public void TryStoreObject<T>(Guid jobId, T value)
    {
        throw new NotImplementedException();
    }

    public void TryStoreString(Guid jobId, string value)
    {
        throw new NotImplementedException();
    }
}
