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
    /// Interaction logic for FrmPrtljag.xaml
    /// </summary>
    public partial class FrmPrtljag : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmPrtljag()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtTezinaPrtljaga.Focus();
        }

        public FrmPrtljag(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            PopuniPadajuceListe();
            txtTezinaPrtljaga.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiPutnika = @"Select PutnikID, ime, prezime from tbl_Putnik";
                DataTable dtPutnici = new DataTable();
                SqlDataAdapter daPutnici = new SqlDataAdapter(vratiPutnika, konekcija);
                daPutnici.Fill(dtPutnici);
                cbPutnik.ItemsSource = dtPutnici.DefaultView;
                dtPutnici.Dispose();
                dtPutnici.Dispose();

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
                cmd.Parameters.Add(@"tezina", SqlDbType.Int).Value = txtTezinaPrtljaga.Text;
                cmd.Parameters.Add(@"putnikID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbPutnik.SelectedItem).Row["PutnikID"].ToString());
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tbl_Prtljag set tezina = @tezina , putnikID = @putnikID where prtljagID = @id";
                    this.azuriraj = false;
                }
                else
                {
                    cmd.CommandText = @"insert into tbl_Prtljag(tezina, putnikID) values(@tezina, @putnikID)";
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
