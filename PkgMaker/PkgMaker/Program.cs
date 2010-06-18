using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PkgMaker.Model;

namespace PkgMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Package pkg = Package.FromFile("Foo.pkg.xml");
        }
    }
}
