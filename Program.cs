using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Enrollment
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form3());

            
        }
 
    }
    static class Value
    {
        public static string sname;
        public static string lname;
        //public static string pwd = 
        public static string source = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\HARSHAL\Desktop\Enrollment\Enrollment\Database1.mdf;Integrated Security=True";
        
    }

    class CourseNotValidException : Exception
    { 
    public CourseNotValidException()
    {
    }

    public CourseNotValidException(string message) : base(message)
    {
    }

    public CourseNotValidException(string message, Exception inner) : base(message, inner)
    {
    }

    }
}
