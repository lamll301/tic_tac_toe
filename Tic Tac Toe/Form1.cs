using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        public static string connectionString = "Data Source=DESKTOP-KB3GR2O\\LAMLYLE;Initial Catalog=tic_tac_toe;Integrated Security=True";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd;
        int id_player_1 = 1;
        int id_player_2 = 2;
        string player1 = "";
        string player2 = "";
        char who = 'o';
        int move = 0;
        public Form1()
        {
            InitializeComponent();
            loadData();
        }
        private void disableBtn()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button)
                {
                    Button button = control as Button;
                    button.Enabled = false;
                }
            }
        }

        private void loadData()
        {
            con.Open();
            string query1 = "SELECT CacODaDanh FROM Player WHERE Id = " + id_player_1;
            string query2 = "SELECT CacODaDanh FROM Player WHERE Id = " + id_player_2;
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlCommand cmd2 = new SqlCommand(query2, con);
            player1 = cmd1.ExecuteScalar().ToString();
            player2 = cmd2.ExecuteScalar().ToString();

            string[] arr1 = player1.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string e1 in arr1)
            {
                Control[] controls = this.Controls.Find(e1, true);
                if (controls.Length > 0 && controls[0] is Button)
                {
                    Button foundButton = (Button)controls[0];
                    foundButton.Text = "o";
                    foundButton.BackColor = Color.Orange;
                    foundButton.Enabled = false;
                }
            }
            string[] arr2 = player2.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string e2 in arr2)
            {
                Control[] controls = this.Controls.Find(e2, true);
                if (controls.Length > 0 && controls[0] is Button)
                {
                    Button foundButton = (Button)controls[0];
                    foundButton.Text = "x";
                    foundButton.BackColor = Color.Orange;
                    foundButton.Enabled = false;
                }
            }
            move = arr1.Length + arr2.Length - 1;
            if (arr1.Length > arr2.Length) who = 'x';
            else who = 'o';
            if ((button1.Text == button2.Text && button3.Text == button2.Text && button2.Text != "") ||
                    (button4.Text == button5.Text && button5.Text == button6.Text && button5.Text != "") ||
                    (button7.Text == button8.Text && button8.Text == button9.Text && button8.Text != "") ||
                    (button1.Text == button4.Text && button4.Text == button7.Text && button4.Text != "") ||
                    (button1.Text == button5.Text && button5.Text == button9.Text && button1.Text != "") ||
                    (button2.Text == button5.Text && button5.Text == button8.Text && button2.Text != "") ||
                    (button3.Text == button6.Text && button6.Text == button9.Text && button3.Text != "") ||
                    (button3.Text == button5.Text && button5.Text == button7.Text && button3.Text != ""))
            {
                disableBtn();
            }
            con.Close();
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.BackColor = Color.Orange;

            if (who == 'o')
            {
                btn.Text = "o";
                player1 += btn.Name + ", ";
                cmd = new SqlCommand("update Player set CacODaDanh=@CacODaDanh where Id=@Id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CacODaDanh", player1);
                cmd.Parameters.AddWithValue("@Id", id_player_1);
                cmd.ExecuteNonQuery();
                con.Close();
                loadData();

                if ((button1.Text == button2.Text && button3.Text == button2.Text && button2.Text != "") ||
                    (button4.Text == button5.Text && button5.Text == button6.Text && button5.Text != "") ||
                    (button7.Text == button8.Text && button8.Text == button9.Text && button8.Text != "") ||
                    (button1.Text == button4.Text && button4.Text == button7.Text && button4.Text != "") ||
                    (button1.Text == button5.Text && button5.Text == button9.Text && button1.Text != "") ||
                    (button2.Text == button5.Text && button5.Text == button8.Text && button2.Text != "") ||
                    (button3.Text == button6.Text && button6.Text == button9.Text && button3.Text != "") ||
                    (button3.Text == button5.Text && button5.Text == button7.Text && button3.Text != ""))
                {
                    MessageBox.Show("Người chơi O chiến thắng!!!");
                    disableBtn();
                }
                else if (move == 8) MessageBox.Show("Hòa!!!");
                who = 'x';
            }
            else if (who == 'x')
            {
                btn.Text = "x";
                player2 += btn.Name + ", ";
                cmd = new SqlCommand("update Player set CacODaDanh=@CacODaDanh where Id=@Id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CacODaDanh", player2);
                cmd.Parameters.AddWithValue("@Id", id_player_2);
                cmd.ExecuteNonQuery();
                con.Close();
                loadData();

                if ((button1.Text == button2.Text && button3.Text == button2.Text && button2.Text != "") ||
                    (button4.Text == button5.Text && button5.Text == button6.Text && button5.Text != "") ||
                    (button7.Text == button8.Text && button8.Text == button9.Text && button8.Text != "") ||
                    (button1.Text == button4.Text && button4.Text == button7.Text && button4.Text != "") ||
                    (button1.Text == button5.Text && button5.Text == button9.Text && button1.Text != "") ||
                    (button2.Text == button5.Text && button5.Text == button8.Text && button2.Text != "") ||
                    (button3.Text == button6.Text && button6.Text == button9.Text && button3.Text != "") ||
                    (button3.Text == button5.Text && button5.Text == button7.Text && button3.Text != ""))
                {
                    MessageBox.Show("Người chơi X chiến thắng!!!");
                    disableBtn();
                }
                else if (move == 8) MessageBox.Show("Hòa!!!");
                who = 'o';
            }
            move++;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            who = 'o';
            player1 = ""; player2 = "";
            SqlCommand cmd1 = new SqlCommand("update Player set CacODaDanh=@CacODaDanh where Id=@Id", con);
            SqlCommand cmd2 = new SqlCommand("update Player set CacODaDanh=@CacODaDanh where Id=@Id", con);
            con.Open();
            cmd1.Parameters.AddWithValue("@CacODaDanh", player1);
            cmd1.Parameters.AddWithValue("@Id", id_player_1);
            cmd1.ExecuteNonQuery();
            cmd2.Parameters.AddWithValue("@CacODaDanh", player2);
            cmd2.Parameters.AddWithValue("@Id", id_player_2);
            cmd2.ExecuteNonQuery();
            con.Close();
            loadData();

            move = 0;
            button1.Enabled = true; button1.Text = ""; button1.BackColor = Color.White;
            button2.Enabled = true; button2.Text = ""; button2.BackColor = Color.White;
            button3.Enabled = true; button3.Text = ""; button3.BackColor = Color.White;
            button4.Enabled = true; button4.Text = ""; button4.BackColor = Color.White;
            button5.Enabled = true; button5.Text = ""; button5.BackColor = Color.White;
            button6.Enabled = true; button6.Text = ""; button6.BackColor = Color.White;
            button7.Enabled = true; button7.Text = ""; button7.BackColor = Color.White;
            button8.Enabled = true; button8.Text = ""; button8.BackColor = Color.White;
            button9.Enabled = true; button9.Text = ""; button9.BackColor = Color.White;
            tableLayoutPanel1.Enabled = true;
        }
    }
}
