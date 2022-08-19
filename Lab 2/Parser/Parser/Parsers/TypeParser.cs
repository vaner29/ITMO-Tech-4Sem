using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Parser.Parsers;

public class TypeParser
{
    private Dictionary<string, TypeSyntax> _types = new Dictionary<string, TypeSyntax>
    {
        { "int", SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)) },
        { "long", SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.LongKeyword)) },
        { "String", SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)) },
        { "char", SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.CharKeyword)) },
        { "short", SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ShortKeyword)) },
        { "float", SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.FloatKeyword)) },
        { "double", SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.DoubleKeyword)) }
    };

    public TypeSyntax GetType(string line)
    {
        if (_types.ContainsKey(line)) return _types[line];
        if (!line.Contains('<')) return SyntaxFactory.IdentifierName(line);
        return SyntaxFactory.GenericName(SyntaxFactory.Identifier(line.Substring(0, line.IndexOf('<'))))
            .WithTypeArgumentList(
                SyntaxFactory.TypeArgumentList(
                    SyntaxFactory.SingletonSeparatedList<TypeSyntax>(GetType(line.Substring(line.IndexOf('<') + 1,
                        line.LastIndexOf('>') - line.IndexOf('<') - 1)))));
    }
}