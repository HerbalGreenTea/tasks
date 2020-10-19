using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

// Я НАПИСАЛ КАКУ-ТО ЖЕСТЬ, ЗАЛИЛ ПРОСТО ПОСМОТРЕТЬ!!! ПАША ЕГОРОВ И ЮРИЙ ОКУЛОВСКИЙ, ПРОСТИТЕ ЗА ТАКОЙ КОД 
// Я НАПИСАЛ КАКУ-ТО ЖЕСТЬ, ЗАЛИЛ ПРОСТО ПОСМОТРЕТЬ!!! ПАША ЕГОРОВ И ЮРИЙ ОКУЛОВСКИЙ, ПРОСТИТЕ ЗА ТАКОЙ КОД
// Я НАПИСАЛ КАКУ-ТО ЖЕСТЬ, ЗАЛИЛ ПРОСТО ПОСМОТРЕТЬ!!! ПАША ЕГОРОВ И ЮРИЙ ОКУЛОВСКИЙ, ПРОСТИТЕ ЗА ТАКОЙ КОД
// Я НАПИСАЛ КАКУ-ТО ЖЕСТЬ, ЗАЛИЛ ПРОСТО ПОСМОТРЕТЬ!!! ПАША ЕГОРОВ И ЮРИЙ ОКУЛОВСКИЙ, ПРОСТИТЕ ЗА ТАКОЙ КОД
// Я НАПИСАЛ КАКУ-ТО ЖЕСТЬ, ЗАЛИЛ ПРОСТО ПОСМОТРЕТЬ!!! ПАША ЕГОРОВ И ЮРИЙ ОКУЛОВСКИЙ, ПРОСТИТЕ ЗА ТАКОЙ КОД


namespace FluentApi.Graph
{
    public class DotGraphBuilder
    {
        private static GraphBuilder graphBuilder = new GraphBuilder();

        public static GraphBuilder DirectedGraph(string graphName)
        {
            GraphBuilderExtention.Graph = new Graph(graphName, true, true);
            return graphBuilder;
		}

        public static GraphBuilder NondirectedGraph(string graphName)
        {
            GraphBuilderExtention.Graph = new Graph(graphName, false, true);
            return graphBuilder;
        }
    }

    public class GraphBuilder
    {
        public GraphBuilder AddNode(string name)
        {
            var node = GraphBuilderExtention.Graph.AddNode(name);
            GraphBuilderExtention.CurrentNode = node;
            GraphBuilderExtention.IsNode = true;
            return this;
        }

        public GraphBuilder AddEdge(string sourceNode, string destinationNode)
        {
            var edge = GraphBuilderExtention.Graph.AddEdge(sourceNode, destinationNode);
            GraphBuilderExtention.CurrentEdge = edge;
            GraphBuilderExtention.IsNode = false;
            return this;
        }

        public string Build()
        {
            var result = GraphBuilderExtention.Graph.ToDotFormat();
            return GraphBuilderExtention.Graph.ToDotFormat();
        }
    }

    public static class GraphMetods
    {
        public static GraphBuilder With(this GraphBuilder graphBuilder, Func<Methods, Methods> func)
        {
            func(new Methods());
            return graphBuilder;
        }

        public static GraphBuilder With(this GraphBuilder graphBuilder, Action<Methods> action)
        {
            action(new Methods());
            return graphBuilder;
        }
    }

    public class Methods
    {

    }

    public static class GraphBuilderExtention
    {
        public static Graph Graph;
        public static GraphNode CurrentNode;
        public static GraphEdge CurrentEdge;
        public static bool IsNode;

        public static Methods Color(this Methods graphBuilder, string color)
        {
            if (IsNode)
                CurrentNode.Attributes.Add("color", color);
            else
                CurrentEdge.Attributes.Add("color", color);

            return graphBuilder;
        }

        public static Methods Shape(this Methods graphBuilder, NodeShape nodeShape)
        {
            CurrentNode.Attributes.Add("shape", nodeShape.ToString().ToLower());
            return graphBuilder;
        }

        public static Methods FontSize(this Methods graphBuilder, int fontSize)
        {
            if (IsNode)
                CurrentNode.Attributes.Add("fontsize", fontSize.ToString());
            else
                CurrentEdge.Attributes.Add("fontsize", fontSize.ToString());

            return graphBuilder;
        }

        public static Methods Label(this Methods graphBuilder, string text)
        {
            if (IsNode)
                CurrentNode.Attributes.Add("label", text);
            else
                CurrentEdge.Attributes.Add("label", text);

            return graphBuilder;
        }

        public static Methods Weight(this Methods graphBuilder, int weight)
        {
            CurrentEdge.Attributes.Add("weight", weight.ToString());
            return graphBuilder;
        }
    }

    public enum NodeShape
    {
        Box,
        Ellipse
    }
}