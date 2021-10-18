using Antlr4.Runtime.Misc;
using LayerCake.DataClasses;

namespace LayerCake
{
    public class LayerCakeVisitor : LayerCakeBaseVisitor<SymbolTable>
    {
        readonly SymbolTable symbolTable;

        public LayerCakeVisitor(SymbolTable layerTable)
        {
            this.symbolTable = layerTable;
        }

        public override SymbolTable VisitLayer_block([NotNull] LayerCakeParser.Layer_blockContext context)
        {
            new LayerCakeLayerVisitor(symbolTable).Visit(context);
            return base.DefaultResult;
        }

        public override SymbolTable VisitIs_statement([NotNull] LayerCakeParser.Is_statementContext context)
        {
            var from = context.GetChild(0)
                .GetText();
            var to = context.GetChild(2)
                .GetText();

            Id id = symbolTable.AddSymbol(new Map(from: from, to: to));

            return base.VisitIs_statement(context);
        }
        public override SymbolTable VisitSwap_statement([NotNull] LayerCakeParser.Swap_statementContext context)
        {
            var from = context.GetChild(1)
                .GetText();
            var to = context.GetChild(3)
                .GetText();

            symbolTable.AddSymbol(new Map(from: from, to: to));
            symbolTable.AddSymbol(new Map(from: to, to: from));

            return base.VisitSwap_statement(context);
        }

        public override SymbolTable VisitToggle_statement([NotNull] LayerCakeParser.Toggle_statementContext context)
        {
            var layer = context.GetChild(1)
                .GetText();
            var key = context.GetChild(3)
                .GetText();

            symbolTable.AddSymbol<Toggle>(new Toggle(key, layer));

            return base.VisitToggle_statement(context);
        }

        public override SymbolTable VisitWhen_block([NotNull] LayerCakeParser.When_blockContext context)
        {
            new LayerCakeWhereVisitor(symbolTable).Visit(context);
            return base.DefaultResult;
        }
    }
}