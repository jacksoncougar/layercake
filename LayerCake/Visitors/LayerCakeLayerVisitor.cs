using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LayerCake.DataClasses;
using System;

namespace LayerCake
{
    public class LayerCakeLayerVisitor : LayerCakeBaseVisitor<SymbolTable> 
    {
        readonly SymbolTable symbolTable;
        Id currentLayer;

        public LayerCakeLayerVisitor(SymbolTable symbolTable)
        {
            this.symbolTable = symbolTable;
        }

        public override SymbolTable VisitLayer_header([NotNull] LayerCakeParser.Layer_headerContext context)
        {
            string name = context.GetChild(0).GetText();

            this.currentLayer = symbolTable.AddSymbol(new Layer(name: name));

            return base.VisitLayer_header(context);
        }

        public override SymbolTable VisitIs_statement([NotNull] LayerCakeParser.Is_statementContext context)
        {
            var from = context.GetChild(0)
                .GetText();
            var to = context.GetChild(2)
                .GetText();

            Id id_ = symbolTable.AddSymbol(new Map(from: from, to: to));
            symbolTable.AddLink(this.currentLayer, id_);

            return base.VisitIs_statement(context);
        }

        public override SymbolTable VisitToggle_statement([NotNull] LayerCakeParser.Toggle_statementContext context)
        {
            var layer = context.GetChild(1)
                   .GetText();
            var key = context.GetChild(3)
                .GetText();

            Id id_ = symbolTable.AddSymbol(new Toggle(key: key, layer: layer));
            symbolTable.AddLink(this.currentLayer, id_);

            return base.VisitToggle_statement(context);
        }

        public override SymbolTable Visit(IParseTree tree)
        {
            base.Visit(tree);
            return symbolTable;
        }

        public override SymbolTable VisitWhen_block([NotNull] LayerCakeParser.When_blockContext context)
        {
            new LayerCakeWhereVisitor(symbolTable, currentLayer).Visit(context);

            return base.VisitWhen_block(context);
        }
    }
}