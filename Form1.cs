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
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
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
                OracleCommand objCmd = new OracleCommand("c##administrator.Set_role_user", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter Ten_userParam = new OracleParameter("Ten_user", OracleDbType.Varchar2);
                Ten_userParam.Value = grantrole_username.Text;
                objCmd.Parameters.Add(Ten_userParam);
                OracleParameter Ten_roleParam = new OracleParameter("Ten_role", OracleDbType.Varchar2);
                Ten_roleParam.Value = grantrole_rolename.Text;
                objCmd.Parameters.Add(Ten_roleParam);
                OracleParameter mk_roleParam = new OracleParameter("Pass_role", OracleDbType.Varchar2);
                mk_roleParam.Value = grantrole_rolepassif.Text;
                objCmd.Parameters.Add(mk_roleParam);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 200));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror1);
                /*
                if (quanly_grantoption.Checked)
                {
                    OracleParameter grant_optionParam = new OracleParameter("@grant_option", OracleDbType.Int32);
                    grant_optionParam.Value = 1;
                    objCmd.Parameters.Add(grant_optionParam);
                }
                */
            } catch (Exception err)
            {
                //MessageBox.Show("Da co loi xay ra. Loi nhu sau: " + err);
                MessageBox.Show("Da co loi xay ra");
               // Console.WriteLine(err.StackTrace);
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
                MessageBox.Show("Da co loi xay ra.");
                //Console.WriteLine(err.StackTrace);
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
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
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
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
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
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
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
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
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
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Sua_User_password", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                //ghep bien
                OracleParameter quanlyRole_tenuser = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                quanlyRole_tenuser.Value = tenuser;
                objCmd.Parameters.Add(quanlyRole_tenuser);
                OracleParameter quanlyRole_passuser = new OracleParameter("@MK_moi", OracleDbType.Varchar2);
                quanlyRole_passuser.Value = passuser;
                objCmd.Parameters.Add(quanlyRole_passuser);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror2);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
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
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }
            Console.Read();
        }

        private void inforquyen_searchuser_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                DataTable newdt = new DataTable();
                String search = inforquyen_user.Text.ToUpper();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Xem_moi_user_tren_bang", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("ten_user", OracleDbType.Varchar2).Value = search;
                objCmd.Parameters.Add("CAU2a", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(objCmd);
                da.Fill(newdt);
                searchuserquyen.DataSource = newdt;
                MessageBox.Show("Tim kiem thanh cong!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cap nhat quota cho tablespace
            conn.Open();
            try
            {
                //khai bao bien
                string tenuser = quanly_user_name.Text.ToLower();
                int quotamb = 5;
                string tablespacename = quanly_user_tablespace.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Sua_User_Quota", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                if(quanly_user_tablespace.Text != "" && quanly_user_quota.Text != "")
                {
                    //MessageBox.Show("ahihi");
                    quotamb = Int32.Parse(quanly_user_quota.Text);
                    //ghep bien
                    OracleParameter quanlyRole_tenuser = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                    quanlyRole_tenuser.Value = tenuser;
                    objCmd.Parameters.Add(quanlyRole_tenuser);
                    OracleParameter quanlyRole_quotauser = new OracleParameter("@User_quota", OracleDbType.Int32);
                    quanlyRole_quotauser.Value = quotamb;
                    objCmd.Parameters.Add(quanlyRole_quotauser);
                    OracleParameter quanlyRole_tablespace = new OracleParameter("@User_tablespace", OracleDbType.Varchar2);
                    quanlyRole_tablespace.Value = tablespacename;
                    objCmd.Parameters.Add(quanlyRole_tablespace);
                    objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 400));
                    objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                    objCmd.ExecuteNonQuery();
                    string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                    MessageBox.Show(perror2);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra");
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void selectbtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (capquyenrolerb.Checked) //Neu nguoi dung chon role --> if bang nao --> select view tren bang do
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    try
                    {
                        string userRole = capquyen_role_name.Text;
                        string userView = viewinputname_capquyen.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Select_Thuoc_cho_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userRole = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userRole.Value = userRole;
                        objCmd.Parameters.Add(capquyen_userRole);
                        OracleParameter capquyen_viewName = new OracleParameter("@Ten_view", OracleDbType.Varchar2);
                        capquyen_viewName.Value = userView;
                        objCmd.Parameters.Add(capquyen_viewName);
                        //doi voi cac bien option
                        OracleParameter T_IDParam = new OracleParameter("@T_ID", OracleDbType.Int32);
                        if (T_ID.Checked)
                        {
                            T_IDParam.Value = 1;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        else
                        {
                            T_IDParam.Value = 0;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        OracleParameter T_TENTHUOCParam = new OracleParameter("@T_TENTHUOC", OracleDbType.Int32);
                        if (T_TENTHUOC.Checked)
                        {
                            T_TENTHUOCParam.Value = 1;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        else
                        {
                            T_TENTHUOCParam.Value = 0;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        OracleParameter T_HANGParam = new OracleParameter("@T_HANG", OracleDbType.Int32);
                        if (T_HANG.Checked)
                        {
                            T_HANGParam.Value = 1;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        else
                        {
                            T_HANGParam.Value = 0;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        OracleParameter T_DONGIAParam = new OracleParameter("@T_DONGIA", OracleDbType.Int32);
                        if (T_DONGIA.Checked)
                        {
                            T_DONGIAParam.Value = 1;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        else
                        {
                            T_DONGIAParam.Value = 0;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra"); ;
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        
                        string userRole = capquyen_role_name.Text;
                        string userView = viewinputname_capquyen.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Select_Nhan_Vien_cho_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userRole = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userRole.Value = userRole;
                        objCmd.Parameters.Add(capquyen_userRole);
                        OracleParameter capquyen_viewName = new OracleParameter("@Ten_view", OracleDbType.Varchar2);
                        capquyen_viewName.Value = userView;
                        objCmd.Parameters.Add(capquyen_viewName);
                        //Doi voi cac bien option
                        OracleParameter NV_IDParam = new OracleParameter("@NV_ID", OracleDbType.Int32);
                        if (NV_ID.Checked)
                        {
                            NV_IDParam.Value = 1;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        else
                        {
                            NV_IDParam.Value = 0;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        OracleParameter NV_TENParam = new OracleParameter("@NV_TEN", OracleDbType.Int32);
                        if (NV_TEN.Checked)
                        {
                            NV_TENParam.Value = 1;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        else
                        {
                            NV_TENParam.Value = 0;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        OracleParameter NV_NAMSINHParam = new OracleParameter("@NV_NAMSINH", OracleDbType.Int32);
                        if (NV_NAMSINH.Checked)
                        {
                            NV_NAMSINHParam.Value = 1;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        else
                        {
                            NV_NAMSINHParam.Value = 0;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        OracleParameter NV_DIACHIParam = new OracleParameter("@NV_DIACHI", OracleDbType.Int32);
                        if (NV_DIACHI.Checked)
                        {
                            NV_DIACHIParam.Value = 1;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        else
                        {
                            NV_DIACHIParam.Value = 0;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        OracleParameter NV_SDTParam = new OracleParameter("@NV_SDT", OracleDbType.Int32);
                        if (NV_SDT.Checked)
                        {
                            NV_SDTParam.Value = 1;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        else
                        {
                            NV_SDTParam.Value = 0;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        OracleParameter NV_LUONGParam = new OracleParameter("@NV_LUONG", OracleDbType.Int32);
                        if (NV_LUONG.Checked)
                        {
                            NV_LUONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        else
                        {
                            NV_LUONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        OracleParameter NV_LUONGCBParam = new OracleParameter("@NV_LUONGCB", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LUONGCBParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        else
                        {
                            NV_LUONGCBParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        OracleParameter NV_LOAIParam = new OracleParameter("@NV_LOAI", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LOAIParam.Value = 1;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        else
                        {
                            NV_LOAIParam.Value = 0;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        OracleParameter NV_SONGAYCONGParam = new OracleParameter("@NV_SONGAYCONG", OracleDbType.Int32);
                        if (NV_SONGAYCONG.Checked)
                        {
                            NV_SONGAYCONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        else
                        {
                            NV_SONGAYCONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        OracleParameter NV_PHUCAPParam = new OracleParameter("@NV_PHUCAP", OracleDbType.Int32);
                        if (NV_PHUCAP.Checked)
                        {
                            NV_PHUCAPParam.Value = 1;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        else
                        {
                            NV_PHUCAPParam.Value = 0;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        OracleParameter NV_PBParam = new OracleParameter("@NV_PB", OracleDbType.Int32);
                        if (NV_PB.Checked)
                        {
                            NV_PBParam.Value = 1;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        else
                        {
                            NV_PBParam.Value = 0;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        OracleParameter NV_CONLVParam = new OracleParameter("@NV_CONLV", OracleDbType.Int32);
                        NV_CONLVParam.Value = NV_CONLV.Checked ? 1 : 0; // Su dung toan tu 3 ngoi
                        objCmd.Parameters.Add(NV_CONLVParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra") ;
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch(Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else if (capquyenuserrb.Checked)
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    //MessageBox.Show("Tab Thuoc dang duoc mo");
                    try
                    {
                        string userName = capquyen_user_name.Text;
                        string userView = viewinputname_capquyen.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Select_Thuoc_cho_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        OracleParameter capquyen_viewName = new OracleParameter("@Ten_view", OracleDbType.Varchar2);
                        capquyen_viewName.Value = userView;
                        objCmd.Parameters.Add(capquyen_viewName);
                        //doi voi cac bien option
                        OracleParameter T_IDParam = new OracleParameter("@T_ID", OracleDbType.Int32);
                        if (T_ID.Checked)
                        {
                            T_IDParam.Value = 1;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        else
                        {
                            T_IDParam.Value = 0;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        OracleParameter T_TENTHUOCParam = new OracleParameter("@T_TENTHUOC", OracleDbType.Int32);
                        if (T_TENTHUOC.Checked)
                        {
                            T_TENTHUOCParam.Value = 1;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        else
                        {
                            T_TENTHUOCParam.Value = 0;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        OracleParameter T_HANGParam = new OracleParameter("@T_HANG", OracleDbType.Int32);
                        if (T_HANG.Checked)
                        {
                            T_HANGParam.Value = 1;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        else
                        {
                            T_HANGParam.Value = 0;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        OracleParameter T_DONGIAParam = new OracleParameter("@T_DONGIA", OracleDbType.Int32);
                        if (T_DONGIA.Checked)
                        {
                            T_DONGIAParam.Value = 1;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        else
                        {
                            T_DONGIAParam.Value = 0;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        OracleParameter grant_optionParam = new OracleParameter("@GRANT_OP", OracleDbType.Int32);
                        if (grantoption_THUOC.Checked)
                        {
                            grant_optionParam.Value = 1;
                            objCmd.Parameters.Add(grant_optionParam);
                        }
                        else
                        {
                            grant_optionParam.Value = 0;
                            objCmd.Parameters.Add(grant_optionParam);
                        }
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra" + err); ;
                        Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        string userName = capquyen_user_name.Text;
                        string userView = viewinputname_capquyen.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Select_Nhan_Vien_cho_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        OracleParameter capquyen_viewName = new OracleParameter("@Ten_view", OracleDbType.Varchar2);
                        capquyen_viewName.Value = userView;
                        objCmd.Parameters.Add(capquyen_viewName);
                        //Doi voi cac bien option
                        OracleParameter NV_IDParam = new OracleParameter("@NV_ID", OracleDbType.Int32);
                        if (NV_ID.Checked)
                        {
                            NV_IDParam.Value = 1;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        else
                        {
                            NV_IDParam.Value = 0;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        OracleParameter NV_TENParam = new OracleParameter("@NV_TEN", OracleDbType.Int32);
                        if (NV_TEN.Checked)
                        {
                            NV_TENParam.Value = 1;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        else
                        {
                            NV_TENParam.Value = 0;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        OracleParameter NV_NAMSINHParam = new OracleParameter("@NV_NAMSINH", OracleDbType.Int32);
                        if (NV_NAMSINH.Checked)
                        {
                            NV_NAMSINHParam.Value = 1;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        else
                        {
                            NV_NAMSINHParam.Value = 0;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        OracleParameter NV_DIACHIParam = new OracleParameter("@NV_DIACHI", OracleDbType.Int32);
                        if (NV_DIACHI.Checked)
                        {
                            NV_DIACHIParam.Value = 1;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        else
                        {
                            NV_DIACHIParam.Value = 0;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        OracleParameter NV_SDTParam = new OracleParameter("@NV_SDT", OracleDbType.Int32);
                        if (NV_SDT.Checked)
                        {
                            NV_SDTParam.Value = 1;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        else
                        {
                            NV_SDTParam.Value = 0;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        OracleParameter NV_LUONGParam = new OracleParameter("@NV_LUONG", OracleDbType.Int32);
                        if (NV_LUONG.Checked)
                        {
                            NV_LUONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        else
                        {
                            NV_LUONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        OracleParameter NV_LUONGCBParam = new OracleParameter("@NV_LUONGCB", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LUONGCBParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        else
                        {
                            NV_LUONGCBParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        OracleParameter NV_LOAIParam = new OracleParameter("@NV_LOAI", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LOAIParam.Value = 1;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        else
                        {
                            NV_LOAIParam.Value = 0;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        OracleParameter NV_SONGAYCONGParam = new OracleParameter("@NV_SONGAYCONG", OracleDbType.Int32);
                        if (NV_SONGAYCONG.Checked)
                        {
                            NV_SONGAYCONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        else
                        {
                            NV_SONGAYCONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        OracleParameter NV_PHUCAPParam = new OracleParameter("@NV_PHUCAP", OracleDbType.Int32);
                        if (NV_PHUCAP.Checked)
                        {
                            NV_PHUCAPParam.Value = 1;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        else
                        {
                            NV_PHUCAPParam.Value = 0;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        OracleParameter NV_PBParam = new OracleParameter("@NV_PB", OracleDbType.Int32);
                        if (NV_PB.Checked)
                        {
                            NV_PBParam.Value = 1;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        else
                        {
                            NV_PBParam.Value = 0;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        OracleParameter NV_CONLVParam = new OracleParameter("@NV_CONLV", OracleDbType.Int32);
                        NV_CONLVParam.Value = NV_CONLV.Checked ? 1:0; // Su dung toan tu 3 ngoi
                        objCmd.Parameters.Add(NV_CONLVParam);
                        OracleParameter grant_optionParam = new OracleParameter("@GRANT_OP", OracleDbType.Int32);
                        grant_optionParam.Value = grantNhanvien.Checked ? 1 : 0;
                        objCmd.Parameters.Add(grant_optionParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra" + err); ;
                        Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void grantquyen_btn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Hienthiquyenmuccot", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                objCmd.Parameters.Add("p_ketqua", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(objCmd);
                da.Fill(dt);
                grantquyen_listquyen.DataSource = dt;
                MessageBox.Show("Tim kiem thanh cong!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        // Hien thi ds quyen tinh den muc bang - revoke
        private void button2_Click(object sender, EventArgs e)
        {

            conn.Open();
            try
            {
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Hienthiquyenmucbang", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                objCmd.Parameters.Add("p_ketqua", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(objCmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                MessageBox.Show("Tim kiem thanh cong!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void revokequyen_btn_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Hienthiquyenmuccot", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                objCmd.Parameters.Add("p_ketqua", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(objCmd);
                da.Fill(dt);
                revokequyen_listquyen.DataSource = dt;
                MessageBox.Show("Tim kiem thanh cong!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Hienthiquyenmucbang", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                objCmd.Parameters.Add("p_ketqua", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(objCmd);
                da.Fill(dt);
                dataGridView3.DataSource = dt;
                MessageBox.Show("Tim kiem thanh cong!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        
        private void updatebtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (capquyenrolerb.Checked) //Neu nguoi dung chon role --> if bang nao --> select view tren bang do
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    try
                    {
                        string userRole = capquyen_role_name.Text;
                        
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Update_Thuoc_cho_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userRole = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userRole.Value = userRole;
                        objCmd.Parameters.Add(capquyen_userRole);
                        
                        //doi voi cac bien option
                        OracleParameter T_IDParam = new OracleParameter("@T_ID", OracleDbType.Int32);
                        if (T_ID.Checked)
                        {
                            T_IDParam.Value = 1;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        else
                        {
                            T_IDParam.Value = 0;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        OracleParameter T_TENTHUOCParam = new OracleParameter("@T_TENTHUOC", OracleDbType.Int32);
                        if (T_TENTHUOC.Checked)
                        {
                            T_TENTHUOCParam.Value = 1;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        else
                        {
                            T_TENTHUOCParam.Value = 0;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        OracleParameter T_HANGParam = new OracleParameter("@T_HANG", OracleDbType.Int32);
                        if (T_HANG.Checked)
                        {
                            T_HANGParam.Value = 1;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        else
                        {
                            T_HANGParam.Value = 0;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        OracleParameter T_DONGIAParam = new OracleParameter("@T_DONGIA", OracleDbType.Int32);
                        if (T_DONGIA.Checked)
                        {
                            T_DONGIAParam.Value = 1;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        else
                        {
                            T_DONGIAParam.Value = 0;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        string userRole = capquyen_role_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Update_Nhan_Vien_cho_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        
                        OracleParameter capquyen_userRole = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userRole.Value = userRole;
                        objCmd.Parameters.Add(capquyen_userRole);
                        
                        //Doi voi cac bien option
                        OracleParameter NV_IDParam = new OracleParameter("@NV_ID", OracleDbType.Int32);
                        if (NV_ID.Checked)
                        {
                            NV_IDParam.Value = 1;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        else
                        {
                            NV_IDParam.Value = 0;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        OracleParameter NV_TENParam = new OracleParameter("@NV_TEN", OracleDbType.Int32);
                        if (NV_TEN.Checked)
                        {
                            NV_TENParam.Value = 1;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        else
                        {
                            NV_TENParam.Value = 0;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        OracleParameter NV_NAMSINHParam = new OracleParameter("@NV_NAMSINH", OracleDbType.Int32);
                        if (NV_NAMSINH.Checked)
                        {
                            NV_NAMSINHParam.Value = 1;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        else
                        {
                            NV_NAMSINHParam.Value = 0;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        OracleParameter NV_DIACHIParam = new OracleParameter("@NV_DIACHI", OracleDbType.Int32);
                        if (NV_DIACHI.Checked)
                        {
                            NV_DIACHIParam.Value = 1;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        else
                        {
                            NV_DIACHIParam.Value = 0;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        OracleParameter NV_SDTParam = new OracleParameter("@NV_SDT", OracleDbType.Int32);
                        if (NV_SDT.Checked)
                        {
                            NV_SDTParam.Value = 1;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        else
                        {
                            NV_SDTParam.Value = 0;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        OracleParameter NV_LUONGParam = new OracleParameter("@NV_LUONG", OracleDbType.Int32);
                        if (NV_LUONG.Checked)
                        {
                            NV_LUONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        else
                        {
                            NV_LUONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        OracleParameter NV_LUONGCBParam = new OracleParameter("@NV_LUONGCB", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LUONGCBParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        else
                        {
                            NV_LUONGCBParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        OracleParameter NV_LOAIParam = new OracleParameter("@NV_LOAI", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LOAIParam.Value = 1;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        else
                        {
                            NV_LOAIParam.Value = 0;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        OracleParameter NV_SONGAYCONGParam = new OracleParameter("@NV_SONGAYCONG", OracleDbType.Int32);
                        if (NV_SONGAYCONG.Checked)
                        {
                            NV_SONGAYCONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        else
                        {
                            NV_SONGAYCONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        OracleParameter NV_PHUCAPParam = new OracleParameter("@NV_PHUCAP", OracleDbType.Int32);
                        if (NV_PHUCAP.Checked)
                        {
                            NV_PHUCAPParam.Value = 1;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        else
                        {
                            NV_PHUCAPParam.Value = 0;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        OracleParameter NV_PBParam = new OracleParameter("@NV_PB", OracleDbType.Int32);
                        if (NV_PB.Checked)
                        {
                            NV_PBParam.Value = 1;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        else
                        {
                            NV_PBParam.Value = 0;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        OracleParameter NV_CONLVParam = new OracleParameter("@NV_CONLV", OracleDbType.Int32);
                        NV_CONLVParam.Value = NV_CONLV.Checked ? 1 : 0; // Su dung toan tu 3 ngoi
                        objCmd.Parameters.Add(NV_CONLVParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else if (capquyenuserrb.Checked)
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    try
                    {
                        string userRole = capquyen_user_name.Text;

                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Update_Thuoc_cho_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userRole = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userRole.Value = userRole;
                        objCmd.Parameters.Add(capquyen_userRole);

                        //doi voi cac bien option
                        OracleParameter T_IDParam = new OracleParameter("@T_ID", OracleDbType.Int32);
                        if (T_ID.Checked)
                        {
                            T_IDParam.Value = 1;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        else
                        {
                            T_IDParam.Value = 0;
                            objCmd.Parameters.Add(T_IDParam);
                        }
                        OracleParameter T_TENTHUOCParam = new OracleParameter("@T_TENTHUOC", OracleDbType.Int32);
                        if (T_TENTHUOC.Checked)
                        {
                            T_TENTHUOCParam.Value = 1;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        else
                        {
                            T_TENTHUOCParam.Value = 0;
                            objCmd.Parameters.Add(T_TENTHUOCParam);
                        }
                        OracleParameter T_HANGParam = new OracleParameter("@T_HANG", OracleDbType.Int32);
                        if (T_HANG.Checked)
                        {
                            T_HANGParam.Value = 1;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        else
                        {
                            T_HANGParam.Value = 0;
                            objCmd.Parameters.Add(T_HANGParam);
                        }
                        OracleParameter T_DONGIAParam = new OracleParameter("@T_DONGIA", OracleDbType.Int32);
                        if (T_DONGIA.Checked)
                        {
                            T_DONGIAParam.Value = 1;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        else
                        {
                            T_DONGIAParam.Value = 0;
                            objCmd.Parameters.Add(T_DONGIAParam);
                        }
                        OracleParameter grant_optionParam = new OracleParameter("@GRANT_OP", OracleDbType.Int32);
                        grant_optionParam.Value = grantoption_THUOC.Checked ? 1 : 0;
                        objCmd.Parameters.Add(grant_optionParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra" + err); ;
                        Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        string userName = capquyen_user_name.Text;

                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Quyen_Update_Nhan_Vien_cho_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        
                        //Doi voi cac bien option
                        OracleParameter NV_IDParam = new OracleParameter("@NV_ID", OracleDbType.Int32);
                        if (NV_ID.Checked)
                        {
                            NV_IDParam.Value = 1;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        else
                        {
                            NV_IDParam.Value = 0;
                            objCmd.Parameters.Add(NV_IDParam);
                        }
                        OracleParameter NV_TENParam = new OracleParameter("@NV_TEN", OracleDbType.Int32);
                        if (NV_TEN.Checked)
                        {
                            NV_TENParam.Value = 1;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        else
                        {
                            NV_TENParam.Value = 0;
                            objCmd.Parameters.Add(NV_TENParam);
                        }
                        OracleParameter NV_NAMSINHParam = new OracleParameter("@NV_NAMSINH", OracleDbType.Int32);
                        if (NV_NAMSINH.Checked)
                        {
                            NV_NAMSINHParam.Value = 1;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        else
                        {
                            NV_NAMSINHParam.Value = 0;
                            objCmd.Parameters.Add(NV_NAMSINHParam);
                        }
                        OracleParameter NV_DIACHIParam = new OracleParameter("@NV_DIACHI", OracleDbType.Int32);
                        if (NV_DIACHI.Checked)
                        {
                            NV_DIACHIParam.Value = 1;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        else
                        {
                            NV_DIACHIParam.Value = 0;
                            objCmd.Parameters.Add(NV_DIACHIParam);
                        }
                        OracleParameter NV_SDTParam = new OracleParameter("@NV_SDT", OracleDbType.Int32);
                        if (NV_SDT.Checked)
                        {
                            NV_SDTParam.Value = 1;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        else
                        {
                            NV_SDTParam.Value = 0;
                            objCmd.Parameters.Add(NV_SDTParam);
                        }
                        OracleParameter NV_LUONGParam = new OracleParameter("@NV_LUONG", OracleDbType.Int32);
                        if (NV_LUONG.Checked)
                        {
                            NV_LUONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        else
                        {
                            NV_LUONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGParam);
                        }
                        OracleParameter NV_LUONGCBParam = new OracleParameter("@NV_LUONGCB", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LUONGCBParam.Value = 1;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        else
                        {
                            NV_LUONGCBParam.Value = 0;
                            objCmd.Parameters.Add(NV_LUONGCBParam);
                        }
                        OracleParameter NV_LOAIParam = new OracleParameter("@NV_LOAI", OracleDbType.Int32);
                        if (NV_LUONGCB.Checked)
                        {
                            NV_LOAIParam.Value = 1;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        else
                        {
                            NV_LOAIParam.Value = 0;
                            objCmd.Parameters.Add(NV_LOAIParam);
                        }
                        OracleParameter NV_SONGAYCONGParam = new OracleParameter("@NV_SONGAYCONG", OracleDbType.Int32);
                        if (NV_SONGAYCONG.Checked)
                        {
                            NV_SONGAYCONGParam.Value = 1;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        else
                        {
                            NV_SONGAYCONGParam.Value = 0;
                            objCmd.Parameters.Add(NV_SONGAYCONGParam);
                        }
                        OracleParameter NV_PHUCAPParam = new OracleParameter("@NV_PHUCAP", OracleDbType.Int32);
                        if (NV_PHUCAP.Checked)
                        {
                            NV_PHUCAPParam.Value = 1;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        else
                        {
                            NV_PHUCAPParam.Value = 0;
                            objCmd.Parameters.Add(NV_PHUCAPParam);
                        }
                        OracleParameter NV_PBParam = new OracleParameter("@NV_PB", OracleDbType.Int32);
                        if (NV_PB.Checked)
                        {
                            NV_PBParam.Value = 1;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        else
                        {
                            NV_PBParam.Value = 0;
                            objCmd.Parameters.Add(NV_PBParam);
                        }
                        OracleParameter NV_CONLVParam = new OracleParameter("@NV_CONLV", OracleDbType.Int32);
                        NV_CONLVParam.Value = NV_CONLV.Checked ? 1 : 0; // Su dung toan tu 3 ngoi
                        objCmd.Parameters.Add(NV_CONLVParam);
                        OracleParameter grant_optionParam = new OracleParameter("@GRANT_OP", OracleDbType.Int32);
                        grant_optionParam.Value = grantNhanvien.Checked ? 1 : 0;
                        objCmd.Parameters.Add(grant_optionParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra" + err); ;
                        Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (capquyenrolerb.Checked) //Neu nguoi dung chon role --> if bang nao --> select view tren bang do
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    try
                    {
                        string userName = capquyen_role_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Insert_Thuoc_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        //khai bao bien out
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        string userName = capquyen_role_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Insert_Nhan_Vien_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else if (capquyenuserrb.Checked)
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    try
                    {
                        string userName = capquyen_user_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Insert_Thuoc_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        OracleParameter grant_optionParam = new OracleParameter("@Grant_op", OracleDbType.Int32);
                        grant_optionParam.Value = grantoption_THUOC.Checked ? 1 : 0;
                        objCmd.Parameters.Add(grant_optionParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        string userName = capquyen_user_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Insert_Nhan_Vien_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        OracleParameter grant_optionParam = new OracleParameter("@Grant_op", OracleDbType.Int32);
                        grant_optionParam.Value = grantNhanvien.Checked ? 1 : 0;
                        objCmd.Parameters.Add(grant_optionParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (capquyenrolerb.Checked) //Neu nguoi dung chon role --> if bang nao --> select view tren bang do
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    try
                    {
                        string userName = capquyen_role_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Delete_Thuoc_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        string userName = capquyen_role_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Delete_Nhan_Vien_Role", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_role", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                        //Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else if (capquyenuserrb.Checked)
            {
                if (this.smallControl.SelectedTab == thuoc)
                {
                    try
                    {
                        string userName = capquyen_user_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Delete_Thuoc_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        OracleParameter grant_optionParam = new OracleParameter("@Grant_op", OracleDbType.Int32);
                        grant_optionParam.Value = grantoption_THUOC.Checked ? 1 : 0;
                        objCmd.Parameters.Add(grant_optionParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra" + err); ;
                        Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == nhanvien)
                {
                    try
                    {
                        string userName = capquyen_user_name.Text;
                        OracleCommand objCmd = new OracleCommand("c##administrator.AD_Cap_Delete_Nhan_Vien_User", conn);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //ghep bien
                        OracleParameter capquyen_userName = new OracleParameter("@Ten_user", OracleDbType.Varchar2);
                        capquyen_userName.Value = userName;
                        objCmd.Parameters.Add(capquyen_userName);
                        OracleParameter grant_optionParam = new OracleParameter("@Grant_op", OracleDbType.Int32);
                        grant_optionParam.Value = grantNhanvien.Checked ? 1 : 0;
                        objCmd.Parameters.Add(grant_optionParam);
                        objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 32767));
                        objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        string perror2 = objCmd.Parameters["@p_Error"].Value.ToString();
                        MessageBox.Show("Thong bao" + perror2);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra" + err); ;
                        Console.WriteLine(err.StackTrace);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == phongban)
                {
                    try
                    {
                        MessageBox.Show("You choose tab PhongBan with role option. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == benhnhan)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Benh nhan. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == dichvukham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Dich vu kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == kham)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Kham. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == donthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == ctdonthuoc)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Ct don thuoc. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (this.smallControl.SelectedTab == sudungdv)
                {
                    try
                    {
                        MessageBox.Show("You choose tab Su dung dich vu. We will update soon!");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Da co loi xay ra");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void revokeselect_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                    string tenrobject = revoke_object.Text.ToLower();
                    string tenview = revoke_view.Text.ToLower();
                    OracleCommand objCmd = new OracleCommand("c##administrator.AD_Thu_Hoi_Select_Bang_view", conn);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter quanlyRole_tenrole = new OracleParameter("@Ten_user_role", OracleDbType.Varchar2);
                    quanlyRole_tenrole.Value = tenrobject;
                    objCmd.Parameters.Add(quanlyRole_tenrole);
                    OracleParameter quanlyRole_passrole = new OracleParameter("@Ten_bang_view", OracleDbType.Varchar2);
                    quanlyRole_passrole.Value = tenview;
                    objCmd.Parameters.Add(quanlyRole_passrole);
                    objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 200));
                    objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                    objCmd.ExecuteNonQuery();
                    string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                    MessageBox.Show(perror1);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void revokeupdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                string tenrobject = revoke_object.Text.ToLower();
                string tenview = revoke_view.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Thu_Hoi_Update_Bang_view", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter quanlyRole_tenrole = new OracleParameter("@Ten_user_role", OracleDbType.Varchar2);
                quanlyRole_tenrole.Value = tenrobject;
                objCmd.Parameters.Add(quanlyRole_tenrole);
                OracleParameter quanlyRole_passrole = new OracleParameter("@Ten_bang_view", OracleDbType.Varchar2);
                quanlyRole_passrole.Value = tenview;
                objCmd.Parameters.Add(quanlyRole_passrole);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 200));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror1);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void revokeinsert_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                string tenrobject = revoke_object.Text.ToLower();
                string tenview = revoke_view.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Thu_Hoi_Insert_Bang_view", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter quanlyRole_tenrole = new OracleParameter("@Ten_user_role", OracleDbType.Varchar2);
                quanlyRole_tenrole.Value = tenrobject;
                objCmd.Parameters.Add(quanlyRole_tenrole);
                OracleParameter quanlyRole_passrole = new OracleParameter("@Ten_bang_view", OracleDbType.Varchar2);
                quanlyRole_passrole.Value = tenview;
                objCmd.Parameters.Add(quanlyRole_passrole);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 200));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror1);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void revokedelete_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            try
            {
                string tenrobject = revoke_object.Text.ToLower();
                string tenview = revoke_view.Text.ToLower();
                OracleCommand objCmd = new OracleCommand("c##administrator.AD_Thu_Hoi_Delete_Bang_view", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                OracleParameter quanlyRole_tenrole = new OracleParameter("@Ten_user_role", OracleDbType.Varchar2);
                quanlyRole_tenrole.Value = tenrobject;
                objCmd.Parameters.Add(quanlyRole_tenrole);
                OracleParameter quanlyRole_passrole = new OracleParameter("@Ten_bang_view", OracleDbType.Varchar2);
                quanlyRole_passrole.Value = tenview;
                objCmd.Parameters.Add(quanlyRole_passrole);
                objCmd.Parameters.Add(new OracleParameter("@p_Error", OracleDbType.Varchar2, 200));
                objCmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                objCmd.ExecuteNonQuery();
                string perror1 = objCmd.Parameters["@p_Error"].Value.ToString();
                MessageBox.Show(perror1);
            }
            catch (Exception err)
            {
                MessageBox.Show("Da co loi xay ra"); ;
                //Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
        // Hien thi ds quyen tinh den muc bang - revoke

    }
}
