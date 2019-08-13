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
using System.Configuration;

namespace PjConexion4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["PjConexion4"].ConnectionString);
       

        public void ListaClientes()
        {            
            using (SqlDataAdapter df = new SqlDataAdapter("Usp_ListaClientes_Neptuno_SinFiltro", cn))
            {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "Clientes");
                    tabla.DataSource = Da.Tables["Clientes"];
                    totalClientes.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }

        public void BuscarClientes()
        {
            string variable = txtBoxBuscar.Text;
            using (SqlDataAdapter df = new SqlDataAdapter("Usp_ListaClientes_Neptuno", cn))

            {
               
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.SqlDbType = SqlDbType.VarChar;
                sqlParameter.SqlValue = variable;
                sqlParameter.Size = 100;
                sqlParameter.ParameterName = "@variable";


                df.SelectCommand.Parameters.Add(sqlParameter);
                
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "Clientes");
                    tabla.DataSource = Da.Tables["Clientes"];
                    totalClientes.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            ListaClientes();
            
        }

        private void txtBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarClientes();
        }
    }
}
