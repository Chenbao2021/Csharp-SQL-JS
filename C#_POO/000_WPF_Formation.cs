using MFControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MFThemes;
using System.Linq;
using System.Drawing;



namespace MEDEXE_Formation1
{
  public partial class FrmEmployes : MFForm
  {

    #region Employee data table constants.
    private readonly string m_sEmp_firstName     = "first_name";
    private readonly string m_sEmp_lastName      = "last_name";
    private readonly string m_sEmp_startDate     = "start_date";
    private readonly string m_sEmp_endDate       = "end_date";
    private readonly string m_sEmp_position      = "position";
    private readonly string m_sEmp_iPosition     = "$iPosition";
    private readonly string m_sEmp_annualSalary  = "annual_salary";
    private readonly string m_sEmp_status        = "$status";
    private readonly string DISPLAY_MEMBER       = "display_member";
    private readonly string VALUE_MEMBER         = "ID";

    public enum ScreenMode
    {
      VIEW    = 0,
      AMEND   = 1,
      CREATE  = 2
    }
    private ScreenMode m_mode = ScreenMode.VIEW;

    public enum UpdateMode
    {
      U = 0, // Update
      G = 1, // Get
      L = 2, // Logical delete
      D = 3, // Undelete
      P = 4, // Physical delete
      A = 5 // Append
    }
    #endregion
    private DataTable m_dtSourceEmployee  = null;
    private DataTable m_dtSourcePosition  = null;
    private string m_sUser_operating      = "CYU";
    private DataView m_dv = null;

    public FrmEmployes()
    {
      InitializeComponent();
      MFGridHelpers.InitGraphicsGrid(FormTheme.m_gridStyle, mfGridEmployee, true, GridStyle.BackColorMode.MAIN_BACKCOLOR);
    }

