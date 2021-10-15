using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LayerCake.DataClasses;

namespace LayerCake
{
    public class LayerCakeWhereVisitor : LayerCakeBaseVisitor<ILayer> 
    {
        ConditionLayer layer;

        public override ILayer VisitWhen_statement([NotNull] LayerCakeParser.When_statementContext context)
        {
            string name = context.GetChild(2).GetText();

            layer = new ConditionLayer(name);

            return base.VisitWhen_statement(context);
        }

        public override ILayer VisitIs_statement([NotNull] LayerCakeParser.Is_statementContext context)
        {
            var from = context.GetChild(0)
                .GetText();
            var to = context.GetChild(2)
                .GetText();

            layer.AddMap(from, to);

            return base.VisitIs_statement(context);
        }

        public override ILayer VisitToggle_statement([NotNull] LayerCakeParser.Toggle_statementContext context)
        {
            var layer = context.GetChild(1)
                   .GetText();
            var key = context.GetChild(3)
                .GetText();

            this.layer.AddToggle(key, layer);

            return base.VisitToggle_statement(context);
        }

        public override ILayer Visit(IParseTree tree)
        {
            base.Visit(tree);
            return layer;
        }
    }
}