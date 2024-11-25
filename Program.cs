namespace TasksApp;

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine($"Main Thread ID: {Thread.CurrentThread.ManagedThreadId}");

        // Simulating an API request with async service calls
        Console.WriteLine("Handling API request...");

        await HandleApiRequest();     

        Console.WriteLine($"API request finished finished on Thread ID: {Thread.CurrentThread.ManagedThreadId}\".");

        await LockProgramAsync();
    }

    static async Task HandleApiRequest()
    {
        // Start timing
        var stopwatch = Stopwatch.StartNew();

        // Simulating multiple async service calls
        var service1Task = CallServiceAsync("Service 1");
        var service2Task = CallServiceAsync("Service 2");
        var service3Task = CallServiceAsync("Service 3");

        // Awaiting all service calls in parallel
        await Task.WhenAll(service1Task, service2Task, service3Task);

        // Log total elapsed time
        stopwatch.Stop();
        Console.WriteLine($"All services completed in {stopwatch.ElapsedMilliseconds}ms");
    }

    static async Task LockProgramAsync()
    {
        Console.WriteLine($"About to deadlock the program...");

        object lock1 = new();
        object lock2 = new();

        var t1 = Task.Run(() =>
        {
            lock (lock1)
            {
                Thread.Sleep(1);
                lock (lock2)
                {
                    Console.WriteLine("Lock1 is locked!");
                }
            }
        });

        var t2 = Task.Run(() =>
        {
            lock (lock2)
            {
                Thread.Sleep(1);
                lock (lock1)
                {
                    Console.WriteLine("Lock2 is locked!");
                }
            }
        });

        await Task.WhenAll(t1, t2);

        Console.WriteLine("Never will be reached...");
    }

    static async Task CallServiceAsync(string serviceName)
    {
        Console.WriteLine($"{serviceName} started on Thread ID: {Thread.CurrentThread.ManagedThreadId}");

        // Simulate async service call with delay
        await Task.Delay(1000);

        Console.WriteLine($"{serviceName} finished on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
    }
}
