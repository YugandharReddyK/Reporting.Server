using Microsoft.Extensions.Logging;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Reporting.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common
{
    public  class MxSRealtimeservice// : IMxSRealtimeService
    {

        //private HubConnection _connection;
        //private static object _lock = new object();
        //private readonly ILogger<MxSRealtimeservice> _logger;
        //private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        //private MxSServerInfo _serverInfo;

        //public MxSRealtimeservice(ILogger<MxSRealtimeservice> logger, MxSServerInfo serverInfo)
        //{
        //    _logger = logger;
        //    _serverInfo = serverInfo;
        //    Connect();
        //}

        //private void Connect()
        //{
        //    CreateDefaultProxy();
        //}
        //private void CleanupConnection()
        //{
        //    try
        //    {
        //        if (_connection != null)
        //        {
        //            _connection.Closed -= connection_Closed;
        //            _connection.DisposeAsync();
        //            _connection = null;
        //        }
        //    }
        //    catch { }
        //}

        //private void CreateDefaultProxy()
        //{
        //    CleanupConnection();
        //    InitConnection();
        //    if (_connection != null)
        //    {
        //        Task.Run(async () =>
        //        {
        //            await _connection.StartAsync().ContinueWith(continuationAction =>
        //            {
        //                if (continuationAction.Exception == null)
        //                {
        //                    if (IsConnected())
        //                    {
        //                        Console.WriteLine("Connected Successfully");
        //                    }
        //                    else
        //                    {
        //                        Reconnect().Wait();
        //                    }
        //                }
        //                else
        //                {
        //                    HandleException(continuationAction.Exception);
        //                }
        //            });
        //        }).Wait();
        //    }
        //}

        //private void HandleException(Exception ex)
        //{
        //    Console.WriteLine("Exception occured while connecting. Exception : " + ex.Message);
        //    Reconnect();
        //}

        //private async Task connection_Closed(Exception ex)
        //{
        //    await Reconnect();
        //}

        //private void InitConnection()
        //{
        //    _connection = new HubConnectionBuilder()
        //                  .WithUrl($"{_serverInfo.NetworkInfo.Address}:{_serverInfo.NetworkInfo.Port}/{_serverInfo.HubName}")
        //                  .Build();
        //    _connection.Closed += connection_Closed;

        //    Console.WriteLine("Connection is Initialized");
        //}

        //public bool IsConnected()
        //{
        //    return _connection != null;// && _connection.State == HubConnectionState.Connected;
        //}

        //private Task Reconnect()
        //{
        //    return Task.Run(() =>
        //    {
        //        lock (_lock)
        //        {
        //            try
        //            {
        //                if (!IsConnected())
        //                {
        //                    Console.WriteLine("Reconnecting...");
        //                    Thread.Sleep(TimeSpan.FromSeconds(3));
        //                    Connect();
        //                }
        //            }
        //            catch { }
        //        }
        //    });
        //}

        //public void BroadcastCustomerReportGeneratedForWell(Guid wellId)
        //{
        //    _semaphoreSlim.Wait();
        //    try
        //    {
        //        _connection?.SendAsync("BroadcastCustomerReportGeneratedForWell", wellId).Wait();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, "Error in invoking the BroadcastCustomerReportGeneratedForWell event");
        //    }
        //    finally
        //    {
        //        _semaphoreSlim.Release();
        //    }
        //}
    }
}
