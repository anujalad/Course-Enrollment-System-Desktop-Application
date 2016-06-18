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
    public partial class Form2 : Form
    {
        // Database connection string
        string source = Value.source;
        //Static parameter to receive values for first name and last name from other forms
        public string frname;
        public string lsname;
        public Form2()
        {
            InitializeComponent();
            try
            {
            //MessageBox.Show( Form1.fname,Form1.lname);
            //selecting input from Form3 or Form1 and assigning vaules to static parameters
                frname = Value.sname;
                lsname = Value.lname;
            
                // creation of Datacontect object
                DataClasses1DataContext db = new DataClasses1DataContext(source);
                //student object create
                Student dbstudent = new Student();
                //query to check if User is alredy exists in database
             var reader = from a in db.Students where a.FirstName == this.frname && a.LastName == this.lsname select a;
                // assigning vaules from each of database column to print
                foreach (var z in reader)
                {
                    label12.Text = z.StudentID.ToString();
                    label9.Text = z.FirstName.ToString();
                    label8.Text = z.LastName.ToString();
                    label7.Text = z.CreditHours.ToString();
                    label6.Text = z.Course1.ToString();
                    // conditions to enable Enroll and Drop buttons
                    if (z.Course1.ToString() != "")
                        button2.Visible = true;
                    else
                    {
                        button7.Visible = true;
                        comboBox1.Visible = true;
                    }
                    label10.Text = z.Course2.ToString();
                    // conditions to enable Enroll and Drop buttons
                    if (z.Course2.ToString() != "")
                        button3.Visible = true;
                    else
                    {
                        button6.Visible = true;
                        comboBox2.Visible = true;
                    }
                    label11.Text = z.Course3.ToString();
                    // conditions to enable Enroll and Drop buttons
                    if (z.Course3.ToString() != "")
                        button4.Visible = true;
                    else
                    {
                        button5.Visible = true;
                        comboBox3.Visible = true;
                    }
                }
                }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2.ActiveForm.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2.ActiveForm.Close();
        }
        // Drop couse button
        private void button2_Click(object sender, EventArgs e)
        {
            // Datacontext object created
            DataClasses1DataContext db = new DataClasses1DataContext(source);
            // Student table class table obkect created to hold vaules
            Student dbstudent = new Student();
            //query to run on database to find out if entry exists for entered data
            var reader = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            foreach (var z in reader)
            {
                try
                {
                    //Filling up the empty coulmn
                z.Course1 = z.Course2;
                z.Course2 = z.Course3;
                z.Course3 = null;
                z.CreditHours = z.CreditHours - db.Courses.First(a => a.CourseName == comboBox3.Text).CourseCredit;
                db.SubmitChanges();
                }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
            try
            {// disbaling all other buttons and enableing only that are required
            var updated = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            foreach (var z in updated)
            {
                label12.Text = z.StudentID.ToString();
                label9.Text = z.FirstName.ToString();
                label8.Text = z.LastName.ToString();
                label7.Text = z.CreditHours.ToString();
                label6.Text = z.Course1.ToString();
                // if course one has vaule then enable drop button
                if (z.Course1.ToString() != "")
                    button2.Visible = true;
                else
                {
                    button7.Visible = true;
                    comboBox1.Visible = true;
                }
                label10.Text = z.Course2.ToString();
                // if course two has vaule then enable drop button
                if (z.Course2.ToString() != "")
                    button3.Visible = true;
                else
                {
                    button6.Visible = true;
                    comboBox2.Visible = true;
                }
                label11.Text = z.Course3.ToString();
                // if course three has vaule then enable drop button
                if (z.Course3.ToString() != "")
                    button4.Visible = true;
                else
                {
                    button5.Visible = true;
                    comboBox3.Visible = true;
                }
            }
              }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //datacontext object created
            DataClasses1DataContext db = new DataClasses1DataContext(source);
            Student dbstudent = new Student();
            //query to find database entry for matching firstname and lastname
            var reader = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            foreach (var z in reader)
            {
                try
                {
                z.Course2 = z.Course3;
                z.Course3 = null;
                z.CreditHours = z.CreditHours - db.Courses.First(a => a.CourseName == comboBox3.Text).CourseCredit;
                db.SubmitChanges();
                    }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
            try
            {
            var updated = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            foreach (var z in updated)
            {
                label12.Text = z.StudentID.ToString();
                label9.Text = z.FirstName.ToString();
                label8.Text = z.LastName.ToString();
                label7.Text = z.CreditHours.ToString();
                label6.Text = z.Course1.ToString();
                // if course one has vaule then enable drop button
                if (z.Course1.ToString() != "")
                    button2.Visible = true;
                else
                {
                    button7.Visible = true;
                    comboBox1.Visible = true;
                }
                // if course two has vaule then enable drop button
                label10.Text = z.Course2.ToString();
                if (z.Course2.ToString() != "")
                    button3.Visible = true;
                else
                {
                    button6.Visible = true;
                    comboBox2.Visible = true;
                }
                // if course three has vaule then enable drop button
                label11.Text = z.Course3.ToString();
                if (z.Course3.ToString() != "")
                    button4.Visible = true;
                else
                {
                    button5.Visible = true;
                    comboBox3.Visible = true;
                }
            }
                }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
                       
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //DB class created
            DataClasses1DataContext db = new DataClasses1DataContext(source);
            Student dbstudent = new Student();
            var reader = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            
            foreach (var z in reader)
            {
                try
                {
                    if (comboBox1.SelectedIndex != 0)
                    {
                        // For course not found exception for third courese
                        var rd = from a in db.Courses where a.CourseName == comboBox1.Text select a;
                        if (rd.Count() <= 0)
                        {
                            throw (new CourseNotValidException("Course \"" + comboBox1.Text + "\" is not an available course"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
                try
                {
                // enroll button is enabled and course is updated in database for user
                    z.Course1 = db.Courses.First(a => a.CourseName == comboBox1.Text).CourseID;
                    z.CreditHours = z.CreditHours + db.Courses.First(a => a.CourseName == comboBox1.Text).CourseCredit;
                    db.SubmitChanges();
                    }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
                
            }
            try
            {
            var updated = from a in db.Students where a.FirstName == lsname && a.LastName == lsname select a;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            foreach (var z in updated)
            {
                label12.Text = z.StudentID.ToString();
                label9.Text = z.FirstName.ToString();
                label8.Text = z.LastName.ToString();
                label7.Text = z.CreditHours.ToString();
                label6.Text = z.Course1.ToString();
                // if course one has vaule then enable drop button
                if (z.Course1.ToString() != "")
                    button2.Visible = true;
                else
                {
                    button7.Visible = true;
                    comboBox1.Visible = true;
                }
                label10.Text = z.Course2.ToString();
                // if course two has vaule then enable drop button
                if (z.Course2.ToString() != "")
                    button3.Visible = true;
                else
                {
                    button6.Visible = true;
                    comboBox2.Visible = true;
                }
                label11.Text = z.Course3.ToString();
                // if course three has vaule then enable drop button
                if (z.Course3.ToString() != "")
                    button4.Visible = true;
                else
                {
                    button5.Visible = true;
                    comboBox3.Visible = true;
                }

            }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //DB connection settings
            DataClasses1DataContext db = new DataClasses1DataContext(source);
            Student dbstudent = new Student();
            var reader = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            foreach (var z in reader)
            {
                try
                {
                    if (comboBox2.SelectedIndex != 0)
                    {
                        // For course not found exception for third courese
                        var rd = from a in db.Courses where a.CourseName == comboBox2.Text select a;
                        if (rd.Count() <= 0)
                        {
                            throw (new CourseNotValidException("Course \"" + comboBox2.Text + "\" is not an available course"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
                dbstudent.Course2 = db.Courses.First(a => a.CourseName == comboBox2.Text).CourseID;
                // check if only undergard or grad courses are selexted
                if ((z.Course1 < 1000 && z.Course2 > 1000) || (z.Course1 > 1000 && z.Course2 < 1000)) 
                {
                    MessageBox.Show("You are allowed to take only Under Graduate or Graduate course, mix cources not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Changes not updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // checking if same courses are selected again
                if (z.Course1 == z.Course2 )
                {
                    MessageBox.Show("You are not allowed to enroll for same course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox3.ResetText();
                }
                else
                {//updating the database
                    try
                    {
                    z.Course2 = db.Courses.First(a => a.CourseName == comboBox2.Text).CourseID;
                    z.CreditHours = z.CreditHours + db.Courses.First(a => a.CourseName == comboBox2.Text).CourseCredit;
                    db.SubmitChanges();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            try
            {
                //display database conctent
            var updated = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            foreach (var z in updated)
            {
                label12.Text = z.StudentID.ToString();
                label9.Text = z.FirstName.ToString();
                label8.Text = z.LastName.ToString();
                label7.Text = z.CreditHours.ToString();
                label6.Text = z.Course1.ToString();
                // if course one has vaule then enable drop button
                if (z.Course1.ToString() != "")
                    button2.Visible = true;
                else
                {
                    button7.Visible = true;
                    comboBox1.Visible = true;
                }
                label10.Text = z.Course2.ToString();
                // if course two has vaule then enable drop button
                if (z.Course2.ToString() != "")
                    button3.Visible = true;
                else
                {
                    button6.Visible = true;
                    comboBox2.Visible = true;
                }
                label11.Text = z.Course3.ToString();
                // if course three has vaule then enable drop button
                if (z.Course3.ToString() != "")
                    button4.Visible = true;
                else
                {
                    button5.Visible = true;
                    comboBox3.Visible = true;
                }

            }
                }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //DB connections open
            DataClasses1DataContext db = new DataClasses1DataContext(source);
            Student dbstudent = new Student();
            var reader = from a in db.Students where a.FirstName ==  frname && a.LastName == lsname select a;

            foreach (var z in reader)
            {
                try
                {
                    // updating database after course drop
                z.Course3 = null;
                z.CreditHours = z.CreditHours - db.Courses.First(a => a.CourseName == comboBox3.Text).CourseCredit;
                db.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            try
            {
                //displaying information after the couser is dropped
                var updated = from a in db.Students where a.FirstName == this.frname && a.LastName == this.lsname select a;
            
                            button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                foreach (var z in updated)
                {
                    label12.Text = z.StudentID.ToString();
                    label9.Text = z.FirstName.ToString();
                    label8.Text = z.LastName.ToString();
                    label7.Text = z.CreditHours.ToString();
                    label6.Text = z.Course1.ToString();
                    // if course one has vaule then enable drop button
                    if (z.Course1.ToString() != "")
                        button2.Visible = true;
                    else
                    {
                        button7.Visible = true;
                        comboBox1.Visible = true;
                    }
                    label10.Text = z.Course2.ToString();
                    // if course two has vaule then enable drop button
                    if (z.Course2.ToString() != "")
                        button3.Visible = true;
                    else
                    {
                        button6.Visible = true;
                        comboBox2.Visible = true;
                    }
                    label11.Text = z.Course3.ToString();
                    // if course three has vaule then enable drop button
                    if (z.Course3.ToString() != "")
                        button4.Visible = true;
                    else
                    {
                        button5.Visible = true;
                        comboBox3.Visible = true;
                    }
                }
            }
                
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
                   

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet6.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter2.Fill(this.database1DataSet6.Courses);
            // TODO: This line of code loads data into the 'database1DataSet5.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter1.Fill(this.database1DataSet5.Courses);
            // TODO: This line of code loads data into the 'database1DataSet4.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.database1DataSet4.Courses);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
          // DB connection setup
            DataClasses1DataContext db = new DataClasses1DataContext(source);
            Student dbstudent = new Student();
            var reader = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            foreach (var z in reader)
            {
                try
                {
                    if (comboBox3.SelectedIndex != 0)
                    {
                        // For course not found exception for third courese
                        var rd = from a in db.Courses where a.CourseName == comboBox3.Text select a;
                        if (rd.Count() <= 0)
                        {
                            throw (new CourseNotValidException("Course \"" + comboBox3.Text + "\" is not an available course"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
                
              
                // checking if all cousres are from Undergrad or grad
                dbstudent.Course3 = db.Courses.First(a => a.CourseName == comboBox3.Text).CourseID;
                if (((z.Course1 < 1000 && z.Course2 > 1000) || (z.Course1 > 1000 && z.Course2 < 1000)) || ((z.Course2 < 1000 && dbstudent.Course3 > 1000) || (z.Course2 > 1000 && z.Course3 < 1000)))
                {
                    MessageBox.Show("You are allowed to take only Under Graduate or Graduate course, mix cources not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Changes not updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // checking if no two cousres are same
                if (z.Course1 == z.Course2 || z.Course2 == z.Course3 || z.Course1 == z.Course3)
                {
                    MessageBox.Show("You are not allowed to enroll for same course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox3.ResetText();
                }
                else
                {
                    try
                    {
                        //updating the selected course information in database.
                        z.Course3 = dbstudent.Course3;
                        z.CreditHours = z.CreditHours + db.Courses.First(a => a.CourseName == comboBox3.Text).CourseCredit;
                        db.SubmitChanges();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            try
            {
                // display updated information
            var updated = from a in db.Students where a.FirstName == frname && a.LastName == lsname select a;
            
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            foreach (var z in updated)
            {
                label12.Text = z.StudentID.ToString();
                label9.Text = z.FirstName.ToString();
                label8.Text = z.LastName.ToString();
                label7.Text = z.CreditHours.ToString();
                label6.Text = z.Course1.ToString();
                // if course one has vaule then enable drop button
                if (z.Course1.ToString() != "")
                    button2.Visible = true;
                else
                {
                    button7.Visible = true;
                    comboBox1.Visible = true;
                }
                label10.Text = z.Course2.ToString();
                // if course two has vaule then enable drop button
                if (z.Course2.ToString() != "")
                    button3.Visible = true;
                else
                {
                    button6.Visible = true;
                    comboBox2.Visible = true;
                }
                label11.Text = z.Course3.ToString();
                // if course three has vaule then enable drop button
                if (z.Course3.ToString() != "")
                    button4.Visible = true;
                else
                {
                    button5.Visible = true;
                    comboBox3.Visible = true;
                }

            }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