    private void FrmEmployes_Load(object sender, EventArgs e)
    {
      m_ckCancelled.Checked = false;
      InitDtComboPosition();
      InitDtEmployee();
    }
    private void InitDtComboPosition()
    {
      if (m_dtSourcePosition == null)
      {
        DataSet dsRetour = DB.DataSetLoadWithParams("CYU_FORMATION_GET_COMBO_POSITIONS", null);
        if(dsRetour != null && dsRetour.Tables.Count > 0)
        {
          m_dtSourcePosition      = dsRetour.Tables[0];
          m_cbPosition.FillRefCombo(m_dtSourcePosition, "", "display_order", "ID", DISPLAY_MEMBER);
        }
      }
    }
    private void InitDtEmployee()
    {

      m_dtSourceEmployee = new DataTable();

      Dictionary<string, Object> dicProcsParams = new Dictionary<string, Object>();
      try
      {
        DataSet dsRetour = DB.DataSetLoadWithParams("CYU_FORMATION_GET_EMPLOYEE_TABLE", dicProcsParams);
        if (dsRetour != null && dsRetour.Tables.Count > 0 )
        {
          m_dtSourceEmployee = dsRetour.Tables[0];
          DisplayGrid();
        }
        else throw new Exception("Problem on database");
      }
      catch (Exception ex)
      {
        MFMessageBoxDlg.ShowDialog(this,ex.Message, "Error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void Clean()
    {
      bool bCreate = m_mode == ScreenMode.CREATE;

      m_edFirstName.Value         = "";
      m_edLastName.Value          = "";
      m_edStartDate.Value         = "";

      m_edEndDate.Value           = "";
      m_cbPosition.SelectedValue  = DBNull.Value;
      m_edWage.Value              = null;

      if (bCreate) 
        m_cbPosition.SelectedValue = 3;

      mf_btDelete.Text      = "Logical delete";
      mf_btPDelete.Visible  = false;


    }
    private void ConfirmAdd()
    {
      if (VerifyScreen(true, true))
      {
        Update(UpdateMode.A);
      }
    }
    private void ConfirmUpdate()
    {
      if (VerifyScreen(true, true))
      {
        Update(UpdateMode.U);
        SetMode(ScreenMode.VIEW);
      }
    }
    private string ConvertIntToPositionName(int id)
    {
      if (m_dtSourcePosition != null)
      {
        DataRow[] drCorrespondingRow = m_dtSourcePosition.Select($"{VALUE_MEMBER} = {id}");
        if (drCorrespondingRow != null && drCorrespondingRow.Count() > 0)
        {
          string sResult = drCorrespondingRow.FirstOrDefault().Field<string>(DISPLAY_MEMBER);
          return sResult;
        }
      }
      return "Unknown";
    }
    private void DisplayGrid()
    {
      // "iPosition" => "position"
      for (int i = 0; i < m_dtSourceEmployee.Rows.Count; i++)
      {
        int iPositionNumber = m_dtSourceEmployee.Rows[i].Field<int>(m_sEmp_iPosition);
        m_dtSourceEmployee.Rows[i].SetField<string>(m_sEmp_position, ConvertIntToPositionName(iPositionNumber));
      }

      string sFilter = "";
      if (!m_ckCancelled.Checked)
        sFilter = string.Format("{0} <> 'C'", "$status");      

      m_dv = new DataView(m_dtSourceEmployee, sFilter, "$id", DataViewRowState.CurrentRows);
      mfGridEmployee.ColumnDateFmt = "dd MMM yy";
      mfGridEmployee.DataSource = new BindingSource() { DataSource =  m_dv };
      mfGridEmployee.AutoResizeColumns();

      mfGridEmployee.Columns[m_sEmp_firstName].HeaderText     = "First Name";
      mfGridEmployee.Columns[m_sEmp_lastName].HeaderText      = "Last Name";
      mfGridEmployee.Columns[m_sEmp_startDate].HeaderText     = "Start Date";
      mfGridEmployee.Columns[m_sEmp_endDate].HeaderText       = "End Date";
      mfGridEmployee.Columns[m_sEmp_annualSalary].HeaderText  = "Annual Salary";
      mfGridEmployee.Columns[m_sEmp_position].HeaderText      = "Poste";

      // Mettre en place l'alignement.
      for (int i = 0; i < mfGridEmployee.Columns.Count; i++)
      {
        if (mfGridEmployee.Columns[i].ValueType.Name == "String")
          mfGridEmployee.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        else
          mfGridEmployee.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      }

      mfGridEmployee.CellFormatting += EmployeeCellFormatting;
    }

    private void SetSelectedRowInformation()
    {
      if (mfGridEmployee.GetSelectedRows().Count() <= 0) return;
      DataRow drData = (mfGridEmployee.GetSelectedRows().FirstOrDefault() as MFGridRow).DataRow;

      if (drData != null)
      {
        m_edFirstName.Value   = drData.Field<string>(m_sEmp_firstName);
        m_edLastName.Value    = drData.Field<string>(m_sEmp_lastName);
        m_edStartDate.Value   = drData.Field<DateTime>(m_sEmp_startDate);
        m_edEndDate.Value     = drData.Field<DateTime?>(m_sEmp_endDate);
        m_cbPosition.Text     = drData.Field<string>(m_sEmp_position);
        m_edWage.Value        = drData.Field<Double>(m_sEmp_annualSalary);
        mf_btDelete.Text      = drData.Field<string>(m_sEmp_status).Replace(" ", "") == "C" ? "Undelete" : "Logical Delete";
        mf_btPDelete.Visible  = drData.Field<string>(m_sEmp_status).Replace(" ", "") == "C" ?  true : false;
      }
    }

    private void SetMode(ScreenMode mode)
    {
      bool bAmendOrCreate     = mode == ScreenMode.AMEND || mode == ScreenMode.CREATE;
      bool bView              = mode == ScreenMode.VIEW;
      bool bAmend             = mode == ScreenMode.AMEND;
      bool bAdd               = mode == ScreenMode.CREATE;
      
      mfGridEmployee.Enabled  = bView;

      m_edEndDate.Enabled     = bAmendOrCreate;
      m_edStartDate.Enabled   = bAmendOrCreate;
      m_edFirstName.Enabled   = bAmendOrCreate;
      m_edLastName.Enabled    = bAmendOrCreate;
      m_edWage.Enabled        = bAmendOrCreate;
      m_cbPosition.Enabled    = bAmendOrCreate;

      mf_btAmend.Text         = bAmendOrCreate ?  "OK"      : "Amend";
      mf_btClose.Text         = bAmendOrCreate ?  "Cancel"  : "Close";

      mf_btAdd.Enabled        = bAmendOrCreate ?  false     : true;
      mf_btDelete.Enabled     = bAmendOrCreate ?  false     : true;

      m_mode = mode;

      if(bAmend) 
        m_dtSourceEmployee.AcceptChanges();

      if (bAdd)
        mfGridEmployee.ClearSelection();
    }

    private void Update(UpdateMode mode)
    {
      string sAction = "";
      DataRow dr = null; 

      switch (mode)
      {
        case UpdateMode.U: sAction =  "U"; break;
        case UpdateMode.G: sAction =  "G"; break;
        case UpdateMode.L: sAction =  "L"; break;
        case UpdateMode.D: sAction =  "D"; break;
        case UpdateMode.A: sAction =  "A"; break;
        default:break;
      }
      if (string.IsNullOrEmpty(sAction))
        return;
      if(mfGridEmployee.GetSelectedRows().Count() > 0)
        dr = (mfGridEmployee.GetSelectedRows().FirstOrDefault() as MFGridRow).DataRow;
 
      Dictionary<string, Object> dicProcsParams = new Dictionary<string, Object>();
      if(sAction != "A") dicProcsParams["id"]   = dr.Field<int>("$id");
      dicProcsParams["status"]                  = sAction;
      dicProcsParams["first_name"]              = m_edFirstName.Value;
      dicProcsParams["last_name"]               = m_edLastName.Value;
      dicProcsParams["start_date"]              = m_edStartDate.Value;
      dicProcsParams["end_date"]                = m_edEndDate.Value;
      dicProcsParams["annual_salary"]           = m_edWage.Value;
      dicProcsParams["iPosition"]               = m_cbPosition.SelectedValue;

      dicProcsParams["user_update"]             = m_sUser_operating;
      dicProcsParams["date_update"]             = DateTime.Now;

      try
      {
        DataSet dsRetour = DB.DataSetLoadWithParams("CYU_FORMATION_DO_ACTION", dicProcsParams);

        if (dsRetour == null || dsRetour.Tables.Count == 0 || dsRetour.Tables[0].Rows.Count == 0)
        {
          MFMessageBoxDlg.ShowDialog(this, "No data received from database", "Database error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Error);
          return; 
        }

        if (dsRetour.Tables[0].Rows[0].Field<int>("codeError") == -1) 
        {
          MFMessageBoxDlg.ShowDialog(this, dsRetour.Tables[0].Rows[0].Field<string>("messageError"), "Database error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Error);
          SetSelectedRowInformation(); 
          return;  
        }
        // Physical Delete
        if(mode == UpdateMode.D)
        {
          DialogResult res = MFMessageBoxDlg.ShowDialog(this, "Do you confirm to physical delete the line?", "Confirmation", MFMessageBoxDlgButtons.OKCancel, MessageBoxIcon.Question);
          if(res == DialogResult.OK)
          {
            mfGridEmployee.BeginUpdate(); 

            m_dtSourceEmployee = m_dtSourceEmployee.Select($"$id <> {dr.Field<int>("$id")}").CopyToDataTable();
            m_dtSourceEmployee.AcceptChanges();

            DisplayGrid();
            
            Clean();
            SetMode(ScreenMode.VIEW);

            mfGridEmployee.EndUpdate(true);
          }
        } 
        else { 
          dr = mode == UpdateMode.A ? m_dtSourceEmployee.NewRow() : (mfGridEmployee.SelectedRows[0] as MFGridRow).DataRow;
          dr["$status"]       = sAction;
          dr["first_name"]    = m_edFirstName.Value;
          dr["last_name"]     = m_edLastName.Value;
          dr["start_date"]    = m_edStartDate.Value;
          dr["annual_salary"] = m_edWage.Value;
          dr["$iPosition"]    = m_cbPosition.SelectedValue;
          dr["position"]      = ConvertIntToPositionName(int.Parse(m_cbPosition.SelectedValue.ToString()));
          dr["$user_update"]  = m_sUser_operating;
          dr["$date_update"]  = DateTime.Now;
          if (m_edEndDate.Value != null) 
            dr["end_date"] = m_edEndDate.Value;

          if (mode == UpdateMode.A)
          {
            dr["$id"]     = dsRetour.Tables[0].Rows[0].Field<int>("new_id");
            m_dtSourceEmployee.Rows.Add(dr);
            m_dtSourceEmployee.AcceptChanges();
            mfGridEmployee.Rows[mfGridEmployee.Rows.Count - 1].Selected = true;
          }
          else
            m_dtSourceEmployee.AcceptChanges();
        }
        SetMode(ScreenMode.VIEW);
      }
      catch (Exception ex)
      {
        MFMessageBoxDlg.ShowDialog(this, ex.Message, "Error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTrackChanges()
    {
      if (mfGridEmployee.SelectedRows.Count == 0)
        MFMessageBoxDlg.ShowDialog(this,"You have selected no row to display track log", "Database error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Error);
      else
      {
        DataRow drcurrent_row = mfGridEmployee.GetSelectedDataRows().FirstOrDefault();
        Dictionary<string, Object> dicProcsParams = new Dictionary<string, Object>();

        dicProcsParams["id"] = drcurrent_row.Field<int>("$id");
        DataSet dsRetour = DB.DataSetLoadWithParams("CYU_FORMATION_GET_TRACK_LOGS", dicProcsParams);

        if(dsRetour.Tables.Count > 0)
        {
          DataTable dt = dsRetour.Tables[0];
          FrmTrackLogs TrackLogs = new FrmTrackLogs(this, $"Track changes of user: {drcurrent_row.Field<string>("first_name")} ");
          TrackLogs.SetDataTable(dt);
          TrackLogs.ShowDialog();
        }
      }
    }

    private void EmployeeCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.RowIndex == 0)
        return;
      DataRow drCurrent = (mfGridEmployee.Rows[e.RowIndex] as MFGridRow)?.DataRow;
      if (drCurrent == null) 
        return;
      if(!string.IsNullOrEmpty(drCurrent.Field<string>(m_sEmp_status)) && drCurrent.Field<string>(m_sEmp_status) == "C")
      {
        mfGridEmployee.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
      }
    }
    // ---- ----- ------  ------  Evénements ---- -----   ----- ------  ------
    private void Mf_GridEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (m_dtSourceEmployee != null)
      {
        SetSelectedRowInformation();
      }
    }

    private void Mf_btAmend_Click(object sender, EventArgs e)
    {
      switch (m_mode)
      {
        case ScreenMode.VIEW  :
          {
            if (mfGridEmployee.SelectedRows.Count == 0)
            {
              MFMessageBoxDlg.ShowDialog(this, "There is no row selected", "Error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Error);
              return;
            }
            SetMode(ScreenMode.AMEND); 
            break;
          }
        case ScreenMode.AMEND : ConfirmUpdate(); break;
        case ScreenMode.CREATE: ConfirmAdd();    break;
        default:  break;
      }
    }

    private void Mf_btClose_Click(object sender, EventArgs e)
    {
      if (m_mode == ScreenMode.VIEW)
      {
        this.Close();
        return;
      }
      SetMode(ScreenMode.VIEW);
      SetSelectedRowInformation();
    }
    private void Mf_btAdd_Click(object sender, EventArgs e)
    {
      SetMode(ScreenMode.CREATE);
      Clean();
    }

    private void Mf_btDelete_Click(object sender, EventArgs e)
    {
      if (mfGridEmployee.GetSelectedRows().Count() == 0)
      {
        MFMessageBoxDlg.ShowDialog(this, "There is no row selected", "Error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Error);
        return;
      }
      DataRow drData = (mfGridEmployee.GetSelectedRows().FirstOrDefault() as MFGridRow).DataRow;
      bool bDelete = mf_btDelete.Text == "Logical Delete";

      DialogResult res =  MFMessageBoxDlg.ShowDialog(this, $"Do you want to {(bDelete ? "cancel" : "active")} the line?", "Confirmation", MFMessageBoxDlgButtons.OKCancel, MessageBoxIcon.Question);

      if (res == DialogResult.OK)
      {
        Dictionary<string, Object> dicProcsParams = new Dictionary<string, Object>();

        dicProcsParams["status"]      = bDelete ? "L" : "R";
        dicProcsParams["id"]          = drData.Field<int>("$id");
        dicProcsParams["first_name"]  = drData.Field<string>("first_name");
        dicProcsParams["last_name"]   = drData.Field<string>("last_name");
        dicProcsParams["user_update"] = m_sUser_operating;
        dicProcsParams["date_update"] = DateTime.Now;

        DataSet dsRetour = DB.DataSetLoadWithParams("CYU_FORMATION_DO_ACTION", dicProcsParams);
        if (dsRetour != null && dsRetour.Tables.Count > 0  && dsRetour.Tables[0].Rows.Count > 0)
        {
          int codeError = dsRetour.Tables[0].Rows[0].Field<int>("codeError");
          mfGridEmployee.BeginEdit(true);
          if (codeError == 1)
          {
            Clean();
            if(bDelete)
            {
              drData["$status"]        = "C";
              drData["$row_forecolor"] = -65536;
              drData["end_date"]       = DateTime.Now;
            }
            else
            {
              drData["$status"]        = 'A';
              drData["$row_forecolor"] = 0;
              drData["end_date"]       = DBNull.Value;
            }
            if (mf_btDelete.Text != "Logical Delete") 
              mf_btDelete.Text = "Logical Delete";
          } else
          {
            MFMessageBoxDlg.ShowDialog(this, dsRetour.Tables[0].Rows[0].Field<string>("messageError"), "Error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Error);
          }
          m_dtSourceEmployee.AcceptChanges();
          mfGridEmployee.ClearSelection();
          mfGridEmployee.EndEdit();
        }
      }
    }

    private void M_ckCancelled_CheckedChanged(object sender, EventArgs e)
    {
      DisplayGrid();
    }
    private void Mf_GridEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      SetSelectedRowInformation();
      SetMode(ScreenMode.AMEND);
    }

    private void mf_btPDelete_Click(object sender, EventArgs e)
    {
      Update(UpdateMode.D);
    }

    private void mf_btTrackLog_Click(object sender, EventArgs e)
    {
      LoadTrackChanges();
      // FrmTrackLogs winLogs = new FrmTrackLogs();
      // winLogs.ShowDialog();
    }
  }
}

