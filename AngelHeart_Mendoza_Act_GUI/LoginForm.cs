using MySql.Data.MySqlClient;
using System.Data;

namespace AngelHeart_Mendoza_Act_GUI
{
    public partial class LoginForm : Form
    {
        private string validUsername = "Angel";
        private string validPassword = "123";
        private int loginAttempts = 0;



        private const string serverName = "localhost";
        private const string dataBaseName = "StudentRecordDB";
        private const string uid = "root";
        private const string password = "lis@0327";

        //set value of index to 0, needed when accessing data
        private int index = 0;

        //initialize class for DataTable
        //DataTable -used to store data in a tabular format,
        private DataTable dt = new DataTable();


        //variables where value of username and password from database will be stored
        public string uname;
        public string pword;

        //initialize properties

        //Represents a connection to database
        public MySqlConnection con { get; set; }
        public MySqlCommand cmd { get; set; }
        public MySqlDataReader dr { get; set; }

        public int id { get; set; }
        public string username { get; set; }
        public string passsword { get; set; }

        //public Form1() { }
        public LoginForm(string username, string password)
        {
            this.username = username;
            this.passsword = password;
        }


        public LoginForm()
        {
            con = new MySqlConnection($"Server = {serverName} ; Database = " +
                $"{dataBaseName}; Uid = {uid}; Pwd = {password};");
            cmd = new MySqlCommand();
            cmd.Connection = this.con;
            InitializeComponent();
            if (!Connect())
                MessageBox.Show("Please Configure Your Connection");
        }

        public bool Connect()
        {
            if (this.con.State == ConnectionState.Closed || this.con.State == ConnectionState.Broken)
            {
                try
                {
                    this.con.Open();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        //method to connect to disconnect
        public void Disconnect()
        {
            if (this.con.State == ConnectionState.Open)
            {
                this.con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                load_users();
                //this.fill_TextBoxes(index);
            }
            else
            {
                Application.Exit();
            }
        }

        private void load_users()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from USERLOGINTB";
            dr = cmd.ExecuteReader();
            dt.Clear();
            dt.Load(dr);
        }

        private void usercred(int i)
        {
            uname = dt.Rows[i][1].ToString();
            pword = dt.Rows[i][2].ToString();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            loginAttempts++;
            if (loginAttempts > 5)
            {
                MessageBox.Show("Too many failed login attempts. Please reset your password using the link below.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Password reset link (for display only)
                MessageBox.Show("Reset Password Link: www.example.com/resetpassword", "Reset Password Link", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit(); // Or disable login button and show reset link
                return;
            }

            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (username == validUsername && password == validPassword)
            {
                // Successful login
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StudentForm studentForm = new StudentForm();
                this.Hide();
                studentForm.Show();
            }
            else
            {
                // Failed login
                string errorMessage = "";
                if (username != validUsername && password != validPassword)
                {
                    errorMessage = "Incorrect Username and Password.";
                }
                else if (username != validUsername)
                {
                    errorMessage = "Incorrect Username.";
                }
                else if (password != validPassword)
                {
                    errorMessage = "Incorrect Password.";
                }
                MessageBox.Show(errorMessage + $"\nAttempt: {loginAttempts} of 5", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear password field after failed attempt
                PasswordTextBox.Clear();
                PasswordTextBox.Focus(); // Set focus back to password textbox for next attempt
            }
        }
    }
}
