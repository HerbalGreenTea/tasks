using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace FluentApi.Graph
{
    public class DotGraphBuilder
    {
        private static GraphBuilder graphBuilder = new GraphBuilder();

        public static GraphBuilder DirectedGraph(string graphName)
        {
            graphBuilder.DirectedGraph(graphName);
            return graphBuilder;
        }

        public static GraphBuilder NondirectedGraph(string graphName)
        {
            graphBuilder.NondirectedGraph(graphName);
            return graphBuilder;
        }
    }

    //public static class GraphExtension
    //{
    //    public static string Build(this Graph graph)
    //    {
    //        var str = graph.ToDotFormat();
    //        return graph.ToDotFormat();
    //    }

    //    public static Graph AddNode(this GraphNode graph, string name)
    //    {

    //        return res;
    //    }
    //}

    public class GraphBuilder
    {
        private Graph graph;
        private List<GraphNode> graphNodes = new List<GraphNode>();
        private List<GraphEdge> graphEdges = new List<GraphEdge>();

        // плохой способ определить ребро или точка, заменить!
        private bool isNode = false;

        public void DirectedGraph(string graphName)
        {
            graph = new Graph(graphName, true, true);
        }

        public void NondirectedGraph(string graphName)
        {
            graph = new Graph(graphName, false, true);
        }

        public GraphBuilder AddNode(string name)
        {
            var node = graph.AddNode(name);
            graphNodes.Add(node);
            isNode = true;
            return this;
        }

        public GraphBuilder AddEdge(string sourceNode, string destinationNode)
        {
            var edge = graph.AddEdge(sourceNode, destinationNode);
            graphEdges.Add(edge);
            isNode = false;
            return this;
        }

        public GraphBuilder With(Func<GraphBuilder, GraphBuilder> func)
        {
            func(this);
            return this;
        }

        public GraphBuilder With(Action<GraphBuilder> action)
        {
            action(this);
            return this;
        }

        public GraphBuilder Color(string color)
        {
            if (isNode)
                graphNodes.Last().Attributes.Add("color", color);
            else
                graphEdges.Last().Attributes.Add("color", color);

            return this;
        }

        public GraphBuilder Shape(NodeShape nodeShape)
        {
            graphNodes.Last().Attributes.Add("shape", nodeShape.ToString().ToLower());
            return this;
        }

        public GraphBuilder FontSize(int fontSize)
        {
            if (isNode)
                graphNodes.Last().Attributes.Add("fontsize", fontSize.ToString());
            else
                graphEdges.Last().Attributes.Add("fontsize", fontSize.ToString());

            return this;
        }

        public GraphBuilder Label(string text)
        {
            if (isNode)
                graphNodes.Last().Attributes.Add("label", text);
            else
                graphEdges.Last().Attributes.Add("label", text);

            return this;
        }

        public GraphBuilder Weight(int weight)
        {
            graphEdges.Last().Attributes.Add("weight", weight.ToString());
            return this;
        }

        public string Build()
        {
            var result = graph.ToDotFormat();
            return graph.ToDotFormat();
        }
    }

    //public static class GraphAtributes // подумать как вынести общие для точки и ребра методы в отдельный класс
    //{
    //    public static GraphNode Label (this GraphNode node, string text)
    //    {
    //        node.Attributes.Add("label", text);
    //        return node;
    //    }

    //    public static GraphNode Color (this GraphNode node, string color)
    //    {
    //        node.Attributes.Add("color", color);
    //        return node;
    //    }

    //    public static GraphNode FontSize (this GraphNode node, int fontSize)
    //    {
    //        node.Attributes.Add("fontsize", fontSize.ToString());
    //        return node;
    //    }

    //    public static GraphNode Shape (this GraphNode node, NodeShape nodeShape)
    //    {
    //        node.Attributes.Add("shape", nodeShape.ToString().ToLower());
    //        return node;
    //    }
    //}

    //public abstract class Atrib
    //{

    //}

    public enum NodeShape
    {
        Box,
        Ellipse
    }
}