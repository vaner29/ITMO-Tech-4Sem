using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Parser.Entities;

namespace Parser.Parsers;

public class ControllerParser
{
    private static string _className = "";
    private static List<Method> _methods = new List<Method>();
    private static readonly List<MemberDeclarationSyntax> _parsedMethods = new List<MemberDeclarationSyntax>();
    private static TypeParser _typeParser = new TypeParser();

    public void ParseControllers()
    {
        foreach (var file in Directory.GetFiles(
                     @"..\..\..\..\..\JavaServer\src\main\java\com\example\ISU\Controllers"))
        {
            using (var reader = new StreamReader(file, System.Text.Encoding.Default))
            {
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == string.Empty) continue;
                    if (line.Contains("class"))
                        _className = line.Split(" ")[2];
                    if (line.Contains("Mapping"))
                    {
                        _methods.Add(new Method());
                        line = line.Remove(0, line.IndexOf("@"));
                        _methods.Last().QueryType = line.Split("(")[0];
                        line = line.Remove(line.Length - 2);
                        _methods.Last().Path = "http://localhost:8080" + line.Split("\"")[1];
                        line = reader.ReadLine();
                        _methods.Last().ReturnType = line.Split(" ").Where(x => x != "").ToList()[1];
                        _methods.Last().Name = line.Split(" ").Where(x => x != "").ToList()[2].Split("(")[0];
                        line = line.Remove(0, line.IndexOf("(") + 1);
                        line = line.Remove(line.Length - 2);
                        if (line[0] != ')')
                        {
                            for (int i = 0; i <= line.Count(c => c == ','); i++)
                            {
                                _methods.Last().Arguments.Add(new Argument());
                                var temp = line.Split(",")[i].Split(" ").Where(x => x != "").ToList();
                                _methods.Last().Arguments.Last().Param = temp[0];
                                _methods.Last().Arguments.Last().Type = temp[1];
                                _methods.Last().Arguments.Last().Name = temp[2];
                            }
                        }

                        continue;
                    }
                }
            }
        }
    }

    public List<ParameterSyntax> ConstructArguments(List<Argument> arguments)
    {
        var result = new List<ParameterSyntax>();
        foreach (var argument in arguments)
        {
            result.Add(
                SyntaxFactory.Parameter(
                        SyntaxFactory.Identifier(argument.Name))
                    .WithType(_typeParser.GetType(argument.Type)));
        }

        return result;
    }
    
    public LocalDeclarationStatementSyntax ConstructContent(List<Argument> arguments)
    {
        LocalDeclarationStatementSyntax result = null;
        ArgumentSyntax content = null;
        if (arguments.Count(x => x.Param == "@RequestBody") > 1)
            throw new ArgumentException("You can't send more than 1 RequestBody Because I said so");
        if (!arguments.Any(argument => argument.Param == "@RequestBody"))
        {
            content = SyntaxFactory.Argument(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal("")));
        }
        else
        {
            content = SyntaxFactory.Argument(
                SyntaxFactory.IdentifierName(arguments.Find(x => x.Param == "@RequestBody").Name));
        }

        result = SyntaxFactory.LocalDeclarationStatement(
            SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.IdentifierName(
                        SyntaxFactory.Identifier(
                            SyntaxFactory.TriviaList(),
                            SyntaxKind.VarKeyword,
                            "var",
                            "var",
                            SyntaxFactory.TriviaList())))
                .WithVariables(
                    SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                        SyntaxFactory.VariableDeclarator(
                                SyntaxFactory.Identifier("content"))
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.InvocationExpression(
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("JsonContent"),
                                                SyntaxFactory.IdentifierName("Create")))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList(
                                                SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(content))))))));
        return result;
    }

    public LocalDeclarationStatementSyntax ConstructResponse(string param, string path, List<Argument> arguments)
    {
        var newPath = path;
        if (arguments.Any(x => x.Param == "@RequestParam"))
        {
            newPath += "?";
            foreach (var argument in arguments.Where(x => x.Param == "@RequestParam"))
            {
                newPath += argument.Name + "=" + "{" + argument.Name + "}&";
            }

            newPath = newPath.Remove(newPath.Length - 1);
        }
        var interpolatedString = new List<InterpolatedStringContentSyntax>();
        var splitString = string.Join(" ", string.Join(" ", newPath.Split("{")).Split("}")).Split(" ");
        for (int i = 0; i < splitString.Length; i++)
        {
            if (i % 2 == 0)
            {
                interpolatedString.Add(SyntaxFactory.InterpolatedStringText()
                    .WithTextToken(
                        SyntaxFactory.Token(
                            SyntaxFactory.TriviaList(),
                            SyntaxKind.InterpolatedStringTextToken,
                            splitString[i],
                            splitString[i],
                            SyntaxFactory.TriviaList())));
            }
            else
            {
                interpolatedString.Add(SyntaxFactory.Interpolation(SyntaxFactory.IdentifierName(splitString[i])));
            }
        }

        var finalString = SyntaxFactory.InterpolatedStringExpression(
                SyntaxFactory.Token(SyntaxKind.InterpolatedStringStartToken))
            .WithContents(SyntaxFactory.List<InterpolatedStringContentSyntax>(interpolatedString));

        if (param == "@GetMapping")
            return SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName(
                            SyntaxFactory.Identifier(
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.VarKeyword,
                                "var",
                                "var",
                                SyntaxFactory.TriviaList())))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                            SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("response"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.AwaitExpression(
                                            SyntaxFactory.InvocationExpression(
                                                    SyntaxFactory.MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        SyntaxFactory.IdentifierName("_client"),
                                                        SyntaxFactory.IdentifierName("GetAsync")))
                                                .WithArgumentList(
                                                    SyntaxFactory.ArgumentList(
                                                        SyntaxFactory
                                                            .SingletonSeparatedList<ArgumentSyntax>(
                                                                SyntaxFactory.Argument(finalString))))))))));
        else
            return SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName(
                            SyntaxFactory.Identifier(
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.VarKeyword,
                                "var",
                                "var",
                                SyntaxFactory.TriviaList())))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                            SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("response"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.AwaitExpression(
                                            SyntaxFactory.InvocationExpression(
                                                    SyntaxFactory.MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        SyntaxFactory.IdentifierName("_client"),
                                                        SyntaxFactory.IdentifierName("PostAsync")))
                                                .WithArgumentList(
                                                    SyntaxFactory.ArgumentList(
                                                        SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                                            new SyntaxNodeOrToken[]
                                                            {
                                                                SyntaxFactory.Argument(finalString),
                                                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                                SyntaxFactory.Argument(
                                                                    SyntaxFactory.IdentifierName("content"))
                                                            })))))))));

    }
    
    public void ConstructMethods()
    {
        _parsedMethods.Add(SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName("HttpClient"))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                            SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("_client"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.ObjectCreationExpression(
                                                SyntaxFactory.IdentifierName("HttpClient"))
                                            .WithArgumentList(
                                                SyntaxFactory.ArgumentList()))))))
            .WithModifiers(
                SyntaxFactory.TokenList(
                    new []{
                        SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
                        SyntaxFactory.Token(SyntaxKind.StaticKeyword)})));
        foreach (var method in _methods)
        {
            var result = SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.GenericName(
                            SyntaxFactory.Identifier("Task"))
                        .WithTypeArgumentList(
                            SyntaxFactory.TypeArgumentList(
                                SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                    (_typeParser.GetType(method.ReturnType))))),
                    SyntaxFactory.Identifier(method.Name))
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        new[]
                        {
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                            SyntaxFactory.Token(SyntaxKind.AsyncKeyword)
                        }))
                .WithParameterList(
                    SyntaxFactory.ParameterList(
                        SyntaxFactory.SeparatedList<ParameterSyntax>(ConstructArguments(method.Arguments))))
                .WithBody(
                    SyntaxFactory.Block(
                        SyntaxFactory.SingletonList<StatementSyntax>(
                            SyntaxFactory.Block(
                                ConstructContent(method.Arguments),
                                ConstructResponse(method.QueryType, method.Path, method.Arguments),
                                SyntaxFactory.LocalDeclarationStatement(
                                    SyntaxFactory.VariableDeclaration(
                                            SyntaxFactory.IdentifierName(
                                                SyntaxFactory.Identifier(
                                                    SyntaxFactory.TriviaList(),
                                                    SyntaxKind.VarKeyword,
                                                    "var",
                                                    "var",
                                                    SyntaxFactory.TriviaList())))
                                        .WithVariables(
                                            SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                SyntaxFactory.VariableDeclarator(
                                                        SyntaxFactory.Identifier("responseString"))
                                                    .WithInitializer(
                                                        SyntaxFactory.EqualsValueClause(
                                                            SyntaxFactory.AwaitExpression(
                                                                SyntaxFactory.InvocationExpression(
                                                                    SyntaxFactory.MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        SyntaxFactory.MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            SyntaxFactory.IdentifierName("response"),
                                                                            SyntaxFactory.IdentifierName("Content")),
                                                                        SyntaxFactory.IdentifierName(
                                                                            "ReadAsStringAsync"))))))))),
                                SyntaxFactory.ReturnStatement(
                                    SyntaxFactory.InvocationExpression(
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("JsonSerializer"),
                                                SyntaxFactory.GenericName(
                                                        SyntaxFactory.Identifier("Deserialize"))
                                                    .WithTypeArgumentList(
                                                        SyntaxFactory.TypeArgumentList(
                                                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                                _typeParser.GetType(method.ReturnType))))))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList(
                                                SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                                    SyntaxFactory.Argument(
                                                        SyntaxFactory.IdentifierName("responseString"))))))))));
            _parsedMethods.Add(result);
        }
    }

    public void ConstructController()
    {
        File.WriteAllText($@"..\..\..\..\..\Client\Client\{_className}.cs", (SyntaxFactory.CompilationUnit()
.WithUsings(
    SyntaxFactory.List<UsingDirectiveSyntax>(
        new UsingDirectiveSyntax[]{
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.QualifiedName(
                            SyntaxFactory.IdentifierName("System"),
                            SyntaxFactory.IdentifierName("Net")),
                        SyntaxFactory.IdentifierName("Http")),
                    SyntaxFactory.IdentifierName("Json"))),
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.IdentifierName("System"),
                        SyntaxFactory.IdentifierName("Text")),
                    SyntaxFactory.IdentifierName("Json")))}))
.WithMembers(
    SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
        SyntaxFactory.FileScopedNamespaceDeclaration(
            SyntaxFactory.IdentifierName("Client"))
        .WithMembers(
            SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                SyntaxFactory.ClassDeclaration("Controller")
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>(_parsedMethods))))))).NormalizeWhitespace().ToString());
    }
}