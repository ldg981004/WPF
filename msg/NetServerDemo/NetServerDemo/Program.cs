// See https://aka.ms/new-console-template for more information
using NetServerDemo;
using static NetServerDemo.Model;

Console.WriteLine("메신저 서버");

var rt = new ServerRuntime();
rt.InitRuntime();
rt.Run();
rt.Stop();


