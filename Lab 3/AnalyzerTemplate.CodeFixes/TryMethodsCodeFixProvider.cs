using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace AnalyzerTemplate
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(TryMethodsCodeFixProvider)), Shared]
    public class TryMethodsCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(TryMethodsAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;
            var node = root.FindNode(diagnosticSpan);
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf()
                .OfType<MethodDeclarationSyntax>().First();
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: "Change Method to return bool and the element as an out",
                    createChangedDocument: c => ChangeTryMethod(context.Document, declaration, c),
                    equivalenceKey: "Change Method to return bool and the element as an out"),
                diagnostic);
        }

        private async Task<Document> ChangeTryMethod(Document document, MethodDeclarationSyntax typeDecl,
            CancellationToken cancellationToken)
        {
            var editor = await DocumentEditor.CreateAsync(document, cancellationToken);
            var type = typeDecl.ReturnType;

            foreach (var node in typeDecl.DescendantNodes().OfType<ReturnStatementSyntax>())
            {
                if (node.Expression == null)
                {
                    var newRes = SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName("res"),
                            null));
                    var newReturn = SyntaxFactory.ReturnStatement(
                        SyntaxFactory.LiteralExpression(
                            SyntaxKind.FalseLiteralExpression));
                    editor.InsertBefore(node, newRes);
                    editor.ReplaceNode(node, newReturn);
                }
                else
                {
                    var newRes = SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName("res"),
                            node.Expression));
                    var newReturn = SyntaxFactory.ReturnStatement(
                        SyntaxFactory.LiteralExpression(
                            SyntaxKind.TrueLiteralExpression));
                    editor.InsertBefore(node, newRes);
                    editor.ReplaceNode(node, newReturn);
                }
            }

            var newReturnTypeSyntax = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.BoolKeyword));
            editor.ReplaceNode(typeDecl.ReturnType, newReturnTypeSyntax);
            var newParam = SyntaxFactory.Parameter(
                    SyntaxFactory.Identifier("res"))
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.OutKeyword)))
                .WithType(type);
            editor.InsertParameter(typeDecl, 0, newParam);
            return editor.GetChangedDocument();
        }
    }
}