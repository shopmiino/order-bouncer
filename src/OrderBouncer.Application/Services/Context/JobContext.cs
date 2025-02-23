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
        _guidStore = [];
        _intStore = [];
        _objectStore = [];
        _stringStore = [];
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
            JobContextObject contextObject = objectStore.LastOrDefault(predicate);

            contextObject.Obj = Convert.ChangeType(contextObject.Obj, contextObject.ObjType);

            return ((T)contextObject.Obj, true);
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
        _ = _guidStore.AddOrUpdate(
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
        _ = _intStore.AddOrUpdate(
            jobId,
            _ => [value],
            (_, store) => {
                lock (store){
                    store.Add(value);
                };
                return store;
            });
    }

    public void TryStoreObject<T>(Guid jobId, T value)
    {
        if(value is null) throw new ArgumentNullException("The value that are going to be added to the store can not be null");

        _ = _objectStore.AddOrUpdate(
            jobId,
            _ => [new JobContextObject(value, typeof(T))],
            (_, store) => {
                lock (store){
                    store.Add(new JobContextObject(value, typeof(T)));
                };
                return store;
            });
    }

    public void TryStoreString(Guid jobId, string value)
    {
        _ = _stringStore.AddOrUpdate(
            jobId,
            _ => [value],
            (_, store) => {
                lock (store){
                    store.Add(value);
                };
                return store;
            });
    }
}
