// See https://aka.ms/new-console-template for more information

using Domain;
using TaskProjectBackend.Application.Services;
using TaskProjectBackend.DataAccess;

Console.WriteLine("Hello, World!");
TaskService taskserv = new();

var frompost = taskserv.Post(new Domain.Task() { Name = "task", Color = "blue" });
var fromGet = taskserv.Get(1);


Console.WriteLine(fromGet);

