using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LayerCake.DataClasses;
using System;

namespace LayerCake
{
    public class LayerCakeLayerVisitor<T> : LayerCakeBaseVisitor<T> 
        where T : ILayer
    {
        T layer;

        public override T VisitLayer_header([NotNull] LayerCakeParser.Layer_headerContext context)
        {
            string name = context.GetChild(0).GetText();

            layer = (T)Activator.CreateInstance(typeof(T),  name );

            return base.VisitLayer_header(context);
        }

        public override T VisitIs_statement([NotNull] LayerCakeParser.Is_statementContext context)
        {
            var from = context.GetChild(0)
                .GetText();
            var to = context.GetChild(2)
                .GetText();

            layer.AddMap(from, to);

            return base.VisitIs_statement(context);
        }

        public override T VisitToggle_statement([NotNull] LayerCakeParser.Toggle_statementContext context)
        {
            var layer = context.GetChild(1)
                   .GetText();
            var key = context.GetChild(3)
                .GetText();

            this.layer.AddToggle(key, layer);

            return base.VisitToggle_statement(context);
        }

        public override T Visit(IParseTree tree)
        {
            base.Visit(tree);
            return layer;
        }

        public override T VisitWhen_block([NotNull] LayerCakeParser.When_blockContext context)
        {
            ILayer layer = new LayerCakeWhereVisitor().Visit(context);
            this.layer.AddNestedLayer(layer);
            return base.VisitWhen_block(context);
        }
    }
}