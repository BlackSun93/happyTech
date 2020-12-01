﻿/**
 * 
 * File: ConfirmApplicantForm.cs
 * 
 * Author 1: Campo, Simone. 1911840
 * Author 2: Osborne, Oliver. 1602819
 * Course: BEng (Hons) Computer Science, Year 2 Trimester 1
 * 
 * Summary:     This file displays all applicants added by the user.
 *              It allows the user to delete all applicants, to add
 *              more, and to begin feedback.
 *              
 */

using System;
using System.Data;
using System.Windows.Forms;

namespace HappyTech
{
    public partial class ConfirmApplicantForm : Form
    {
        string cancelStage;

        /// <summary>
        /// Constructor of the current Form
        /// </summary>
        /// <param name="value">holds the value which says whether the recruiter added a new applicant</param>
        public ConfirmApplicantForm(bool value)
        {
            InitializeComponent();

            cancelStage = "notClicked";

            // Checks whether the value is false
            if (value == false)
            {
                // successfull message is not displayed
                lbSuccess.Visible = false;
            }

            // Load the Applicants added into the Database
            DataSet ds = Connection.GetDbConn().getDataSet(SqlQueries.SelectApplicant());
            dgvApplicant.DataSource = ds.Tables[0]; //shows first table
            for (int i = 0; i < dgvApplicant.Columns.Count; i++)
            {
                dgvApplicant.Columns[i].Width = 181;
            }

            // displays how many applicants have been added so far
            lblRecruiterVal.Text = Recruiter.GetInstance().Name + " " + Recruiter.GetInstance().Surname;
            lblAppTotalVal.Text = Applicant.applicants.Count.ToString();
        }

        /// <summary>
        /// Occurs when the recruiter clicks the new button
        /// </summary>
        private void btNewApp_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Dashform displayed again
            DashForm f2 = new DashForm("newApp");
            f2.Show();
        }

        /// <summary>
        /// Occurs when the recruiter clics the Next button
        /// </summary>
        private void btStartFeed_Click(object sender, EventArgs e)
        {
            this.Hide();

            // New EditorForm is created passing by values 2 (Default value) and 0
            FeedbackForm f = FeedbackClass.NextForm("default", 0); 

            f.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        private void btCancel_Click(object sender, EventArgs e)
        {
            if (cancelStage == "notClicked")
            {
                btCancel.Text = "Are you sure?";
                cancelStage = "clicked";
            }
            else if (cancelStage == "clicked")
            {
                this.Hide();
                DashForm f2 = new DashForm("default");
                f2.Show();
            }
        }
    }
}