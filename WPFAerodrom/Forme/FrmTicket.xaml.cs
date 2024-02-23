using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFAerodrom.Forme
{
    /// <summary>
    /// Interaction logic for FrmTicket.xaml
    /// </summary>
    public partial class FrmTicket : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocnired;
        public FrmTicket()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            cbPutnik.Focus();
        }
        public FrmTicket(bool azuriraj, DataRowView pomocnired)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            cbPutnik.Focus();
            this.azuriraj = azuriraj;
            this.pomocnired = pomocnired;
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();

                string vratiPutnika = @"select putnikID, ime  from tbl_Putnik";
                DataTable dtPutnik = new DataTable();
                SqlDataAdapter daPutnik = new SqlDataAdapter(vratiPutnika, konekcija);
                daPutnik.Fill(dtPutnik);
                cbPutnik.ItemsSource = dtPutnik.DefaultView;
                dtPutnik.Dispose();
                dtPutnik.Dispose();

                string vratiLet = @"select letId , destinacija from tbl_Let";
                DataTable dtLet = new DataTable();
                SqlDataAdapter daLet = new SqlDataAdapter(vratiLet, konekcija);
                daLet.Fill(dtLet);
                cbLet.ItemsSource = dtLet.DefaultView;
                dtLet.Dispose();
                dtLet.Dispose();
            }
            catch(SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@putnikID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbPutnik.SelectedItem).Row["PutnikID"].ToString());
                cmd.Parameters.Add("@letID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbLet.SelectedItem).Row["LetID"].ToString());
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocnired;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Ticket set putnikID = @putnikID, letID = @letID where ticketID = @ticketID";
                    this.pomocnired = null;
                }
                else
                {
                    cmd.CommandText = "insert into tbl_Ticket(putnikID, LetID) values(@putnikID, @letID)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch(SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
