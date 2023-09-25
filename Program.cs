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

        // Folder Access Control
        FileInfo fileInfo = new FileInfo(folderPath);
        FileSecurity fileSecurity = fileInfo.GetAccessControl();
        AuthorizationRuleCollection authorizationRuleCollection = fileSecurity.GetAccessRules(true, true, typeof(NTAccount));

        // Output
        Console.WriteLine();
        Console.WriteLine(
            "Account\t\t\t" +
            "Rights\t\t" +
            "Access\t" +
            "Inherited ACE\t" +
            "Inheritance Flags\t" +
            "Propagation Flags");
        foreach (FileSystemAccessRule rule in authorizationRuleCollection)
        {
            if (rule.IdentityReference is NTAccount)
            {
                NTAccount account = rule.IdentityReference as NTAccount;
                Console.WriteLine(
                    $"{account.Value}\t" +
                    $"{rule.FileSystemRights}\t" +
                    $"{rule.AccessControlType}\t" +
                    $"{rule.IsInherited}\t\t" +
                    $"{rule.InheritanceFlags}\t\t\t" +
                    $"{rule.PropagationFlags}");
            }
        }
        Console.WriteLine();
    }
}