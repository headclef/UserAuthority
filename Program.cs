using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string? folderPath;
        Console.Write("Please write the console path correctly without misinformation (use file extention too): ");
        folderPath = Console.ReadLine();
        FileInfo fileInfo = new FileInfo(folderPath);
        FileSecurity fileSecurity = fileInfo.GetAccessControl();
        AuthorizationRuleCollection authorizationRuleCollection = fileSecurity.GetAccessRules(true, true, typeof(NTAccount));
        foreach (FileSystemAccessRule rule in authorizationRuleCollection)
        {
            Console.WriteLine("Identity: {0}", rule.IdentityReference.Value);
            Console.WriteLine("Type: {0}", rule.AccessControlType);
            Console.WriteLine("Rights: {0}", rule.FileSystemRights);
            Console.WriteLine("Inherited ACE: {0}", rule.IsInherited);
            Console.WriteLine();
        }
    }
}