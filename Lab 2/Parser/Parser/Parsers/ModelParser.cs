namespace Parser.Parsers;
using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


public class ModelParser
{
    private static string _className = "";
    private static readonly List<KeyValuePair<string, string>> _fields = new List<KeyValuePair<string, string>>();
    private static FieldParser _fieldParser = new FieldParser();
    private static ClassDeclarationSyntax _syntax;
    public void ParseModels()
    {
        foreach (var file in Directory.GetFiles(@"..\..\..\..\..\JavaServer\src\main\java\com\example\ISU\Models"))
        {
            using (var reader = new StreamReader(file, System.Text.Encoding.Default))
            {
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.Contains("class")) continue;
                    _className = line.Split(" ")[2];
                    while ((line = reader.ReadLine()) != null && line != "}")
                    {
                        var curLine = line.Remove(line.Length - 1);
                        _fields.Add(new KeyValuePair<string, string>(curLine.Split(" ")[^1],curLine.Split(" ")[^2]));
                    }
                    break;
                }
            }
        }
    }
    
    
    public void CreateDeclarationSyntax()
    {
        var someshit = SyntaxFactory.ClassDeclaration(_className);
        someshit = someshit.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
        foreach (var pair in _fields)
        {
            someshit = someshit.AddMembers(_fieldParser.GetField(pair.Key, pair.Value));
        }
        _syntax = someshit;
    }

    public void CreateClientModel()
    {
        File.WriteAllText($@"..\..\..\..\..\Client\Client\{_className}.cs", 
            SyntaxFactory.CompilationUnit()
                .WithMembers(
                    SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                        SyntaxFactory.FileScopedNamespaceDeclaration(
                                SyntaxFactory.IdentifierName("Client"))
                            .WithMembers(
                                SyntaxFactory.SingletonList<MemberDeclarationSyntax>(_syntax)))).NormalizeWhitespace().ToString());
    }
}