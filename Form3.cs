using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Enrollment
{
          
    public partial class Form3 : Form
    {

        // Static variables to transfer vaules for first name and last name from one form to another
        public static string fname;
        public static string lname;
        //Database connection string
        string source = Value.source;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.ResetText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) && String.IsNullOrEmpty(textBox2.Text))
            {
                try
                {
                    DataClasses1DataContext db = new DataClasses1DataContext(source);
                    Student dbstudent = new Student();
                    // findout database entry for matching first name and last name
                    var reader = from a in db.Students where a.StudentID == Convert.ToInt32(comboBox1.Text) select a;


                    if (reader.Count() > 0)
                    {
                                        
                        
                        foreach (var z in reader)
                        {
                            Value.sname = z.FirstName;
                            Value.lname = z.LastName;
                            dbstudent.CreditHours = z.CreditHours;
                            dbstudent.Course1 = z.Course1;
                            dbstudent.Course2 = z.Course2;
                            dbstudent.Course3 = z.Course3;
                        }
                        Form2 newform = new Form2();
                        newform.Show();
                        // displaying appropriate message for available option to enroll
                        if (dbstudent.CreditHours == 9)
                            MessageBox.Show("Maximum credit for semester is reached", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (dbstudent.CreditHours == 6)
                            MessageBox.Show("You can enroll for one more class", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (dbstudent.CreditHours == 3)
                            MessageBox.Show("You can enroll for two more classes", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (dbstudent.CreditHours == 0)
                            MessageBox.Show("You have not enrolled in any class", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else 
            {
                
                //Setting up database connections


                try
                {
                    DataClasses1DataContext db = new DataClasses1DataContext(source);
                    Student dbstudent = new Student();
                    // findout database entry for matching first name and last name
                    var reader = from a in db.Students where a.FirstName == textBox1.Text && a.LastName == textBox2.Text select a;

                    if (reader.Count() > 0)
                    {
                        //fname = textBox1.Text;
                        //lname = textBox2.Text;
                        Value.sname = textBox1.Text;
                        Value.lname = textBox2.Text;
                        Form2 newform = new Form2();
                        newform.Show();
                        foreach (var z in reader)
                        {

                            dbstudent.CreditHours = z.CreditHours;
                            dbstudent.Course1 = z.Course1;
                            dbstudent.Course2 = z.Course2;
                            dbstudent.Course3 = z.Course3;
                        }
                        // displaying appropriate message for available option to enroll
                        if (dbstudent.CreditHours == 9)
                            MessageBox.Show("Maximum credit for semester is reached", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (dbstudent.CreditHours == 6)
                            MessageBox.Show("You can enroll for one more class", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (dbstudent.CreditHours == 3)
                            MessageBox.Show("You can enroll for two more classes", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (dbstudent.CreditHours == 0)
                            MessageBox.Show("You have not enrolled in any class", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        // message displayed if the information not in database
                        MessageBox.Show("Your information is not in system", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Value.sname = textBox1.Text;
                        Value.lname = textBox2.Text;
                        Form1 newform = new Form1();
                        newform.Show();

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet8.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter1.Fill(this.database1DataSet8.Student);
            // TODO: This line of code loads data into the 'database1DataSet7.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.database1DataSet7.Student);

        }
    }
}
