using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace PakFileReader
{
    /// <summary>
    /// POC Code.
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Chrome Pak Editor");

            var pakfile = new ChromePakfile(args[0]);

            var resource101 = pakfile.GetResource(101);

            pakfile.UpdateResource(101, resource101);
            

            //var position = 0;
            //do
            //{
            //    if (position + 1 > resourceCount)
            //        break;

            //    var size = resources[position+1].Item2 - resources[position].Item2;
            //    if (size == 0)
            //    {
            //        position++;
            //        continue;
            //    }

            //    buffer = new byte[size];
            //    fs.Seek(resources[position].Item2, SeekOrigin.Begin);
            //    readLen = fs.Read(buffer, 0, buffer.Length);
            //    var str = Encoding.UTF8.GetString(buffer, 0, readLen);

            //    if(str.Contains("\"display_in_new_tab_page\": true"))
            //        System.Diagnostics.Debugger.Break();

            //    try
            //    {
            //        //Some of this is optional.
            //        Console.WriteLine("Extracting Resource...");
            //        File.WriteAllBytes($"Resources\\{resources[position].Item1}", buffer);

            //        Console.WriteLine(
            //            $"Attempting to parse resource ({position}) id: {resources[position].Item1}; size: {size} as json.");
            //        var obj = JsonConvert.DeserializeObject<dynamic>(str);

            //        try
            //        {
            //            if(File.Exists($"Resources\\{resources[position].Item1}.json"))
            //                File.Delete($"Resources\\{resources[position].Item1}.json");

            //            File.Move($"Resources\\{resources[position].Item1}",
            //                $"Resources\\{resources[position].Item1}.json");
            //        }
            //        catch (Exception ex)
            //        {
            //            int a23 = 0;
            //        }

            //        //Console.WriteLine($"Json: {obj}");
            //    }
            //    catch (JsonException ex)
            //    {
            //        //resource is not a json resource.
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.Error.WriteLine(ex.Message);
            //        Console.ResetColor();


            //        if (buffer.Length > 4)
            //        {

            //            //PNG Header
            //            if (buffer[0] == 137 && buffer[1] == 80)
            //            {
            //                try
            //                {
            //                    if (File.Exists($"Resources\\{resources[position].Item1}.png"))
            //                        File.Delete($"Resources\\{resources[position].Item1}.png");

            //                    File.Move($@"Resources\{resources[position].Item1}",
            //                        $@"Resources\{resources[position].Item1}.png");
            //                }
            //                catch (Exception ex2)
            //                {
            //                    int ja = 0;
            //                }
            //            }


            //            //html
            //            if (str.StartsWith("<!doctype html>"))
            //            {
            //                if (File.Exists($"Resources\\{resources[position].Item1}.html"))
            //                    File.Delete($"Resources\\{resources[position].Item1}.html");

            //                File.Move($@"Resources\{resources[position].Item1}",
            //                    $@"Resources\{resources[position].Item1}.html");
            //            }

            //            //Xml type files
            //            try
            //            {
            //                XmlDocument doc = new XmlDocument();
            //                doc.LoadXml(str);

            //                if (File.Exists($"Resources\\{resources[position].Item1}.xml"))
            //                    File.Delete($"Resources\\{resources[position].Item1}.xml");

            //                File.Move($@"Resources\{resources[position].Item1}",
            //                    $@"Resources\{resources[position].Item1}.xml");
            //            }
            //            catch (Exception notXml)
            //            {
            //                int as4 = 0;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //    position++;

            //} while (position+1 <= resources.Count);

            ////Read the first resource.




            //int a = 0;
            //}
        }
    }
}
