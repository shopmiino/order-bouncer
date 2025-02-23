using System;

namespace OrderBouncer.Domain.Models;

public struct JobContextObject
{
    public object Obj {get; set;}
    public Type ObjType {get; set;}

    public JobContextObject(object obj, Type type){
        Obj = obj;
        ObjType = type;
    }

}
