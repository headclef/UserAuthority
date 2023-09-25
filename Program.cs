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
        foreach (FileSystemAccessRule rule in authorizationRuleCollection)
        {
            if (rule.IdentityReference is NTAccount)
            {
                NTAccount account = rule.IdentityReference as NTAccount;
                Console.WriteLine("Account: {0}", account.Value);
                Console.WriteLine("Rights: {0}", rule.FileSystemRights);
                Console.WriteLine("Type: {0}", rule.AccessControlType);
                Console.WriteLine("Inherited ACE: {0}", rule.IsInherited);
                Console.WriteLine("Inheritance Flags: {0}", rule.InheritanceFlags);
                Console.WriteLine("Propagation Flags: {0}", rule.PropagationFlags);
                Console.WriteLine();
            }
            //Console.WriteLine();
            //Console.WriteLine("Identity: {0}", rule.IdentityReference.Value);
            //Console.WriteLine("Rights: {0}", rule.FileSystemRights);
            //Console.WriteLine("Inherited ACE: {0}", rule.IsInherited);
            //Console.WriteLine(": {0}", rule);
        }
    }
}