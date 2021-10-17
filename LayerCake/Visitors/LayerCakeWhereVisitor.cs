using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LayerCake.DataClasses;

namespace LayerCake
{
    public class LayerCakeWhereVisitor : LayerCakeBaseVisitor<SymbolTable> 
    {
        SymbolTable symbolTable;
        Id currentLayer;
        Id currentCondition;

        public LayerCakeWhereVisitor(SymbolTable layerTable, Id currentLayer)
        {
            this.symbolTable = layerTable;
            this.currentLayer = currentLayer;
        }

        public LayerCakeWhereVisitor(SymbolTable symbolTable)
        {
            this.symbolTable = symbolTable;
        }

        public override SymbolTable VisitWhen_statement([NotNull] LayerCakeParser.When_statementContext context)
        {
            string name = context.GetChild(2).GetText();

            currentCondition = symbolTable.AddSymbol(new When(name: name));

            return base.VisitWhen_statement(context);
        }

        public override SymbolTable VisitIs_statement([NotNull] LayerCakeParser.Is_statementContext context)
        {
            var from = context.GetChild(0)
                .GetText();
            var to = context.GetChild(2)
                .GetText();

            Id id_ = symbolTable.AddSymbol(new Map(from: from, to: to));
            symbolTable.AddLink(this.currentCondition, id_);
            if(currentLayer)
            {
                symbolTable.AddLink(this.currentLayer, id_);
            }

            return base.VisitIs_statement(context);
        }

        public override SymbolTable VisitSwap_statement([NotNull] LayerCakeParser.Swap_statementContext context)
        {
            var from = context.GetChild(1)
                .GetText();
            var to = context.GetChild(3)
                .GetText();

            Id id1_ = symbolTable.AddSymbol(new Map(from: from, to: to));
            Id id2_ = symbolTable.AddSymbol(new Map(from: to, to: from));
            symbolTable.AddLink(this.currentCondition, id1_);
            symbolTable.AddLink(this.currentCondition, id2_);
            if (currentLayer)
            {
                symbolTable.AddLink(this.currentLayer, id1_);
                symbolTable.AddLink(this.currentLayer, id2_);
            }

            return base.VisitSwap_statement(context);
        }

        public override SymbolTable VisitToggle_statement([NotNull] LayerCakeParser.Toggle_statementContext context)
        {
            var layer = context.GetChild(1)
                   .GetText();
            var key = context.GetChild(3)
                .GetText();

            Id id_ = symbolTable.AddSymbol(new Toggle(key: key, layer: layer));
            symbolTable.AddLink(this.currentCondition, id_);
            if (currentLayer)
            {
                symbolTable.AddLink(this.currentLayer, id_);
            }

            return base.VisitToggle_statement(context);
        }

        public override SymbolTable Visit(IParseTree tree)
        {
            base.Visit(tree);
            return symbolTable;
        }
    }
}