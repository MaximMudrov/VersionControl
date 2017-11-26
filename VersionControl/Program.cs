using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace VersionControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Your command:");
            string command = Console.ReadLine();
            string dirPath = "";
            List<Folders> Folders = new List<Folders>();
            FileStream file = new FileStream("test.txt", FileMode.OpenOrCreate);
            FileInfo Infile = new FileInfo("test.txt");
            Folders folder = new Folders();
            if (Infile.Exists)
            {
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    string name = reader.ReadLine();
                    Folders nfolder = new Folders();
                    nfolder.Name = name;
                    nfolder.Initialized = true;
                    Folders.Add(nfolder);
                }
                reader.Close();
            }
            string[] arg = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            while (arg[0] != "exit")
            {
                switch (arg[0])
                {
                    case "init":
                        {
                            Boolean tr = false;
                            dirPath = arg[1];
                            FileInfo initfile = new FileInfo(dirPath + "test.txt");
                            List<Finfo> nfiles = new List<Finfo>();
                            if (Infile.Exists)
                            {
                                nfiles = Rit(dirPath + "test.txt");
                                tr = true;
                            }
                            Init(dirPath + "test.txt", dirPath);
                            Folders nfolder = new Folders();
                            nfolder.Name = dirPath;
                            nfolder.Initialized = true;
                            List<Finfo> files = new List<Finfo>();
                            files = Rit(dirPath + "test.txt");
                            if (tr)
                            {
                                for (int i = 0; i < files.Count; i++)
                                {
                                    int t = 0;
                                    for (int j = 0; j < nfiles.Count; j++)
                                    {
                                        if (files[i].Name == nfiles[j].Name)
                                            t = 1;
                                    }
                                    if (t == 0)
                                        files[i].Note = "New";
                                }
                            }
                            nfolder.Files = files;
                            Folders.Add(nfolder);
                            folder = nfolder;
                            Console.WriteLine("The folder is successfuly initialized!");
                            break;
                        }
                    case "status":
                        {
                            if (folder.Initialized == true)
                            {
                                for (int i = 0; i < folder.Files.Count; i++)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    FileInfo finf = new FileInfo(folder.Name + folder.Files[i].Name);
                                    if (finf.Exists)
                                    {
                                        if (Convert.ToString(finf.Length) != folder.Files[i].Size)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("File: {0}", folder.Files[i].Name);
                                            if (folder.Files[i].Note != "0")
                                                Console.WriteLine("<<-- {0}", folder.Files[i].Note);
                                            else
                                                Console.WriteLine();
                                            Console.WriteLine("Created: {0}", folder.Files[i].Crtime);
                                            Console.WriteLine("Modified: {0}", folder.Files[i].Modtime);
                                            Console.WriteLine("Size: {0} b", folder.Files[i].Size);
                                            Console.WriteLine("<<-- {0} b", Convert.ToString(finf.Length));
                                            Console.WriteLine();
                                        }
                                        else
                                        {
                                            if (folder.Files[i].Note != "0")
                                            {
                                                if (folder.Files[i].Note == "removed")
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("File: {0}", folder.Files[i].Name);
                                                Console.WriteLine("<<-- {0}", folder.Files[i].Note);
                                                Console.WriteLine("Created: {0}", folder.Files[i].Crtime);
                                                Console.WriteLine("Modified: {0}", folder.Files[i].Modtime);
                                                Console.WriteLine("Size: {0} b", folder.Files[i].Size);
                                                Console.WriteLine();
                                            }
                                            else
                                            {
                                                Console.WriteLine("File: {0}", folder.Files[i].Name);
                                                Console.WriteLine("Created: {0}", folder.Files[i].Crtime);
                                                Console.WriteLine("Modified: {0}", folder.Files[i].Modtime);
                                                Console.WriteLine("Size: {0} b", folder.Files[i].Size);
                                                Console.WriteLine();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        folder.Files[i].Note = "deleted";
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("File: {0}", folder.Files[i].Name);
                                        Console.WriteLine("<<-- {0}", folder.Files[i].Note);
                                        Console.WriteLine("Created: {0}", folder.Files[i].Crtime);
                                        Console.WriteLine("Modified: {0}", folder.Files[i].Modtime);
                                        Console.WriteLine("Size: {0} b", folder.Files[i].Size);
                                        Console.WriteLine();
                                    }
                                    Init(folder.Name + "test.txt", folder.Name);
                                    List<Finfo> nfiles = new List<Finfo>();
                                    nfiles = Rit(folder.Name + "test.txt");
                                    for (int j = 0; j < nfiles.Count; j++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        int t = 0;
                                        for (int n = 0; n < folder.Files.Count; n++)
                                        {
                                            if (folder.Files[n].Name == nfiles[j].Name)
                                                t = 1;
                                        }
                                        if (t == 0)
                                        {
                                            nfiles[j].Note = "New";
                                            folder.Files.Add(nfiles[j]);
                                        }
                                    }
                                }
                                Console.ResetColor();
                            }
                            else
                                Console.WriteLine("The folder isn't already initialized!");
                            break;
                        }
                    case "add":
                        {
                            if (folder.Initialized == true)
                            {
                                string name = arg[1];
                                FileInfo finf = new FileInfo(folder.Name + name);
                                if (finf.Exists)
                                {
                                    Finfo fin = new Finfo();
                                    fin.Name = finf.Name;
                                    fin.Crtime = Convert.ToString(finf.CreationTime);
                                    fin.Modtime = Convert.ToString(finf.LastWriteTime);
                                    fin.Size = Convert.ToString(finf.Length);
                                    fin.Note = "added";
                                    folder.Files.Add(fin);
                                }
                                else
                                    Console.WriteLine("The file isn't exist!");
                            }
                            else
                                Console.WriteLine("The folder isn't already initialized!");
                            break;
                        }
                    case "apply":
                        {
                            if (folder.Initialized == true)
                            {
                                FileStream tile = new FileStream(folder.Name + "test.txt", FileMode.Truncate);
                                StreamWriter twriter = new StreamWriter(tile);
                                twriter.WriteLine("Directory: {0}", folder.Name);
                                twriter.WriteLine();
                                for (int i = 0; i < folder.Files.Count; i++)
                                {
                                    if (folder.Files[i].Note != "deleted")
                                    {
                                        FileInfo finf = new FileInfo(folder.Name + folder.Files[i].Name);
                                        if (Convert.ToString(finf.Length) != folder.Files[i].Size)
                                            folder.Files[i].Size = Convert.ToString(finf.Length);
                                        if (folder.Files[i].Note == "removed")
                                            folder.Files[i].Note = "_removed";
                                        else
                                            folder.Files[i].Note = "0";
                                        twriter.WriteLine(folder.Files[i].Name);
                                        twriter.WriteLine(folder.Files[i].Crtime);
                                        twriter.WriteLine(folder.Files[i].Modtime);
                                        twriter.WriteLine(folder.Files[i].Size);
                                        twriter.WriteLine(folder.Files[i].Note);
                                        twriter.WriteLine();
                                    }
                                    else
                                        folder.Files.Remove(folder.Files[i]);
                                }
                                twriter.Close();
                                Console.WriteLine("Success!");
                            }
                            else
                                Console.WriteLine("The folder isn't already initialized!");
                            break;
                        }
                    case "remove":
                        {
                            if (folder.Initialized == true)
                            {
                                string name = arg[1];
                                foreach (Finfo i in folder.Files)
                                {
                                    if (name == i.Name)
                                        i.Note = "removed";
                                }
                            }
                            else
                                Console.WriteLine("The folder isn't already initialized!");
                            break;
                        }
                    case "listbranch":
                        {
                            int p = 0;
                            foreach (Folders i in Folders)
                            {
                                Console.Write("{0})", p + 1);
                                Console.WriteLine(i.Name);
                                p++;
                            }
                            break;
                        }
                    case "checkout":
                        {
                            int i = Convert.ToInt32(arg[1]);
                            if (i <= Folders.Count)
                            {
                                folder = Folders[i - 1];
                                Console.WriteLine("{0}", folder.Name);
                                List<Finfo> files = new List<Finfo>();
                                files = Rit(folder.Name + "test.txt");
                                folder.Files = files;
                            }
                            else
                                Console.WriteLine("Wrong folder's number!");
                            break;
                        }
                    default:
                        Console.WriteLine("Wrong command");
                        break;
                }
                Console.WriteLine("Your command:");
                command = Console.ReadLine();
                arg = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            FileStream tfile = new FileStream("test.txt", FileMode.Truncate);
            StreamWriter writer = new StreamWriter(tfile);
            foreach (Folders i in Folders)
            {
                writer.WriteLine(i.Name);
            }
            writer.Close();
        }

        private static void ProcessDirectory(string targetDirectory, StreamWriter writer)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, writer);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, writer);

        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path, StreamWriter writer)
        {
            FileInfo finf = new FileInfo(path);
            writer.WriteLine(finf.Name);
            writer.WriteLine(finf.CreationTime);
            writer.WriteLine(finf.LastWriteTime);
            writer.WriteLine(finf.Length);
            writer.WriteLine("0");
            writer.WriteLine();
        }

        public static void Init(string path, string dirPath)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine("Directory: {0}", dirPath);
            writer.WriteLine();
            ProcessDirectory(dirPath, writer);
            writer.Close();
        }

        public static List<Finfo> Rit(string path)
        {
            List<Finfo> files = new List<Finfo>();
            StreamReader reader = new StreamReader(path);
            reader.ReadLine();
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string name = reader.ReadLine();
                string crtime = reader.ReadLine();
                string modtime = reader.ReadLine();
                string size = reader.ReadLine();
                string note = reader.ReadLine();
                Finfo fin = new Finfo();
                fin.Name = name;
                fin.Crtime = crtime;
                fin.Modtime = modtime;
                fin.Size = size;
                fin.Note = "0";
                files.Add(fin);
                reader.ReadLine();
            }
            reader.Close();
            return files;
        }
    }
}