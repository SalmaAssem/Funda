using System;
using Automation.Utilities;
using OpenQA.Selenium;
using NUnit.Framework;


namespace Funda_Assignment
{

    public  class ColorObject
    {
        public  int r;
        public  int g;
        public  int b;
        public  string hex;

        public ColorObject(int _r, int _g, int _b)
        {
            r = _r;
            g = _g;
            b = _b;
        }
    }

    
}
