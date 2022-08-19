// See https://aka.ms/new-console-template for more information

using System.Numerics;
using GeneticAlgo.Shared;
using GeneticAlgo.Shared.Models;
using GeneticAlgo.Shared.Tools;
using Newtonsoft.Json;
using Serilog;
using ExecutionContext = GeneticAlgo.Shared.Tools.ExecutionContext;
using Formatting = System.Xml.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

/*Logger.Init();
Log.Information("Start console polygon");
var dummyExecutionContext = new ExecutionContext(10);
dummyExecutionContext.Reset();
await dummyExecutionContext.ExecuteIterationAsync();
Log.Information("Polygon end");*/

var runConfig = new RunConfiguration
{
    TimeDelay = 10,
    DotAmount = 1000,
    IterationAmount = 500,
    FMax = 0.001f,
    Obstacles = new List<BarrierCircle>
    {
        new BarrierCircle(new Vector2(0.73218833804130554f, 0.95921106934547424f), 22),
        new BarrierCircle(new Vector2(0.9211785793304443f, 0.31001200377941132f), 22),
        new BarrierCircle(new Vector2(0.7558014154434204f, 0.7025460004806519f), 21),
        new BarrierCircle(new Vector2(0.95513463541865349f, 1.3919896245002747f), 23),
    }
};
            
var fileName = "Config.json"; 
File.WriteAllText(@"D:\work\vaner29\Lab 4\GeneticAlgo.WpfInterface\" + fileName, JsonConvert.SerializeObject(
    runConfig,
    (Newtonsoft.Json.Formatting)Formatting.Indented,
    new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.All,
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
    }));

var random = new Random();

Console.WriteLine("Hello, World!");
