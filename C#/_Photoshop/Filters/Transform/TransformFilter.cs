using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MyPhotoshop
{
    public class TransformFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        string name;
        ITransformer<TParameters> transformer;
        public TransformFilter(string name, ITransformer<TParameters> transformer)
        {
            this.name = name;
            this.transformer = transformer;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.width, original.height);
            transformer.Prepare(oldSize, parameters);
            var result = new Photo(transformer.ResultSize.Width, transformer.ResultSize.Height);

            for (int x = 0; x < transformer.ResultSize.Width; x++)
                for (int y = 0; y < transformer.ResultSize.Height; y++)
                {
                    var point = new Point(x, y);
                    var oldPoint= transformer.PointMap(point);

                    if (oldPoint.HasValue)
                        result[x, y] = original[oldPoint.Value.X, oldPoint.Value.Y];
                }
            return result;
        }

        public override string ToString() => name;
    }

    public class TransformFilter : TransformFilter<EmptyParameters>
    {
        public TransformFilter(string name, Func<Size, Size> sizeTransform, Func<Point, Size, Point> pointTransform)
            : base (name, new FreeTransformer(sizeTransform, pointTransform))
        {

        }
    }
}
