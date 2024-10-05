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

    static async Task CallServiceAsync(string serviceName)
    {
        Console.WriteLine($"{serviceName} started on Thread ID: {Thread.CurrentThread.ManagedThreadId}");

        // Simulate async service call with delay
        await Task.Delay(1000);

        Console.WriteLine($"{serviceName} finished on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
    }
}
