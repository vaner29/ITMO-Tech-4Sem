using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace AnalyzerTemplate
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TryMethodsAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "TryMethodsAnalyzer";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources2.AnalyzerTitle), Resources2.ResourceManager, typeof(Resources2));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources2.AnalyzerMessageFormat), Resources2.ResourceManager, typeof(Resources2));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources2.AnalyzerDescription), Resources2.ResourceManager, typeof(Resources2));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(AnalyzeMethod, SyntaxKind.MethodDeclaration);
        }
        
        private static void AnalyzeMethod(SyntaxNodeAnalysisContext context)
        {
            var methodSyntax = (MethodDeclarationSyntax)context.Node;
            if (methodSyntax.Identifier.Text.Contains("Try") && methodSyntax.ReturnType.ToString() != "bool")
            {
                // var properties = new Dictionary<string, string>();
                // properties.Add("returnType", methodSyntax.ReturnType.ToString());
                context.ReportDiagnostic(Diagnostic.Create(Rule, methodSyntax.GetLocation()));
            }
        }
    }
}
