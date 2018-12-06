using sqlApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sql
{
    class Program
    {
        static void Main(string[] args)
        {
            server _server = new server(constantinople.XML_FILEPATH);
            dir _dir = new dir(constantinople.XML_FILEPATH);

            if (args == null || args.Length == 0) return;

            switch (args[0])
            {
                case "help":
                    runHelp();
                    break;
                case "servername":
                    Console.WriteLine(_server.Name);
                    setter(args, () => new server(constantinople.XML_FILEPATH, args[2], string.Empty));  
                    break;
                case "db":
                    Console.WriteLine(_server.Db);
                    setter(args, () => new server(constantinople.XML_FILEPATH, string.Empty, args[2]));
                    break;
                case "cs": // connection string
                    Console.WriteLine(_server.connectionString);
                    break;
                case "dir": 
                    Console.WriteLine(_dir.Dirpath);
                    setter(args, () => new dir(constantinople.XML_FILEPATH, args[2]));
                    break;
                case "recursive":
                    Console.WriteLine(_dir.Recursive);
                    setter(args, () => new dir(constantinople.XML_FILEPATH, args[2].ToLowerInvariant()=="true" || args[2].ToLowerInvariant() == "t"));
                    break;
                case "load":
                    _dir.loadSql(args)
                        .ForEach(p=>Console.WriteLine(p));
                    break;
                case "add":
                    _dir.addSql(args).ForEach(p => Console.WriteLine(p));
                    break;
                case "leave":
                    _dir.leaveSql(args).ForEach(p => Console.WriteLine(p));
                    break;
                case "list":
                    _dir.listSql().ForEach(p => Console.WriteLine(p));
                    break;
                case "exists":
                    if(args.Count() > 1) {
                        Console.WriteLine("The filename {0} is {1} the current list.", args[1], _dir.exists(args[1]) ? "in" : "NOT in");
                    } else {
                        Console.WriteLine("The exists needs one filename argument.");
                    }
                    break;
                case "run":
                    runSql runsql = new runSql(_server.connectionString, _dir);
                    runsql.run().ForEach(p =>
                    {
                        Console.WriteLine(p);
                    });
                    break;
                case "change":
                    if (args.Length > 2)
                    {
                        editor ed = new editor(_dir.listSql(), _dir.Dirpath);
                        ed.change(args[1], args[2]).ForEach(p =>
                        {
                            Console.WriteLine(p);
                        });
                    }
                    break;
                case "show":
                    if (args.Length==2) Console.WriteLine(System.IO.File.ReadAllText(_dir.Dirpath + "\\" + args[1]));
                    break;
                case "addsql":
                    if (args.Length > 2)
                    {
                        editor ed = new editor(_dir.listSql(), _dir.Dirpath);
                        ed.add(args[1], args[2]).ForEach(p =>
                        {
                            Console.WriteLine(p);
                        });
                    }
                    break;
                case "edit":
                    if (args.Length > 1)
                    {
                        Process.Start(_dir.Dirpath + "\\" + args[1]);
                    }
                    break;
                case "delSql":
                    if(args.Length > 2) {
                        var delSp = new deleteSql(_server.connectionString, args[1]);
                        args.Skip(2).ToList().ForEach(p => {
                            Console.WriteLine(delSp.run(p));
                        });
                    } else {
                        Console.WriteLine("To delete an SQL entity you need the type of sql (ie function, procedure...) and then a list of names.");
                    }
                    break;
            }
        }

        static void setter(string[] args, Func<entityCore> f_entityCore)
        {
            if (args.Length > 2 && args[1] == "set")
            {
                var ec = f_entityCore();
                ec.save();
                Console.WriteLine("changed to {0}.", args[2]);
            }
        }
       

        static void runHelp()
        {
            Console.WriteLine("What you're beyond help!!!");
        }
    }
}
