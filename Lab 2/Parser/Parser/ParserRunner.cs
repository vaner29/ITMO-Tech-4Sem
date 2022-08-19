using Parser.Parsers;

namespace Parser;

public class ParserRunner
{
    public static void Main()
    {
        var modelParser = new ModelParser();
        modelParser.ParseModels();
        modelParser.CreateDeclarationSyntax();
        modelParser.CreateClientModel();
        var controllerParser = new ControllerParser();
        controllerParser.ParseControllers();
        controllerParser.ConstructMethods();
        controllerParser.ConstructController();

    }
}