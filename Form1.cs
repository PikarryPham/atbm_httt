using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace atbm
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        OracleConnection conn = DBUtils.GetDBConnection();
        DataSet ds = new DataSet();
        //
        
        private void userlist_search_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                DataTable dt = new DataTable();
                String search = timkiemuserroletb.Text.ToUpper();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Xem_ds_user", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("USER_USERNAME", OracleDbType.Varchar2).Value = search;
                objCmd.Parameters.Add("p_ketqua", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(objCmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Tim kiem thanh cong!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally {
                conn.Close();
                //conn.Dispose();
            }
            Console.Read();
        }

        
        private void caprole_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            try
            { 
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_CapRolechoUser", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter Ten_userParam = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                Ten_userParam.Value = grantrole_username.Text;
                objCmd.Parameters.Add(Ten_userParam);
                OracleParameter Ten_roleParam = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                Ten_roleParam.Value = grantrole_rolename.Text;
                objCmd.Parameters.Add(Ten_roleParam);
                OracleParameter mk_roleParam = new OracleParameter("@mk_role", OracleDbType.Varchar2);
                mk_roleParam.Value = grantrole_rolepassif.Text;
                objCmd.Parameters.Add(mk_roleParam);
                if (quanly_grantoption.Checked)
                {
                    OracleParameter grant_optionParam = new OracleParameter("@grant_option", OracleDbType.Int32);
                    grant_optionParam.Value = 1;
                    objCmd.Parameters.Add(grant_optionParam);
                }
                MessageBox.Show("Add success");
            } catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }
            Console.Read();
        }


        //Quan ly role
        
        private void xoarolebtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                string tenrole = quanly_role_tenrole.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Xoa_Role", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter quanlyRole_tenrole = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                quanlyRole_tenrole.Value = tenrole;
                objCmd.Parameters.Add(quanlyRole_tenrole);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 50));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror1);
            }
            catch(Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void taorolebtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                string tenrole = quanly_role_tenrole.Text.ToLower();
                string passrole = quanly_tenpass_role.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Tao_Role", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter quanlyRole_tenrole = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                quanlyRole_tenrole.Value = tenrole;
                objCmd.Parameters.Add(quanlyRole_tenrole);
                OracleParameter quanlyRole_passrole = new OracleParameter("@pass_role", OracleDbType.Varchar2);
                quanlyRole_passrole.Value = passrole;
                objCmd.Parameters.Add(quanlyRole_passrole);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 200));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror1);
            }
            catch(Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void capnhatpassrolebtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                string tenrole = quanly_role_tenrole.Text.ToLower();
                string passrole = quanly_tenpass_role.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_CapnhatRole", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter quanlyRole_tenrole = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                quanlyRole_tenrole.Value = tenrole;
                objCmd.Parameters.Add(quanlyRole_tenrole);
                OracleParameter quanlyRole_passrole = new OracleParameter("@pass_role", OracleDbType.Varchar2);
                quanlyRole_passrole.Value = passrole;
                objCmd.Parameters.Add(quanlyRole_passrole);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror2);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        //Quan ly User
        private void createuserbtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                //khai bao bien
                string tenuser = quanly_user_name.Text.ToLower();
                string passuser = quanly_user_password.Text.ToLower();
                int quotanum = Int32.Parse(quanly_user_quota.Text);
                string tablespace = quanly_user_tablespace.Text.ToLower();
                //
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Tao_User", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                //ghep bien
                OracleParameter quanlyRole_tenuser = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                quanlyRole_tenuser.Value = tenuser;
                objCmd.Parameters.Add(quanlyRole_tenuser);
                OracleParameter quanlyRole_passuser = new OracleParameter("@MK", OracleDbType.Varchar2);
                quanlyRole_passuser.Value = passuser;
                objCmd.Parameters.Add(quanlyRole_passuser);
                OracleParameter quanlyRole_quota = new OracleParameter("@User_quota", OracleDbType.Int32);
                quanlyRole_quota.Value = quotanum;
                objCmd.Parameters.Add(quanlyRole_quota);
                OracleParameter quanlyRole_tablespace = new OracleParameter("@User_tablespace", OracleDbType.Varchar2);
                quanlyRole_tablespace.Value = tablespace;
                objCmd.Parameters.Add(quanlyRole_tablespace);
                

                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror2);
            }
            catch(Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void xoauserbtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                string tenuser = quanly_user_name.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Xoa_User", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter quanlyRole_tenuser = new OracleParameter("Ten_user", OracleDbType.Varchar2);
                quanlyRole_tenuser.Value = tenuser;
                objCmd.Parameters.Add(quanlyRole_tenuser);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 50));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror1);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void capnhatpass_userbtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                //khai bao bien
                string tenuser = quanly_user_name.Text.ToLower();
                string passuser = quanly_user_password.Text.ToLower();
                int quotanum = 0;
                if ((quanly_user_quota.Text != "" && quanly_user_tablespace.Text != ""))
                {
                    MessageBox.Show(quanly_user_quota.Text);
                    quotanum = Int32.Parse(quanly_user_quota.Text);
                }
                string tablespace = quanly_user_tablespace.Text.ToLower();
                //
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Sua_User", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                //ghep bien
                OracleParameter quanlyRole_tenuser = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                quanlyRole_tenuser.Value = tenuser;
                objCmd.Parameters.Add(quanlyRole_tenuser);
                OracleParameter quanlyRole_passuser = new OracleParameter("@MK", OracleDbType.Varchar2);
                quanlyRole_passuser.Value = passuser;
                objCmd.Parameters.Add(quanlyRole_passuser);
                OracleParameter quanlyRole_quota = new OracleParameter("@User_quota", OracleDbType.Int32);
                quanlyRole_quota.Value = quotanum;
                objCmd.Parameters.Add(quanlyRole_quota);
                OracleParameter quanlyRole_tablespace = new OracleParameter("@User_tablespace", OracleDbType.Varchar2);
                quanlyRole_tablespace.Value = tablespace;
                objCmd.Parameters.Add(quanlyRole_tablespace);


                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror2);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
        private void rolequyen_search_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                DataTable newdt = new DataTable();
                String search = inforquyen_role.Text.ToUpper();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Xem_moi_role_tren_bang", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("ten_role", OracleDbType.Varchar2).Value = search;
                objCmd.Parameters.Add("CAU2c", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(objCmd);
                da.Fill(newdt);
                searchquyenrole.DataSource = newdt;
                MessageBox.Show("Tim kiem thanh cong!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }
            Console.Read();
        }
    }
}
