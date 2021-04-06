using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace atbm
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        OracleConnection conn = DBUtils.GetDBConnection();
        
        private void btnlogin_Click(object sender, EventArgs e)
        {

            conn.Open();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            String username = usernamelogin.Text.ToLower();
            String password = passlogin.Text.ToLower();
            // Convert no ve chuoi thuong neu lo co ghi hoa thi van dang nhap duoc
            objCmd.CommandText = "c##administrator.SYS_KTraTaiKhoanAdmin";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("adUsername", username);
            objCmd.Parameters.Add("adPassword", password);
            objCmd.Parameters.Add("tmp", OracleDbType.Int32).Direction = ParameterDirection.Output;
            
            if (username == "c##administrator" && password == "addm")
            {
                admin f = new admin();
                f.Show();
                this.Hide();
            }
            else if (username == "" || password == "")
            {
                MessageBox.Show("Vui long nhap day du thong tin");
            }
            else
            {
                MessageBox.Show("Vui long kiem tra lai password va username");
            }
            objCmd.Connection.Close();
        }
    }
}
