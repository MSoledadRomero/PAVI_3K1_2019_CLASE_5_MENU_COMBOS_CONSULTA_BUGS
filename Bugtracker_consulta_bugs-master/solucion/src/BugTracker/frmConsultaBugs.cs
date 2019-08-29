using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker
{
    public partial class frmConsultaBugs : Form
    {
        public frmConsultaBugs()
        {
            InitializeComponent();
        }

        private void frmBugs_Load(object sender, EventArgs e)
        {

            //LLenar combos y limpiar grid
            LlenarCombo(cboEstados, DBHelper.GetDBHelper().ConsultaSQL("Select * from Estados"), "nombre", "id_estado");

            LlenarCombo(cboPrioridades, DBHelper.GetDBHelper().ConsultaSQL("Select * from Prioridades"), "nombre", "id_prioridad");

            LlenarCombo(cboCriticidades, DBHelper.GetDBHelper().ConsultaSQL("Select * from Criticidades"), "nombre", "id_criticidad");

            LlenarCombo(cboAsignadoA, DBHelper.GetDBHelper().ConsultaSQL("Select * from Usuarios"), "usuario", "id_usuario");

            LlenarCombo(cboProductos, DBHelper.GetDBHelper().ConsultaSQL("Select * from Productos"), "nombre", "id_producto");

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT TOP 20 * FROM bugs WHERE 1=1 ";

            // Dictionary: Representa una colección de claves y valores.
            // Dictionary: Solo se usa si pasamos por parámetro los filtros de la selección de los combos
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            DateTime fechaDesde;
            DateTime fechaHasta;
            if (DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) &&
                DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
            {
                //strSql += " AND (fecha_alta>=@fechaDesde AND fecha_alta<=@fechaHasta) ";
                strSql += " AND (fecha_alta>="+txtFechaDesde.Text + " AND fecha_alta<=" +txtFechaHasta.Text +")" ;
                //parametros.Add("fechaDesde", txtFechaDesde.Text);
                //parametros.Add("fechaHasta", txtFechaHasta.Text);
            }


            if (!string.IsNullOrEmpty(cboEstados.Text))
            {
                var idEstado = cboEstados.SelectedValue.ToString();
                //strSql += "AND (id_estado=@idEstado) ";
                strSql += "AND (id_estado="+ idEstado +") ";
                
                //parametros.Add("idEstado", idEstado);
            }

            if (!string.IsNullOrEmpty(cboAsignadoA.Text))
            {
                var asignadoA = cboAsignadoA.SelectedValue.ToString();
                //strSql += "AND (id_usuario_asignado=@idUsuarioAsignado) ";
                strSql += "AND (id_usuario_asignado="+ asignadoA+") ";
                //parametros.Add("idUsuarioAsignado", asignadoA);
            }

            if (!string.IsNullOrEmpty(cboPrioridades.Text))
            {
                var prioridad = cboPrioridades.SelectedValue.ToString();
               // strSql += "AND (id_prioridad=@idPrioridad) ";
                strSql += "AND (id_prioridad="+ prioridad+") ";
               // parametros.Add("idPrioridad", prioridad);
            }

            if (!string.IsNullOrEmpty(cboCriticidades.Text))
            {
                var criticidad = cboCriticidades.SelectedValue.ToString();
                //strSql += "AND (id_criticidad=@idCriticidad) ";
                strSql += "AND (id_criticidad=" + criticidad+") ";
               //parametros.Add("idCriticidad", criticidad);
            }

            if (!string.IsNullOrEmpty(cboProductos.Text))
            {
                var producto = cboProductos.SelectedValue.ToString();
                //strSql += "AND (id_producto=@idProducto) ";
                strSql += "AND (id_producto="+producto+") ";
                //parametros.Add("idProducto", producto);
            }

            strSql += " ORDER BY fecha_alta DESC";
            //dgvBugs.DataSource = DBHelper.GetDBHelper().ConsultaSQLConParametros(strSql, parametros);
            dgvBugs.DataSource = DBHelper.GetDBHelper().ConsultaSQL(strSql);
            if (dgvBugs.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron coincidencias para el/los filtros ingresados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            // Datasource: establece el origen de datos de este objeto.
            cbo.DataSource = source;
            // DisplayMember: establece la propiedad que se va a mostrar para este ListControl.
            cbo.DisplayMember = display;
            // ValueMember: establece la ruta de acceso de la propiedad que se utilizará como valor real para los elementos de ListControl.
            cbo.ValueMember = value;
            //SelectedIndex: establece el índice que especifica el elemento seleccionado actualmente.
            cbo.SelectedIndex = -1;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
