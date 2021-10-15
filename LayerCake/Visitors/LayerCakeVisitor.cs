using Antlr4.Runtime.Misc;
using LayerCake.DataClasses;

namespace LayerCake
{
    public class LayerCakeVisitor : LayerCakeBaseVisitor<dynamic>
    {
        readonly ILayerTable LayerTable;

        public LayerCakeVisitor(ILayerTable layerTable)
        {
            LayerTable = layerTable;
        }

        public override dynamic VisitLayer_block([NotNull] LayerCakeParser.Layer_blockContext context)
        {
            ILayer layer = new LayerCakeLayerVisitor<Layer>().Visit(context);
            LayerTable.AddLayer(layer);
            return base.DefaultResult;
        }

        public override dynamic VisitIs_statement([NotNull] LayerCakeParser.Is_statementContext context)
        {
            var from = context.GetChild(0)
                .GetText();
            var to = context.GetChild(2)
                .GetText();

            LayerTable.Keyboard.AddMap(from, to);

            return base.VisitIs_statement(context);
        }
        public override dynamic VisitSwap_statement([NotNull] LayerCakeParser.Swap_statementContext context)
        {
            var from = context.GetChild(1)
                .GetText();
            var to = context.GetChild(3)
                .GetText();

            LayerTable.Keyboard.AddMap(from, to);
            LayerTable.Keyboard.AddMap(from: to, to: from);

            return base.VisitSwap_statement(context);
        }

        public override dynamic VisitToggle_statement([NotNull] LayerCakeParser.Toggle_statementContext context)
        {
            var layer = context.GetChild(1)
                .GetText();
            var key = context.GetChild(3)
                .GetText();

            LayerTable.Keyboard.AddToggle(key, layer);

            return base.VisitToggle_statement(context);
        }

        public override dynamic VisitWhen_block([NotNull] LayerCakeParser.When_blockContext context)
        {
            ILayer layer = new LayerCakeWhereVisitor().Visit(context);
            LayerTable.Keyboard.AddNestedLayer(layer);
            return base.VisitWhen_block(context);
        }
    }
}