using System;

namespace OrderBouncer.GoogleDrive.Interfaces.Architectors;

public interface IGoogleDriveArchitector
{
    public Task Execute(int orderId);
}
