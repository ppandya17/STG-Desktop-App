using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qlik.Engine;
using Qlik.Sense.Client;

namespace STG
{
    class TextHelper
    {
        public static void Print(string name, string title)
        {
            WriteLine(4, name + ": " + title);
        }

        public static void Print(string name, IHyperCubeDef hyperCube, string title, IApp app)
        {
            if (hyperCube == null)
                return;
            WriteLine(4, name + ": " + title + " " + SerializeDimensions(hyperCube.Dimensions, app) +
                         " " +
                         SerializeMeasures(hyperCube.Measures, app));
        }

        private static string SerializeMeasures(IEnumerable<NxMeasure> measures, IApp app)
        {
            if (measures == null)
                return null;
            return "[" +
                   String.Join(", ", measures.Select(m => GetMeasureExpression(m, app))) +
                   "]";
        }

        private static string GetMeasureExpression(INxMeasure measure, IApp app)
        {
            if (measure == null)
                return null;
            if (String.IsNullOrWhiteSpace(measure.LibraryId))
                return measure.Def.Def;

            var meas = app.GetMeasure(measure.LibraryId);

            return meas.Properties.Measure.Def;
        }

        private static string SerializeDimensions(IEnumerable<NxDimension> dimensions, IApp app)
        {
            if (dimensions == null)
                return null;
            return "[" +
                   String.Join(", ", dimensions.Select(d =>
                       "{" +
                       String.Join(", ", GetDimensionFields(d, app)) +
                       "}")) +
                   "]";
        }

        private static IEnumerable<string> GetDimensionFields(INxDimension dimension, IApp app)
        {
            if (dimension == null)
                return null;
            if (String.IsNullOrWhiteSpace(dimension.LibraryId))
                return dimension.Def.FieldDefs;

            var dim = app.GetDimension(dimension.LibraryId);

            return dim.Properties.Dim.FieldDefs;
        }

        public static string Indent(int n = 1)
        {
            return String.Join("", Enumerable.Repeat("    ", n));
        }

        public static void WriteLine(string s)
        {
            Console.WriteLine(s);

            //writing in log file
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                        GetTempPath() + "STG_Log-1.txt");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.", System.DateTime.Now, s);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }

        public static void WriteLine(int n, string s)
        {
            Console.WriteLine(Indent(n) + s);

        }

        public static String GetTempPath()
        {
            string path = System.Environment.GetEnvironmentVariable("TEMP");
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }
    }
}
