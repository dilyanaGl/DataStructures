using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            var textEditor = new TextEditor();
            while (true)
            {
                string line = Console.ReadLine();

                var regex = new Regex("\"");

                string[] lineParts = regex.Split(line);  // line.Split(regex);


                string[] commandLine = lineParts[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var method = typeof(TextEditor)
                    .GetMethods()
                    .FirstOrDefault(p => p.Name.Equals(commandLine[0], StringComparison.InvariantCultureIgnoreCase));

                var commandParams = commandLine.Skip(1).ToList();

                if (commandLine[0].Equals("users", StringComparison.InvariantCultureIgnoreCase))
                {
                    commandParams.Add("");
                }

                if (method == null && commandLine.Length > 1)
                {
                    method = typeof(TextEditor)
                    .GetMethods()
                    .FirstOrDefault(p => p.Name.Equals(commandLine[1], StringComparison.InvariantCultureIgnoreCase));

                    commandParams[0] = commandLine[0];

                    if (lineParts.Length > 1)
                    {
                        commandParams.Add(lineParts[1]);
                    }
                }

                if (method == null)
                {
                    continue;
                }

                try
                {
                var methodParams = method.GetParameters();
                var parameters = new object[methodParams.Length];

                for (int i = 0; i < methodParams.Length; i++)
                {
                    var type = methodParams[i].ParameterType;
                    parameters[i] = Convert.ChangeType(commandParams.ToArray()[i], type);
                }

                    string result = String.Empty;
                    var returnResult = method.Invoke(textEditor, parameters);
                    if (method.ReturnType != typeof(void))
                    {
                        if (method.ReturnType.IsGenericType)
                        {
                            var sb = new StringBuilder();
                            foreach (var word in (IEnumerable<string>)returnResult)
                            {
                                sb.AppendLine(word);
                            }

                            result = sb.ToString().TrimEnd();
                        }
                        else
                        {
                            result = String.Join("", returnResult);
                        }


                    }
                    Console.WriteLine(result);


                }
                catch
                {
                    // as mentioned in the assignment, exceptions are to be ignored
                }



            }
        }

    }
}
