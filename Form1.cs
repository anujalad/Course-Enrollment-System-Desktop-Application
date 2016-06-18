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
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data.Linq;

namespace Enrollment
{
    public partial class Form1 : Form
    {
        // static public variable to pass vaules from one form to other
        public static string fname1;
        public static string lname1;
        //Database connection string
        string source = Value.source;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Value.sname;
            textBox2.Text = Value.lname;
            //MessageBox.Show(Value.source,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // this is reset button it will reload the form 
                //textBox1.Clear();
                //textBox2.Clear();
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();

            }
            catch (Exception ex)
            {
                // catching exceptions
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // conditions to check no input for First name
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("First Name can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // conditions to check no input for last name
                else if (String.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Last Name can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                

                // Object created from Data conext class which was created using Database tables aviable in dbml file
                DataClasses1DataContext db = new DataClasses1DataContext(source);
                // a object created to hold the values for each column in table
                Student dbstudent = new Student();
                // assigning vaules from text box to columns first name
                dbstudent.FirstName = textBox1.Text;
                fname1 = textBox1.Text;
                // assigning vaules from text box to columns last name
                dbstudent.LastName = textBox2.Text;
                lname1 = textBox2.Text;
                // condition to check if courses not selected
                if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
                {
                    MessageBox.Show("Please Select at least one class to enroll", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (comboBox1.SelectedIndex != 0)
                    {
                        // For course not found exception for first courese
                        var reader = from a in db.Courses where a.CourseName == comboBox1.Text select a;
                        if (reader.Count() <= 0)
                        {
                            
                            throw (new CourseNotValidException("Course \"" + comboBox1.Text + "\" is not an available course"));
                        }
                        
                    }
                    if (comboBox2.SelectedIndex != 0)
                    {
                        // For course not found exception for second courese
                        var reader = from a in db.Courses where a.CourseName == comboBox2.Text select a;
                        if (reader.Count() <= 0)
                        {
                            
                            throw (new CourseNotValidException("Course \"" + comboBox2.Text + "\" is not an available course"));
                            
                        }

                    }
                    if (comboBox3.SelectedIndex != 0)
                    {
                        // For course not found exception for third courese
                        var reader = from a in db.Courses where a.CourseName == comboBox3.Text select a;
                        if (reader.Count() <= 0)
                        {
                            
                            throw (new CourseNotValidException("Course \"" + comboBox3.Text + "\" is not an available course"));
                            
                        }

                    }
                        // conditions to check if only one course selected
                        if (comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
                        {
                            // assigning course ID to course column in database
                            dbstudent.Course1 = db.Courses.First(a => a.CourseName == comboBox1.Text).CourseID;
                            dbstudent.Course2 = null;
                            dbstudent.Course3 = null;
                        }
                        // conditions to check if only two course selected
                        if (comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex != 0 && comboBox3.SelectedIndex == 0)
                        {
                            // assigning course ID to course column in database
                            dbstudent.Course1 = db.Courses.First(a => a.CourseName == comboBox1.Text).CourseID;
                            dbstudent.Course2 = db.Courses.First(a => a.CourseName == comboBox2.Text).CourseID;
                            //condition to check if courses are from undergard or grad only else it is nullfied
                            if ((dbstudent.Course1 < 1000 && dbstudent.Course2 > 1000) || (dbstudent.Course1 > 1000 && dbstudent.Course2 < 1000))
                            {
                                MessageBox.Show("You are allowed to take only Under Graduate or Graduate course, mix cources not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dbstudent.Course1 = null;
                                dbstudent.Course2 = null;
                            }
                            else
                            {
                                dbstudent.Course3 = null;
                            }
                        }
                        // check if all courses selected
                        if (comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex != 0 && comboBox3.SelectedIndex != 0)
                        {
                            dbstudent.Course1 = db.Courses.First(a => a.CourseName == comboBox1.Text).CourseID;
                            dbstudent.Course2 = db.Courses.First(a => a.CourseName == comboBox2.Text).CourseID;
                            dbstudent.Course3 = db.Courses.First(a => a.CourseName == comboBox3.Text).CourseID;
                            //condition to check if courses are from undergard or grad only else it is nullfied 
                            if (((dbstudent.Course1 < 1000 && dbstudent.Course2 > 1000) || (dbstudent.Course1 > 1000 && dbstudent.Course2 < 1000)) || ((dbstudent.Course2 < 1000 && dbstudent.Course3 > 1000) || (dbstudent.Course2 > 1000 && dbstudent.Course3 < 1000)))
                            {
                                MessageBox.Show("You are allowed to take only Under Graduate or Graduate course, mix cources not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dbstudent.Course1 = null;
                                dbstudent.Course2 = null;
                                dbstudent.Course3 = null;
                            }


                        }
                        //condition to check if courses are from undergard or grad only else it is nullfied
                        if (comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex != 0)
                        {
                            dbstudent.Course1 = db.Courses.First(a => a.CourseName == comboBox1.Text).CourseID;
                            dbstudent.Course2 = db.Courses.First(a => a.CourseName == comboBox3.Text).CourseID;
                            if ((dbstudent.Course1 < 1000 && dbstudent.Course2 > 1000) || (dbstudent.Course1 > 1000 && dbstudent.Course2 < 1000))
                            {
                                MessageBox.Show("You are allowed to take only Under Graduate or Graduate course, mix cources not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dbstudent.Course1 = null;
                                dbstudent.Course2 = null;
                            }
                            else
                            {
                                dbstudent.Course3 = null;
                            }

                        }
                        //condition to check if courses are from undergard or grad only else it is nullfied
                        if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex != 0 && comboBox3.SelectedIndex != 0)
                        {
                            dbstudent.Course1 = db.Courses.First(a => a.CourseName == comboBox2.Text).CourseID;
                            dbstudent.Course2 = db.Courses.First(a => a.CourseName == comboBox3.Text).CourseID;
                            if ((dbstudent.Course1 < 1000 && dbstudent.Course2 > 1000) || (dbstudent.Course1 > 1000 && dbstudent.Course2 < 1000))
                            {
                                MessageBox.Show("You are allowed to take only Under Graduate or Graduate course, mix cources not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dbstudent.Course1 = null;
                                dbstudent.Course2 = null;
                            }
                            else
                            {
                                dbstudent.Course3 = null;
                            }
                        }
                        //condition to check if courses selected and assigning it to appropriate column in database
                        if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex != 0)
                        {
                            dbstudent.Course1 = db.Courses.First(a => a.CourseName == comboBox3.Text).CourseID;
                            dbstudent.Course2 = null;
                            dbstudent.Course3 = null;
                        }
                        //condition to check if courses selected and assigning it to appropriate column in database
                        if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex != 0 && comboBox3.SelectedIndex == 0)
                        {
                            dbstudent.Course1 = db.Courses.First(a => a.CourseName == comboBox2.Text).CourseID;
                            dbstudent.Course2 = null;
                            dbstudent.Course3 = null;
                        }
                        //condition to check if courses selected are same
                        if (dbstudent.Course2 != null && dbstudent.Course3 != null)
                        {
                            if (dbstudent.Course1 == dbstudent.Course2 || dbstudent.Course2 == dbstudent.Course3 || dbstudent.Course1 == dbstudent.Course3)
                            {
                                MessageBox.Show("You are not allowed to enroll for same course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dbstudent.Course1 = null;
                                dbstudent.Course2 = null;
                                dbstudent.Course3 = null;
                            }
                        }
                        //calculating credit hours 
                        dbstudent.CreditHours = 0;
                        if (dbstudent.Course1 != null)
                            dbstudent.CreditHours = dbstudent.CreditHours + db.Courses.First(a => a.CourseID == dbstudent.Course1).CourseCredit;
                        if (dbstudent.Course2 != null)
                            dbstudent.CreditHours = dbstudent.CreditHours + db.Courses.First(a => a.CourseID == dbstudent.Course2).CourseCredit;
                        if (dbstudent.Course3 != null)
                            dbstudent.CreditHours = dbstudent.CreditHours + db.Courses.First(a => a.CourseID == dbstudent.Course3).CourseCredit;
                        if (dbstudent.Course1 != null || dbstudent.Course2 != null || dbstudent.Course3 != null)
                        {//exception handling for sql
                            try
                            {
                                // inserting the information collected in database
                                db.Students.InsertOnSubmit(dbstudent);

                                //commiting the changes
                                db.SubmitChanges(ConflictMode.ContinueOnConflict);
                            }
                            catch (ChangeConflictException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            //new form will be opened for details
                            Form2 newform = new Form2();
                            newform.Show();
                        }
                        else
                        {
                            // resetting all the inoyts
                            MessageBox.Show("Try again choose All courses from only Under Graduate or Graduate ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            comboBox1.ResetText();
                            comboBox2.ResetText();
                            comboBox3.ResetText();
                            
                        }
                        
                    }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet3.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter2.Fill(this.database1DataSet3.Courses);
            // TODO: This line of code loads data into the 'database1DataSet2.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter1.Fill(this.database1DataSet2.Courses);
            // TODO: This line of code loads data into the 'database1DataSet1.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.database1DataSet1.Courses);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // this code will ensure that special charecter are not added to user name
            Regex regexItem = new Regex("[^a-z0-9A-Z]");
            if (regexItem.IsMatch(textBox1.Text))
            {
                // this will prompt message box which if special charecters added to user name
                MessageBox.Show("This doesn't except special charectors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // this will clear text box for new entry
                textBox1.Clear();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // this code will ensure that special charecter are not added to user name
            Regex regexItem = new Regex("[^a-z0-9A-Z]");
            if (regexItem.IsMatch(textBox2.Text))
            {
                // this will prompt message box which if special charecters added to user name
                MessageBox.Show("This doesn't except special charectors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // this will clear text box for new entry
                textBox2.Clear();
            }

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            
        }


    }
}
