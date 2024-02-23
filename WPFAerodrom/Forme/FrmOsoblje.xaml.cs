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
    /// Interaction logic for FrmOsoblje.xaml
    /// </summary>
    public partial class FrmOsoblje : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmOsoblje()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtIme.Focus();
        }
        public FrmOsoblje(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtIme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;

        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiKompaniju = @"select kompanijaID, imeKompanije from tbl_AvioKompanija";
                DataTable dtKompanija = new DataTable();
                SqlDataAdapter daKompanija = new SqlDataAdapter(vratiKompaniju, konekcija);


                daKompanija.Fill(dtKompanija);

                cbKompanija.ItemsSource = dtKompanija.DefaultView;
                dtKompanija.Dispose();

            }
            catch (SqlException)
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
                cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@zaduzenje", SqlDbType.NVarChar).Value = txtZaduzenje.Text;
                cmd.Parameters.Add("@kompanijaID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbKompanija.SelectedItem).Row["KompanijaID"].ToString());
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["id"];
                    cmd.CommandText = @"Update tbl_Osoblje set ime = @ime, prezime = @prezime, zaduzenje = @zaduzenje , kompanijaID = @kompanijaID where OsobljeID = @id";
                    this.azuriraj = false;
                }
                else
                {
                    cmd.CommandText = @"insert into tbl_Osoblje(Ime, Prezime , Zaduzenje , KompanijaID) values(@ime , @prezime, @zaduzenje, @kompanijaID)";

                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();


            }
            catch (SqlException)
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
