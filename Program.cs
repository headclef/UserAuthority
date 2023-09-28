using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Variables
        string? folderPath;

        // Input
        Console.Write("Please write the console path correctly without misinformation (use file extention too): ");
        folderPath = Console.ReadLine();

        // Output
        Console.WriteLine();
        Console.WriteLine(
            "Account\t\t\t" +
            "Rights\t\t" +
            "Can Read\t" +
            "Can Write\t" +
            "Access\t" +
            "Inherited ACE\t" +
            "Inheritance Flags\t" +
            "Propagation Flags");

        //Console.WriteLine();

        FindFolders(folderPath);
    }
    static void FindFolders(string? folderPath)
    {
        ICollection<string>? folders = Directory.GetDirectories(folderPath);                // Get folders
        if (folders != null)
        {
            foreach (string folder in folders)
            {
                Console.WriteLine(folder);                                                  // Output           - If true
                ICollection<string>? subFolders = Directory.GetDirectories(folder);         // Get subfolders
                if (subFolders != null)
                {
                    // Folder Access Control
                    FileInfo fileInfo = new FileInfo(folderPath);
                    FileSecurity fileSecurity = fileInfo.GetAccessControl();
                    AuthorizationRuleCollection authorizationRuleCollection = fileSecurity.GetAccessRules(true, true, typeof(NTAccount));

                    // Output
                    foreach (FileSystemAccessRule rule in authorizationRuleCollection)
                    {
                        if (rule.IdentityReference is NTAccount)
                        {
                            NTAccount account = rule.IdentityReference as NTAccount;
                            Console.WriteLine(
                                $"{account.Value}\t" +
                                $"{rule.FileSystemRights}\t" +
                                $"{(rule.FileSystemRights & FileSystemRights.Read) == FileSystemRights.Read}\t\t" +
                                $"{(rule.FileSystemRights & FileSystemRights.Write) == FileSystemRights.Write}\t\t" +
                                $"{rule.AccessControlType}\t" +
                                $"{rule.IsInherited}\t\t" +
                                $"{rule.InheritanceFlags}\t\t\t" +
                                $"{rule.PropagationFlags}");
                        }
                    }
                    Console.WriteLine();
                    FindFolders(folder);                                                    // Recursive
                }
                else
                {
                    Console.WriteLine("No folders found.");                                 // Output           - If false
                }
            }
            
        }
        else
        {
            Console.WriteLine("No folders found.");                                         // Output           - If false
        }
    }
}