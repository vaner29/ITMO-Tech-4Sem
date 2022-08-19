using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
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
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ConstantArgumentsCodeFixProvider)), Shared]
    public class ConstantArgumentsCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ConstantArgumentsAnalyzer.DiagnosticId); }
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
            
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<ArgumentSyntax>().First();
            var argumentName = diagnostic.Properties["argName"];
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: "Add argument name to constants",
                    createChangedDocument: c => AddArgumentName(context.Document, declaration, c, argumentName),
                    equivalenceKey: "Add argument name to constants"),
                diagnostic);
        }

        private async Task<Document> AddArgumentName(Document document, ArgumentSyntax typeDecl, CancellationToken cancellationToken, string argumentName)
        {
            var editor = await DocumentEditor.CreateAsync(document, cancellationToken);
            var newDecl = typeDecl.WithNameColon(SyntaxFactory.NameColon(
                SyntaxFactory.IdentifierName(argumentName)));
            editor.ReplaceNode(typeDecl, newDecl);
            return editor.GetChangedDocument();
        }
    }
}
